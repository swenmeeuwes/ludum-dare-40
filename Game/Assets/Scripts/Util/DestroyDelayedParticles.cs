using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelayedParticles : MonoBehaviour
{
    [SerializeField] private float _secondsTillDestroy;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(_secondsTillDestroy);

        _particleSystem.Stop();

        yield return  new WaitForSeconds(5f);

        Destroy(gameObject);
    }
}
