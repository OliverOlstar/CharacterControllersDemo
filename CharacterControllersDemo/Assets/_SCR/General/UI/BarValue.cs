using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class BarValue : MonoBehaviour
{
    [HideInInspector] public GameObject root => transform.parent.parent.gameObject; // Override .gameObject to ensure they are referencing the root object

    [SerializeField] private Image topBar = null;
    [SerializeField] private Image bottemBar = null;

    [Header("Colors")]
    [Tooltip("Leave null if color changing is not desired")]
    [SerializeField] private Image coloringImage = null;
    [HideIf("@coloringImage == null"), SerializeField] private Color defaultColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    [HideIf("@coloringImage == null || doHealColor == false"), SerializeField] private Color healColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    [HideIf("@coloringImage == null"), SerializeField] private Color toggledColor = new Color(0.0f, 0.0f, 1.0f, 1.0f);
    [HideIf("@coloringImage == null"), SerializeField] private Color inactiveColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);

    [Header("Timings")]
    [SerializeField, Min(0)] private float delay = 0.75f;
    [Tooltip("Seconds for bar to fill from 0% to 100% (0% to 50% will take half the amount of seconds)")]
    [SerializeField, Min(0.00001f)] private float seconds = 1.0f;
    private float inverseSeconds;
    [ShowIf("@doColorFades && coloringImage != null"), SerializeField, Min(0.00001f)] private float colorSeconds = 0.1f;
    private float inverseColorSeconds;

    [Header("Options")]
    [HideIf("@coloringImage == null"), SerializeField] private bool doHealColor = false;
    [HideIf("@coloringImage == null"), SerializeField] private bool doColorFades = false;

    private bool isToggled = false;
    private Coroutine colorRoutine = null;

    private void Awake() 
    {
        inverseSeconds = 1 / seconds;
        inverseColorSeconds = 1 / colorSeconds;

        topBar.fillAmount = 1;
        bottemBar.fillAmount = 1;
    }

    private void OnEnable() 
    {
        SetColor(isToggled ? toggledColor : defaultColor);
    }

    private void OnDisable() 
    {
        SetColor(inactiveColor);
    }

    public void SetShielded(bool pBool)
    {
        SetColor(pBool ? toggledColor : defaultColor);
        isToggled = pBool;
    }

    public void SetValue(float pValue01)
    {
        if (enabled == false) { return; }

        StopAllCoroutines();

        if (pValue01 > topBar.fillAmount)
        {
            StartCoroutine(TopBarRoutine(pValue01));
            bottemBar.fillAmount = pValue01;

            if (doHealColor)
                SetColor(isToggled ? toggledColor : healColor);
        }
        else
        {
            StartCoroutine(BottemBarRoutine(pValue01));
            topBar.fillAmount = pValue01;
            
            if (doHealColor)
                SetColor(isToggled ? toggledColor : defaultColor);
        }
    }

    private IEnumerator TopBarRoutine(float pValue01)
    {
        yield return new WaitForSeconds(delay);

        while (topBar.fillAmount < pValue01)
        {
            topBar.fillAmount = Mathf.Min(pValue01, topBar.fillAmount + (Time.deltaTime * inverseSeconds));
            yield return null;
        }
    }

    private IEnumerator BottemBarRoutine(float pValue01)
    {
        yield return new WaitForSeconds(delay);

        while (bottemBar.fillAmount > pValue01)
        {
            bottemBar.fillAmount = Mathf.Max(pValue01, bottemBar.fillAmount - (Time.deltaTime * inverseSeconds));
            yield return null;
        }
    }

    public void SetColor(Color pColor)
    {
        if (coloringImage != null)
        {
            if (doColorFades)
            {
                if (colorRoutine != null)
                    StopCoroutine(colorRoutine);
                colorRoutine = StartCoroutine(SetColorRoutine(pColor));
            }
            else
            {
                pColor.a = coloringImage.color.a;
                coloringImage.color = pColor;
            }
        }
    }

    private IEnumerator SetColorRoutine(Color pColor)
    {
        // Check color
        Color startColor = coloringImage.color;
        if (startColor != pColor)
        {
            // Transition color
            float progress01 = 0;
            while (progress01 < 1)
            {
                progress01 += Time.deltaTime * inverseColorSeconds;
                coloringImage.color = Color.Lerp(startColor, pColor, progress01);
                yield return null;
            }
        }
        colorRoutine = null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() 
    {
        if (Application.isPlaying == false)
        {
            if (coloringImage != null)
            {
                coloringImage.color = defaultColor;
            }
        }
    }
#endif
}