﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fireball : MonoBehaviour
{
    [Tooltip("Time in seconds before the object despawns, put to -1 for never.")] public float Lifetime = 30f;

    [Tooltip("If true the fireball will fall through the ground once it hit something.")]
    public bool FallOffWorldOnceHit = false;

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

        if (FallOffWorldOnceHit)
            Invoke("MoveToDeactivedLayer", 0.1f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var iceBlock = other.GetComponent<IceBlock>();
        var meltPower = 8f;
        if (_rigidbody.velocity.magnitude > 2f)
            meltPower = float.MaxValue;
        if (iceBlock != null)
            iceBlock.AddHealth(-meltPower * Time.deltaTime);
    }

    private void MoveToDeactivedLayer()
    {
        gameObject.layer = (int)Layers.Deactivated;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    public void SetAngularDrag(float angularDrag)
    {
        _rigidbody.angularDrag = angularDrag;
    }    
}
