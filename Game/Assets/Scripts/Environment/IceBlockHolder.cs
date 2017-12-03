using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockHolder : MonoBehaviour
{
    [SerializeField] private IceBlock _iceBlock;

    private void Update()
    {
        if (TemperatureManager.Instance != null && TemperatureManager.Instance.Temperature < _iceBlock.FreezeThreshold)
            _iceBlock.gameObject.SetActive(true);
    }

    public void Freeze(float amount)
    {
        _iceBlock.gameObject.SetActive(true);
        _iceBlock.AddHealth(amount);
    }
}
