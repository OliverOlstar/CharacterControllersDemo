using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
    public class CharacterValue : MonoBehaviour
    {
        // [Tooltip("Zero counts as infinite")]
        [SerializeField, Min(0.0f)] protected float value = 100.0f;
        protected float maxValue = 100.0f;

        [SerializeField] protected bool doRecharge = false;
        [SerializeField] private bool canRunOut = true;
        [SerializeField, ShowIf("@canRunOut && doRecharge")] private bool canRechargeBackIn = true;

        [Header("Recharge")]
        [SerializeField, Min(0.0f)] private float rechargeValueTo = 100.0f;
        [SerializeField, Min(0.0f), ShowIf("@doRecharge")] private float rechargeDelay = 1.0f;
        [SerializeField, Min(0.0f), ShowIf("@doRecharge")] private float rechargeRate = 20.0f;

        [Header("UI")]
        [SerializeField] protected BarValue UIBar = null;

        [FoldoutGroup("Events")] public UnityEventsUtil.FloatEvent onValueChanged;
        [FoldoutGroup("Events")] public UnityEventsUtil.FloatEvent onValueLowered;
        [FoldoutGroup("Events")] public UnityEventsUtil.FloatEvent onValueRaised;
        [FoldoutGroup("Events"), ShowIf("@canRunOut")] public UnityEvent onValueOut;
        [FoldoutGroup("Events"), ShowIf("@canRunOut && doRecharge && canRechargeBackIn")] public UnityEvent onValueIn;
        [FoldoutGroup("Events")] public UnityEventsUtil.FloatEvent onMaxValueChanged;

        // OnValueOut (when can not recharge)

        public bool isOut { get; protected set; } = false;

        protected virtual void Start() 
        {
            maxValue = value;
            UIBar.SetValue(1.0f);
        }

        public float Get() { return value; }
        public void Modify(float pValue)
        {
            Set(value + pValue);
        }
        public void Set(float pValue)
        {
            bool lower = pValue < value;
            value = Mathf.Clamp(pValue, 0.0f, maxValue);

            onValueChanged?.Invoke(value);
            if (lower)
            {
                if (canRunOut && isOut == false && value == 0.0f)
                {
                    ValueOut();
                }
                else
                {
                    onValueLowered?.Invoke(value);
                }
            }
            else
            {
                if (canRechargeBackIn && isOut == true && value == maxValue)
                {
                    ValueIn();
                }
                else
                {
                    onValueRaised?.Invoke(value);
                }
            }

            if (doRecharge)
            {
                StopAllCoroutines();
                StartCoroutine(RechargeRoutine());
            }
            
            UIBar.SetValue(value / maxValue);
            onValueChanged?.Invoke(value);
        }

        public float GetMax() { return maxValue; }
        public void ModifyMax(float pValue)
        {
            SetMax(maxValue + pValue);
        }
        public void SetMax(float pValue)
        {
            maxValue = pValue;
            value = Mathf.Clamp(pValue, 0.0f, maxValue);
            
            UIBar.SetValue(value / maxValue);
            onMaxValueChanged?.Invoke(maxValue);
        }

        private IEnumerator RechargeRoutine()
        {
            yield return new WaitForSeconds(rechargeDelay);

            while (value < Mathf.Min(maxValue, rechargeValueTo))
            {
                value += Time.deltaTime * rechargeRate;
                value = Mathf.Min(value, maxValue);

                UIBar.SetValue(value / maxValue);

                yield return null;
            }

            if (canRechargeBackIn && isOut)
                ValueIn();
        }

        private void ValueOut()
        {
            onValueOut?.Invoke();
            isOut = true;

            UIBar.SetToggled(true);
        }

        private void ValueIn()
        {
            onValueIn?.Invoke();
            isOut = false;

            UIBar.SetToggled(false);
        }
    }
}