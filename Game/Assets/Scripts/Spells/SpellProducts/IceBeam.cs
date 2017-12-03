using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBeam : MonoBehaviour
{
    [SerializeField] private float _freezePower = 4f; // If equal or lower than 1 it will only have effect when the environment is cold aswell

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

    private void OnTriggerStay2D(Collider2D other)
    {        
        var iceBlockHolder = other.GetComponent<IceBlockHolder>();
        if (iceBlockHolder)
            iceBlockHolder.Freeze(_freezePower * Time.deltaTime);
    }
}
