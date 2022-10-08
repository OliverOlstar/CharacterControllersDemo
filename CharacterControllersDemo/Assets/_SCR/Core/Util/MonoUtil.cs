using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
	/// <summary>
	/// Public access point for any class to access MonoBehaviour functions
	/// </summary>
    public class MonoUtil : MonoBehaviourSingleton<MonoUtil>
    {
		private void Awake()
		{
			DontDestroyOnLoad(this);
		}
		private void OnDestroy()
		{
			StopAllCoroutines();
		}

		#region Updatables
		public enum UpdateType
		{
			Default = 0,
			Early,
			Late,
			Fixed
		}

		public static class Priorities
		{
			public const int First = int.MaxValue;
			public const int Cameras = -100;
			public const int Default = 0;
			public const int ModelControllers = 100;
			public const int CharacterControllers = 200;
			public const int Last = int.MinValue;
		}

		private struct Updatable
		{
			public int key { get; }
			public Action<float> action { get; }
			public float priority { get; }

			public Updatable(int pKey, Action<float> pAction, float pPriority)
			{
				key = pKey;
				action = pAction;
				priority = pPriority;
			}
		}

		private List<Updatable> updatables = new List<Updatable>();
		private List<Updatable> earlyUpdatables = new List<Updatable>();
		private List<Updatable> lateUpdatables = new List<Updatable>();
		private List<Updatable> fixedUpdatables = new List<Updatable>();

		public static void RegisterUpdate(UnityEngine.Object pKey, Action<float> pUpdatable, UpdateType pType, int pPriority)
		{
			if (Instance == null)
			{
				return;
			}
			ref List<Updatable> items = ref Instance.GetUpdatables(pType);
			int index;
			for (index = 0; index < items.Count; index++)
			{
				if (items[index].priority <= pPriority)
				{
					break;
				}
			}
			items.Insert(index, new Updatable(pKey.GetInstanceID(), pUpdatable, pPriority));
		}

		public static void DeregisterUpdate(UnityEngine.Object pKey, UpdateType pType)
		{
			if (Instance == null)
			{
				return;
			}
			ref List<Updatable> items = ref Instance.GetUpdatables(pType);
			int i = items.FindIndex(u => u.key == pKey.GetInstanceID());
			if (i < 0)
			{
				Debug.LogError($"Updatable {pKey.name} could not be found and failed to be removed.");
				return;
			}
			items.RemoveAt(i);
		}

		private ref List<Updatable> GetUpdatables(UpdateType pType)
		{
			switch (pType)
			{
				case UpdateType.Early:
					return ref earlyUpdatables;
				case UpdateType.Late:
					return ref lateUpdatables;
				case UpdateType.Fixed:
					return ref fixedUpdatables;
				default:
					return ref updatables;
			}
		}

		private void Update()
		{
			foreach (Updatable updatable in Instance.earlyUpdatables)
			{
				updatable.action.Invoke(Time.deltaTime);
			}
			foreach (Updatable updatable in Instance.updatables)
			{
				updatable.action.Invoke(Time.deltaTime);
			}
		}

		private void LateUpdate()
		{
			foreach (Updatable updatable in Instance.lateUpdatables)
			{
				updatable.action.Invoke(Time.deltaTime);
			}
		}

		private void FixedUpdate()
		{
			foreach (Updatable updatable in Instance.fixedUpdatables)
			{
				updatable.action.Invoke(Time.fixedDeltaTime);
			}
		}
		#endregion Updatables

		#region Coroutines
		public static Coroutine Start(in IEnumerator pEnumerator)
		{
			return Instance.StartCoroutine(pEnumerator);
		}
		public static void Stop(ref Coroutine pCoroutine)
		{
			if (pCoroutine == null)
			{
				return;
			}
			Instance.StopCoroutine(pCoroutine);
			pCoroutine = null;
		}
		#endregion Coroutines
	}
}
