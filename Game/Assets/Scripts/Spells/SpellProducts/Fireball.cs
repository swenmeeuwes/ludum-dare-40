using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fireball : MonoBehaviour
{
    [Tooltip("Time in seconds before the object despawns, put to -1 for never.")] public float Lifetime = 30f;

    private ParticleSystem _particleSystem;
    private Rigidbody2D _rigidbody;

    private float _startAwakeTime;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _startAwakeTime = Time.time;

        _particleSystem.Play();
    }

    private void Update()
    {
        if (Lifetime > 0 && Time.time - _startAwakeTime > Lifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CameraManager.Instance != null)
            CameraManager.Instance.Shake(0.2f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var iceBlock = other.GetComponent<IceBlock>();
        if (iceBlock != null)
            iceBlock.AddHealth(-8f * Time.deltaTime);
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
