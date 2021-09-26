using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link_TurnAround : BaseState
{
    [SerializeField] private Link_InputBridge input = null;
    [SerializeField] private Link_AnimController animController = null;
    [SerializeField] private float animationLength = 0.4f;

    public override void OnEnter()
    {
        animController.TriggerTurnAround();
        animController.SetSpeed01(1.0f);

        Invoke(nameof(ReturnToDefault), animationLength);
    }

    public override void OnExit()
    {
        CancelInvoke(nameof(ReturnToDefault));
    }

    public override bool CanEnter()
    {
        return (animController.GetSpeed01() >= 0.75f && Vector3.Angle(transform.forward, input.moveInputVector3) >= 165);
    }

    private void ReturnToDefault()
    {
        machine.ReturnToDefault();
    }
}
