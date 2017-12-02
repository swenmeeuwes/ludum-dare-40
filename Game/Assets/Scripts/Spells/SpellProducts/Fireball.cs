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

    private void Start()
    {        
        //StartCoroutine(PlayParticleSystemDelayed()); // Not needed anymore?
    }        

    // Quick hack to prevent particles from spawning at Vector3.zero in world pos on first tick
    private IEnumerator PlayParticleSystemDelayed()
    {
        yield return null;

        _particleSystem.Play();
    }

    public void Sleep()
    {
        StopAllCoroutines();
        _rigidbody.Sleep();        
        _particleSystem.Stop();        
    }

    public void WakeUp()
    {
        _rigidbody.WakeUp();
        StartCoroutine(PlayParticleSystemDelayed());
    }
}
