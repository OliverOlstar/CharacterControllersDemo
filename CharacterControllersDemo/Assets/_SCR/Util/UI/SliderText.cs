using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class SliderText : MonoBehaviour
{
    [SerializeField] private TMP_Text text = null;
    private Slider slider = null;
    
    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
        OnValueChanged(slider.value);
    }

    private void OnDestroy() 
    {
        slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void OnValueChanged(float pValue)
    {
        text.text = pValue.ToString();
    }
}
