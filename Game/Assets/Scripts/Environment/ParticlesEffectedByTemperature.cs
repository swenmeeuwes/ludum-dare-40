using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlesEffectedByTemperature : EffectedByTemperature
{
    [SerializeField] private bool _onWhenHot;

    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    protected override void OnCold()
    {
        if (_onWhenHot && _particleSystem.isPlaying)
        {
            _particleSystem.Stop();
            var particleSystemMainModule = _particleSystem.main;
            particleSystemMainModule.prewarm = false;
        }
        else if (!_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
    }

    protected override void OnHot()
    {
        if (_onWhenHot)
        {
            if (!_particleSystem.isPlaying)
                _particleSystem.Play();
        }
        else if (_particleSystem.isPlaying)
        {
            _particleSystem.Stop();
            var particleSystemMainModule = _particleSystem.main;
            particleSystemMainModule.prewarm = false;
        }
    }
}
