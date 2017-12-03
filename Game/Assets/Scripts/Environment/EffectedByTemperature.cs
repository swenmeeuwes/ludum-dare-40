using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectedByTemperature : MonoBehaviour
{
    [SerializeField] protected float _coldThreshold;
    [SerializeField] protected float _heatThreshold;

    protected float _triggeredForSeconds = 0;

    private void Update()
    {
        if (TemperatureManager.Instance == null)
            return;

        if (TemperatureManager.Instance.Temperature < _coldThreshold)
        {
            _triggeredForSeconds += Time.deltaTime;
            OnCold();;
        }

        if (TemperatureManager.Instance.Temperature > _heatThreshold)
        {
            _triggeredForSeconds += Time.deltaTime;
            OnHot();
        }
    }

    protected abstract void OnCold();
    protected abstract void OnHot(); // Man can never be hot
}
