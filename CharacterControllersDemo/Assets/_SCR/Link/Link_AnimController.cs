using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Link_AnimController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private float speedDampening = 1.0f;
    private float targetSpeed = 0.0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerTurnAround()
    {
        animator.SetTrigger("TurnAround");
    }

    public void SetSpeed01(float pValue01)
    {
        targetSpeed = pValue01;
    }
    public float GetSpeed01()
    {
        return animator.GetFloat("Speed");
    }

    private void Update() 
    {
        float s = animator.GetFloat("Speed");
        if (targetSpeed != s)
        {
            if (Mathf.Abs(targetSpeed - s) < 0.001f)
            {
                // Round if close enough
                animator.SetFloat("Speed", (targetSpeed));
            }
            else
            {
                // Lerp value
                animator.SetFloat("Speed", Mathf.Lerp(s, targetSpeed, Time.deltaTime * speedDampening));
            }
        }
    }
}
