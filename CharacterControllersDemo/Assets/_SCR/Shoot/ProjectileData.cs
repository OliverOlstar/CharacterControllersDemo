using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.Weapon
{
    [CreateAssetMenu(fileName = "Projectile Data", menuName = "Custom/Projectile Data", order = 1)]
    public class ProjectileData : ScriptableObject
    {
        public enum BulletCollision
        {
            Destroy,
            Stick,
            Penetrate,
            Reflect,
            ReflectBack,
            Physics
        }

        // public enum BulletExplosion
        // {
        //     Null,
        //     ExplodeOnHit,
        //     ExplodeOnDeath
        // }

        // public enum BulletHoming
        // {
        //     Null,
        //     HomingDamageables,
        //     HomingRigidbodies
        // }

        // public enum BulletHomingMovement
        // {
        //     RotateVelocity,
        //     AddForce
        // }
        
        [Title("Raycast")]
        public bool useRaycast = false;
        [ShowIf("@useRaycast")] public LayerMask layerMask = new LayerMask();
        
        [Title("Damage")]
        public int damage = 1;
        [Range(0, 1)] public float critChance01 = 0.1f;
        [HideIf("@critChance01 == 0")] public float critDamageMultiplier = 2;
        
        [Title("Stats")]
        public Vector2 lifeTime = new Vector2(4.0f, 4.5f);
        public Vector2 shootForce = new Vector2(5.0f, 5.0f);
        public float bulletGravity = 0.0f;

        [Header("Collision")]
        public BulletCollision bulletCollision = BulletCollision.Destroy;
        public float hitForce = 8;
    }
}