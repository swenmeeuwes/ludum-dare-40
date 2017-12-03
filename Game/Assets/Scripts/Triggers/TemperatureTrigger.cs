using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TemperatureTrigger : TagTrigger
{
    [SerializeField] private float _setTemperatureTo = 0.5f;   

    protected override void Trigger(Collider2D other)
    {
        base.Trigger(other);

        if (TemperatureManager.Instance != null)
            TemperatureManager.Instance.SetTemperature(_setTemperatureTo);
    }

    private void OnDrawGizmos()
    {
        _collider = GetComponent<Collider2D>();
        if (_collider == null)
            return;

        Gizmos.color = _setTemperatureTo >= 0.5 ? Color.red : Color.blue;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
