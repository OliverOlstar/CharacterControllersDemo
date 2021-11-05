using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Sirenix.OdinInspector;

public class WeaponMultiplayer : MonoBehaviour
{
    [Required] public WeaponData data = null;

    [Header("References")]
    [SerializeField] private ParticleSystem muzzleFlash = null;
    [ShowIf("@muzzleFlash != null")] [SerializeField] private Vector3 muzzleFlashRelOffset = new Vector3();
    public AudioSourcePool sourcePool = null;
    public GameObject sender = null;

    [FoldoutGroup("Unity Events")] public UnityEvent OnShoot;
    [FoldoutGroup("Unity Events")] public UnityEvent OnFailedShoot;

    private void Start() 
    {
        if (sender == null)
        {
            sender = gameObject;
        }
    }

    [PunRPC]
    public void RPC_ShootProjectile(Vector3 pPoint, Vector3 pForce)
    {
        // Spawn projectile
        GameObject projectile;
        if (data.projecilePoolKey != "")
        {
            projectile = ObjectPoolDictionary.dictionary[data.projecilePoolKey].CheckOutObject(true);
            if (projectile == null)
                return;
        }
        else
        {
            projectile = Instantiate(data.projectilePrefab);
        }
        
        projectile.transform.position = pPoint;
        projectile.transform.rotation = Quaternion.LookRotation(pForce);

        Projectile projectileScript = projectile.GetComponentInChildren<Projectile>();
        projectileScript.data = data;

        projectileScript.sender = sender;

        projectileScript.Init(pForce);

        DidShot();
    }

    // [PunRPC]
    // private void RPC_ShootRaycast(Vector3 pHitPoint, Vector3 pHitNormal)
    // {
    //     ApplyParticleFX(pHitPoint, Quaternion.FromToRotation(Vector3.forward, pHitNormal), pHitCollider);

    //     // push object if rigidbody
    //     Rigidbody hitRb = pHitCollider.attachedRigidbody;
    //     if (hitRb != null)
    //         hitRb.AddForceAtPosition(pForce, pHitPoint);

    //     DidShot(pMuzzle);
    // }

    [PunRPC]
    private void RPC_ShootRaycastMissed(Transform pMuzzle)
    {
        DidShot();
    }

    [PunRPC]
    private void RPC_ShootFailed()
    {
        // Audio
        data.faildShotSound.Play(sourcePool.GetSource());

        // Event
        OnFailedShoot?.Invoke();
    }

    private void DidShot()
    {
        // Particles
        // if (muzzleFlash != null)
        // {
        //     if (muzzleFlash.transform.parent != pMuzzle)
        //     {
        //         muzzleFlash.transform.SetParent(pMuzzle);
        //         muzzleFlash.transform.localPosition = muzzleFlashRelOffset;
        //         muzzleFlash.transform.localRotation = Quaternion.identity;
        //     }
        //     muzzleFlash.Play();
        // }

        // Audio
        if (sourcePool != null)
            data.shotSound.Play(sourcePool.GetSource());

        // Event
        OnShoot?.Invoke();
    }

    private void ApplyParticleFX(Vector3 position, Quaternion rotation, Collider attachTo) 
    {
        if (data.hitFXPrefab) 
        {
            GameObject impact = Instantiate(data.hitFXPrefab, position, rotation) as GameObject;
        }
    }
}
