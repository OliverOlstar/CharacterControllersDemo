using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

[RequireComponent(typeof(PhotonView))]
public class PhotonViewPlayerInstance : MonoBehaviour
{
    private PhotonView photonView;

    [SerializeField] private CinemachineVirtualCamera myCamera = null;
    [SerializeField] private Rigidbody myRigidbody = null;
    [SerializeField] private OliverLoescher.DamageFlash damageFlash = null;
    [SerializeField] private GameObject[] destroyObjectsIfNotMine = new GameObject[0];
    [SerializeField] private GameObject[] destroyObjectsIfMine = new GameObject[0];

    private void Awake() 
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine == false)
        {
            myCamera.enabled = false;
            myRigidbody.isKinematic = true;
            foreach (GameObject go in destroyObjectsIfNotMine)
            {
                DestroyImmediate(go);
            }
        }
        else
        {
            DestroyImmediate(damageFlash);
            foreach (GameObject go in destroyObjectsIfMine)
            {
                DestroyImmediate(go);
            }
        }
    }
}
