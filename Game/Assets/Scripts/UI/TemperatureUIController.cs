using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TemperatureUIController : MonoBehaviour
{    
    [SerializeField] private Slider _temperatureSlider;
    [SerializeField] private Color _coldColor;
    [SerializeField] private Color _hotColor;

    private Animator _animator;

    private Coroutine _activeShakeCoroutine;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _temperatureSlider.onValueChanged.AddListener(OnTemperatureSliderValueChanged);
    }

    private void OnDestroy()
    {
        _temperatureSlider.onValueChanged.RemoveListener(OnTemperatureSliderValueChanged);
    }

    private void Update()
    {
        if (TemperatureManager.Instance != null)
            _temperatureSlider.value = TemperatureManager.Instance.Temperature;
    }

    public void Hide(bool hide)
    {
        if (_animator != null)
            _animator.SetBool("Hidden", hide);
    }

    private void OnTemperatureSliderValueChanged(float newValue)
    {
        _temperatureSlider.fillRect.gameObject.GetComponent<Image>().color = Color.Lerp(_coldColor, _hotColor, newValue);

        if (newValue < 0.15f || newValue > 0.85)
        {
            if (_activeShakeCoroutine == null)
                _activeShakeCoroutine = StartCoroutine(Shake());
        }
        else if (_activeShakeCoroutine != null)
        {
            StopCoroutine(_activeShakeCoroutine);
            _activeShakeCoroutine = null;
            _temperatureSlider.GetComponent<RectTransform>().rotation = Quaternion.identity;            
        }
    }   

    private IEnumerator Shake()
    {
        var rectTransform = _temperatureSlider.GetComponent<RectTransform>();
        while (true)
        {
            rectTransform.rotation = Quaternion.Euler(rectTransform.rotation.x, rectTransform.rotation.y, Random.value * 6);
            yield return null;
        }
    }
}
