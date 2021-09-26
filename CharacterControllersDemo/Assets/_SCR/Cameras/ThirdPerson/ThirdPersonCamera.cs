using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform followTarget = null;
    [SerializeField] private Vector3 offset = new Vector3(0.0f, 2.0f, -5.0f);

    private void LateUpdate() 
    {
        transform.position = followTarget.position + offset;
    }

    private void OnDrawGizmosSelected() 
    {
        if (Application.isPlaying == false)
        {
            LateUpdate();
        }
    }
}
