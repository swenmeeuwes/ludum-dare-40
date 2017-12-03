using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    [Tooltip("Temperature to warm to")]
    [Range(0, 1)]
    [SerializeField] private float _warmTill = 0.6f;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        if (_particleSystem != null)
            _particleSystem.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null && TemperatureManager.Instance != null &&
            TemperatureManager.Instance.Temperature < _warmTill)
        {
            TemperatureManager.Instance.SetTemperature(_warmTill);
            if (_particleSystem != null)
                _particleSystem.Play();
        }
    }
}
