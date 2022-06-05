using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Link
{
    public class Link_Roll : BaseState
    {
        [SerializeField] private InputBridge_Platformer input = null;
        [SerializeField] private Link_AnimController animController = null;

        public override void Init(StateMachine pMachine)
        {
            base.Init(pMachine);

            input.onRollPerformed.AddListener(TryEnter);
        }

        public override void OnEnter()
        {
            animController.TriggerRoll();
            Invoke(nameof(TryExit), 0.5f);
        }

        public override void OnExit()
        {
            CancelInvoke(nameof(TryExit));
        }

        public void TryEnter()
        {
            if (machine.IsDefaultState())
                machine.SwitchState(this);
        }

        public void TryExit()
        {
            machine.ReturnToDefault();
        }
    }
}
