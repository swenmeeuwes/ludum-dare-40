using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BossPhase
{
    FireballRain,
    Frost
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Boss : MonoEventDispatcher
{
    public static readonly string Died = "Boss.Died";

    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _hitMaterial;

    [SerializeField] private Slider _temperatureIndicator;

    [SerializeField] private GameObject _bossFireballPrefab;
    [SerializeField] private Rect _fireballSpawnArea;
    [SerializeField] private int _fireballSpawnCount;

    [SerializeField] private GameObject _bossFrostPrefab;
    [SerializeField] private Vector2 _frostSpawnPosition; // todo: should be refacted to rect as an area

    [SerializeField] private Vector2 _hurtTemperatureThresholds;
    [SerializeField] private float _survivalTimeWhileInPain; // In seconds    

    public BossPhase CurrentPhase = BossPhase.Frost;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private float _health;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _health = _survivalTimeWhileInPain;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            Dispatch(new EventObject
            {
                Sender = this,
                Type = Died,
                Data = null
            });

            return;
        }

        if (TemperatureManager.Instance != null)
            _temperatureIndicator.value = (TemperatureManager.Instance.Temperature - _hurtTemperatureThresholds.x) / _hurtTemperatureThresholds.y;

        var fireballs = FindObjectsOfType<Fireball>();
        foreach (var fireball in fireballs)
        {
            if (Vector2.Distance(transform.position, fireball.transform.position) < 1.2f)
                fireball.SetVelocity(new Vector2(-10f, 10f));
        }

        if (TemperatureManager.Instance != null)
        {
            var isTakingDamage = (TemperatureManager.Instance.Temperature < _hurtTemperatureThresholds.x ||
                                  TemperatureManager.Instance.Temperature > _hurtTemperatureThresholds.y);

            var material = isTakingDamage ? _hitMaterial : _defaultMaterial;
            _spriteRenderer.material = material;

            if (isTakingDamage)
                _health -= Time.deltaTime;
        }       
    }

    public void BeginCasting()
    {
        _animator.SetTrigger("BeginCasting");
        StartCoroutine(CastWhenAnimated());
    }

    private IEnumerator SpawnFireballs()
    {
        var fireballsLeft = _fireballSpawnCount;

        while (fireballsLeft > 0)
        {
            SpawnFireball();

            yield return new WaitForSeconds(0.1f);

            fireballsLeft--;
        }
    }

    private void SpawnFireball()
    {
        var instantiatePosition = new Vector2(
            _fireballSpawnArea.x + Random.value * _fireballSpawnArea.width,
            _fireballSpawnArea.y + Random.value * _fireballSpawnArea.height);

        var fireballObject = Instantiate(_bossFireballPrefab, instantiatePosition, Quaternion.identity);
        var fireball = fireballObject.GetComponent<Fireball>();
        fireball.SetVelocity(Vector2.down * 5f);
        fireball.FallOffWorldOnceHit = true;

        if (TemperatureManager.Instance != null)
            TemperatureManager.Instance.AddTemperature(0.01f, true);
    }

    private void SpawnFrost()
    {
        var frostObject = Instantiate(_bossFrostPrefab, _frostSpawnPosition, Quaternion.identity);

        if (TemperatureManager.Instance != null)
            TemperatureManager.Instance.AddTemperature(-0.2f, false);
    }

    private IEnumerator CastWhenAnimated()
    {
        yield return new WaitForSeconds(0.5f);

        // Cast spells
        switch (CurrentPhase)
        {
            case BossPhase.FireballRain:
                StartCoroutine(SpawnFireballs());
                break;
            case BossPhase.Frost:
                SpawnFrost();
                break;
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(_fireballSpawnArea.x + _fireballSpawnArea.width / 2, _fireballSpawnArea.y + _fireballSpawnArea.height / 2), new Vector3(_fireballSpawnArea.width, _fireballSpawnArea.height));
    }
}
