using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace OliverLoescher
{
	[RequireComponent(typeof(PhotonView)), DisallowMultipleComponent]
	public class CharacterValuePhotonSync : MonoBehaviour
	{
		[System.Serializable]
		private class ValueSync
		{
			public CharacterValue value = null;
			private PhotonView view;
			private int index;

			[SerializeField] private bool doLogs = false;

			public void Initalize(PhotonView pView, int pIndex)
			{
				view = pView;
				index = pIndex;
				if (view.IsMine)
				{
					value.onValueChangedEvent += OnValueChanged;
				}
			}

			private void OnValueChanged(float pValue, float pDelta)
			{
				if (pDelta < 0)
				{
					view.RPC(nameof(RPC_OnValueChanged), RpcTarget.Others, index, pValue);
				}
			}
			public void Set(float pValue, PhotonMessageInfo pInfo)
			{
				if (doLogs)
				{
					Debug.Log($"{pInfo.Sender.NickName} Changed {view.Owner.NickName}'s {value.GetType().ToString()}: {pValue}");
				}
				value.Set(pValue);
			}
		}

		[SerializeField] private ValueSync[] valueSyncs = new ValueSync[1];
		private PhotonView view;

		private void Start()
		{
			view = GetComponent<PhotonView>();
			for (int i = 0; i < valueSyncs.Length; i++)
			{
				valueSyncs[i].Initalize(view, i);
			}
		}
		
		[PunRPC]
		public void RPC_OnValueChanged(int pIndex, float pValue, PhotonMessageInfo pInfo)
		{
			valueSyncs[pIndex].Set(pValue, pInfo);
		}
	}
}