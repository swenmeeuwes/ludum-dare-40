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

    public void AddTemperature(float addition)
    {
        _temperature = Mathf.Clamp01(_temperature + addition);
    }
}
