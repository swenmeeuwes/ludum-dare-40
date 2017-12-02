using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fireball : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _particleSystem.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CameraManager.Instance != null)
            CameraManager.Instance.Shake(0.2f);
    }

    // Quick hack to prevent particles from spawning at Vector3.zero in world pos on first tick
    [Obsolete]
    private IEnumerator PlayParticleSystemDelayed()
    {
        yield return null;

        _particleSystem.Play();
    }

    [Obsolete]
    public void Sleep()
    {
        StopAllCoroutines();
        _rigidbody.Sleep();
        _particleSystem.Stop();
    }

    [Obsolete]
    public void WakeUp()
    {
        _rigidbody.WakeUp();
        StartCoroutine(PlayParticleSystemDelayed());
    }
}
