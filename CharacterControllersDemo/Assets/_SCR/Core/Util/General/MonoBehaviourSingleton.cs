using OliverLoescher;
using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>, new()
{
    private static T _Instance = null;
    public static T Instance
    {
        get
        {
            if (!Util.IsApplicationQuitting && _Instance == null)
            {
                _Instance = new GameObject().AddComponent<T>();
				_Instance.gameObject.name = _Instance.GetType().Name;
				DontDestroyOnLoad(_Instance.gameObject);
			}
			return _Instance;
        }
    }
}