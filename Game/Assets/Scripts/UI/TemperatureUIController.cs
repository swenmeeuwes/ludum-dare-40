using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureUIController : MonoBehaviour
{    
    [SerializeField] private Slider _temperatureSlider;
    [SerializeField] private Color _coldColor;
    [SerializeField] private Color _hotColor;

    private void Start()
    {
        _temperatureSlider.onValueChanged.AddListener(UpdateTemperatureSliderColor);
    }

    private void OnDestroy()
    {
        _temperatureSlider.onValueChanged.RemoveListener(UpdateTemperatureSliderColor);
    }

    private void Update()
    {
        if (TemperatureManager.Instance != null)
            _temperatureSlider.value = TemperatureManager.Instance.Temperature;
    }

    private void UpdateTemperatureSliderColor(float newValue)
    {
        _temperatureSlider.fillRect.gameObject.GetComponent<Image>().color = Color.Lerp(_coldColor, _hotColor, newValue);
    }
}
