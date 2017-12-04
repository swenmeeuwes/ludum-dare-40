using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhase
{
    FireballRain,
    Frost
}

[RequireComponent(typeof(Animator))]
public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject _bossFireballPrefab;
    [SerializeField] private Rect _fireballSpawnArea;
    [SerializeField] private int _fireballSpawnCount;

    [SerializeField] private GameObject _bossFrostPrefab;
    [SerializeField] private Vector2 _frostSpawnPosition; // todo: should be refacted to rect as an area

    public BossPhase CurrentPhase = BossPhase.Frost;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();        
    }

    private void Update()
    {
        var fireballs = FindObjectsOfType<Fireball>();
        foreach (var fireball in fireballs)
        {
            if (Vector2.Distance(transform.position, fireball.transform.position) < 1.2f)
                fireball.SetVelocity(new Vector2(-10f, 10f));
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
