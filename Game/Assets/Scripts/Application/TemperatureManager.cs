using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureManager : MonoSingleton<TemperatureManager>
{
    [Tooltip("Scales from 0 to 1, where 0 is cold and 1 hot")][Range(0, 1)] public float Temperature;

    private void Awake()
    {
        DefineSingleton(this);
    }
}
