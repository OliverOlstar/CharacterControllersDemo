using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    [System.Serializable]
    private class Linecast
    {
        [SerializeField] private Vector3 startPosition = new Vector3();
        [SerializeField] private float distance = 1.0f;

        [HideInInspector] public RaycastHit hitInfo = new RaycastHit();

        public bool Check(Transform pTransform, LayerMask pLayerMask)
        {
            Vector3 start = pTransform.TransformPoint(startPosition);
            Vector3 end = start - (pTransform.up * distance);
            return Physics.Linecast(start, end, out hitInfo, pLayerMask);
        }

        public void OnDrawGizmos(Transform pTransform, LayerMask pLayerMask) 
        {
            Vector3 start = pTransform.TransformPoint(startPosition);
            Vector3 end = start - (pTransform.up * distance);
            Gizmos.color = Check(pTransform, pLayerMask) ? Color.green : Color.red;
            Gizmos.DrawLine(start, end);
        }
    }

    [SerializeField] private Linecast[] lines = new Linecast[1];
    [SerializeField] private LayerMask layerMask = new LayerMask();

    private float lastFrameTime = 0.0f;
    private bool lastFrameGrounded = false;
    
    public bool IsGrounded()
    {
        if (Time.time == lastFrameTime)
        {
            // Incase grounded has already been check this frame
            return lastFrameGrounded;
        }
        else
        {
            foreach (Linecast line in lines)
            {
                if (line.Check(transform, layerMask))
                    return true;
            }
            return false;
        }
    }

    public Vector3 GetAverageNormal()
    {
        if (lines.Length == 1)
        {
            return lines[0].hitInfo.normal;
        }
        else
        {
            Vector3 total = Vector3.zero;
            foreach (Linecast line in lines)
            {
                total += line.hitInfo.normal;
            }
            return total / lines.Length;
        }
    }

    private void OnDrawGizmosSelected() 
    {
        foreach (Linecast line in lines)
        {
            line.OnDrawGizmos(transform, layerMask);
        }
    }
}