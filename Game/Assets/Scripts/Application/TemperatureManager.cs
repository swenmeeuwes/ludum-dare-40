using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureManager : MonoSingleton<TemperatureManager>
{
    [Tooltip("Scales from 0 to 1, where 0 is cold and 1 hot")][Range(0, 1)][SerializeField] private float _temperature;
    
    public float Temperature {
        get { return _temperature; }
    }

    private void Awake()
    {
        DefineSingleton(this);
    }

    public void AddTemperature(float addition, bool instant = false)
    {
        var targetTemperature = Mathf.Clamp01(_temperature + addition);

        if (instant)
        {
            _temperature = targetTemperature;
            return;
        }

        // todo: replace by coroutine?
        iTween.ValueTo(gameObject, iTween.Hash(
                "from", _temperature,
                "to", targetTemperature,
                "time", 0.6f,
                "onupdatetarget", gameObject,
                "onupdate", "TemperatureTweenOnUpdateCallBack",
                "easetype", iTween.EaseType.easeInOutCirc
            )
        );
    }

    private void TemperatureTweenOnUpdateCallBack(float newValue)
    {
        _temperature = newValue;
    }
}
