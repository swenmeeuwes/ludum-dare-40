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
    private bool _didCameraShake = false;

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
        var screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (!_didCameraShake && CameraManager.Instance != null && screenPosition.x > 0 && screenPosition.x < 1 &&
            screenPosition.y > 0 && screenPosition.y < 1)
        {            
            CameraManager.Instance.Shake(0.2f);
            _didCameraShake = true;
        }

        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.Hit();
            gameObject.layer = (int) Layers.Deactivated;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var iceBlock = other.GetComponent<IceBlock>();
        if (iceBlock != null)
            iceBlock.AddHealth(-8f * Time.deltaTime);
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    public void SetAngularDrag(float angularDrag)
    {
        _rigidbody.angularDrag = angularDrag;
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
