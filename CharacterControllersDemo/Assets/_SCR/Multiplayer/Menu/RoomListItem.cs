using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

namespace OliverLoescher.Multiplayer
{
	public class RoomListItem : MonoBehaviour
	{
		[SerializeField] private TMP_Text nameText = null;
		// [SerializeField] private TMP_Text countText = null;
		private RoomInfo info;

		public void Init(RoomInfo pInfo)
		{
			info = pInfo;
			nameText.text = info.Name;
			// countText.text = info.PlayerCount + "/" + info.MaxPlayers;
		}

		public RoomInfo GetInfo()
		{
			return info;
		}

		public void OnClick()
		{
			Launcher.Instance.JoinRoom(info);
		}
	}
}