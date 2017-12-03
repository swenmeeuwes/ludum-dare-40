using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// todo: refactor to work with the 'EffectedByTemperature' class
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class IceBlock : MonoBehaviour
{
    public bool DestoryOnMelt = true;
    [SerializeField] private bool _startFrozen = true;

    [SerializeField] private float _freezeThreshold;
    [SerializeField] private float _meltThreshold;
    [SerializeField] private float _secondsTillStateChange;    

    public GameObject Holding;

    public float FreezeThreshold { get { return _freezeThreshold; } }
    public float MeltThreshold { get { return _meltThreshold; } }

    private float _health;
    private float _startOpacity;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private void Awake()
    {
        _health = _secondsTillStateChange;        

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        _startOpacity = _spriteRenderer.color.a;        
    }

    private void Start()
    {
        ReleaseHoldingObject(false);

        if (!_startFrozen)
            AddHealth(-float.MaxValue);
    }

    private void Update()
    {
        if (_health <= 0)
        {
            ReleaseHoldingObject(true);

            if (DestoryOnMelt)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }

        var screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (TemperatureManager.Instance != null && screenPosition.x > 0 && screenPosition.x < 1 &&
            screenPosition.y > 0 && screenPosition.y < 1)
        {
            if (TemperatureManager.Instance.Temperature < _freezeThreshold)
                AddHealth(Time.deltaTime);
            if (TemperatureManager.Instance.Temperature > _meltThreshold)
                AddHealth(-Time.deltaTime);
        }
    }

    public void AddHealth(float amount)
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
