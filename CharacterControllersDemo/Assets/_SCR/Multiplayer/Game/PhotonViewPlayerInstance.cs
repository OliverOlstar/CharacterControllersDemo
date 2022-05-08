using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using TMPro;

[RequireComponent(typeof(PhotonView))]
public class PhotonViewPlayerInstance : MonoBehaviour
{
	private PhotonView photonView;

	[SerializeField] private TMP_Text nikNameText = null;
	[SerializeField] private Rigidbody rigid = null;
	[SerializeField] private Rigidbody2D rigid2D = null;
	[SerializeField] private GameObject[] destroyObjectsIfNotMine = new GameObject[0];
	[SerializeField] private GameObject[] destroyObjectsIfMine = new GameObject[0];

	private void Start() 
	{
		photonView = GetComponent<PhotonView>();
		if (photonView.IsMine == false)
		{
			if (rigid != null)
				rigid.isKinematic = true;
			if (rigid2D != null)
				rigid2D.isKinematic = true;

			foreach (GameObject go in destroyObjectsIfNotMine)
			{
				DestroyImmediate(go);
			}
		}
		else
		{
			foreach (GameObject go in destroyObjectsIfMine)
			{
				DestroyImmediate(go);
			}
		}

		nikNameText.text = photonView.Owner.NickName;
	}
}
