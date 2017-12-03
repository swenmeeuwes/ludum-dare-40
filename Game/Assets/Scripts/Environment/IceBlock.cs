using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// todo: refactor to work with the 'EffectedByTemperature' class
[RequireComponent(typeof(SpriteRenderer))]
public class IceBlock : MonoBehaviour
{
    [SerializeField] private float _freezeThreshold;
    [SerializeField] private float _meltThreshold;
    [SerializeField] private float _secondsTillStateChange;

    public GameObject Holding;

    private float _health;
    private float _startOpacity;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _health = _secondsTillStateChange;        

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startOpacity = _spriteRenderer.color.a;       
    }

    private void Start()
    {
        ReleaseHoldingObject(false);
    }

    private void Update()
    {
        if (TemperatureManager.Instance != null)
        {
            if (TemperatureManager.Instance.Temperature < _freezeThreshold)
                AddHealth(Time.deltaTime);
            if (TemperatureManager.Instance.Temperature > _meltThreshold)
                AddHealth(-Time.deltaTime);
        }

        if (_health <= 0)
        {
            ReleaseHoldingObject(true);
            Destroy(gameObject);
        }
    }

    private void AddHealth(float amount)
    {
        _health = Mathf.Clamp(_health + amount, 0, _secondsTillStateChange);

        var newColor = _spriteRenderer.color;
        newColor.a = (_health / _secondsTillStateChange) * _startOpacity;

        _spriteRenderer.color = newColor;
    }

    private void ReleaseHoldingObject(bool release)
    {
        if (Holding != null)
        {
            Holding.transform.position = transform.position;

            var holdingColliders = Holding.GetComponents<Collider2D>();
            foreach (var collider in holdingColliders)
            {
                collider.enabled = release;
            }

            var holdingRigidbody = Holding.GetComponent<Rigidbody2D>();
            if (holdingRigidbody)
            {
                if (release)
                    holdingRigidbody.WakeUp();
                else
                    holdingRigidbody.Sleep();
            }

            var holdingParticleSystems = Holding.GetComponentsInChildren<ParticleSystem>();
            foreach (var particleSystem in holdingParticleSystems)
            {
                if (release)
                    particleSystem.Play();
                else
                    particleSystem.Stop();
            }
        }
    }
}
