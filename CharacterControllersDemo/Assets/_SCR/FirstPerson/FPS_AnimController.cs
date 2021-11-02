using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_AnimController : MonoBehaviour
{
    [SerializeField] private Animator animController = null;

    [Header("Rotation")]
    [SerializeField] private Transform forwardCamera = null;

    private void LateUpdate() 
    {
        transform.rotation = Quaternion.Euler(0.0f, forwardCamera.eulerAngles.y, 0.0f);
    }
}
