using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Camera
{
    public class FreeflyRigidbodyCamera : FreeflyCamera
    {
        [SerializeField] private Rigidbody moveRigidbody = null;

        private void Start() 
        {
            moveTransform = null;
        }

        protected override void DoMove(Vector2 pMovement, float pUp, float pMult)
        {
            Vector3 move = (pMovement.y * transform.forward) + (pMovement.x * transform.right) + (pUp * transform.up);
            moveRigidbody.velocity = move.normalized * pMult;
        }
    }
}