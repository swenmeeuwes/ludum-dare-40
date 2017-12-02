using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class StressingNpc : MonoBehaviour
{
    [SerializeField] private Vector2 _speedBounds;
    [SerializeField] private Vector2 _rangeBounds;
    public bool IsStressing;

    public Vector2 CurrentCheckpoint
    {
        get { return _path[_currentCheckpointIndex]; }
    }

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private float _speed;
    private Vector2[] _path;
    private int _currentCheckpointIndex;    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        InitializePath();
        _currentCheckpointIndex = 0;
        _speed = Random.Range(_speedBounds.x, _speedBounds.y);        
    }

    private void Start()
    {
        NpcManager.Instance.Register(this);
    }

    private void Update()
    {
        _animator.SetBool("IsWalking", IsStressing);

        if (!IsStressing)
            return;

        var direction = (CurrentCheckpoint - (Vector2)transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);

        _spriteRenderer.flipX = direction.x < 0;

        if (Vector2.Distance(transform.position, CurrentCheckpoint) < 0.1f)
            NextCheckpoint();
    }

    private void InitializePath()
    {
        var randomRange = Random.Range(_rangeBounds.x, _rangeBounds.y);
        _path = new Vector2[]
        {
            new Vector2(transform.position.x - randomRange, transform.position.y),
            new Vector2(transform.position.x + randomRange, transform.position.y)
        };
    }

    private void NextCheckpoint()
    {
        _currentCheckpointIndex++;

        if (_currentCheckpointIndex > _path.Length - 1)
            _currentCheckpointIndex = 0;
    }
}
