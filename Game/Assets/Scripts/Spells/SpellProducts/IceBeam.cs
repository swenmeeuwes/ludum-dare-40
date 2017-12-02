using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBeam : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void CleanUp()
    {
        _particleSystem.Stop();
        StartCoroutine(PrepareForDestruction());
    }

    private IEnumerator PrepareForDestruction()
    {
        yield return new WaitUntil(() => !_particleSystem.IsAlive());

        Destroy(gameObject);
    }
}
