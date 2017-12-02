using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [SerializeField] private float _slowDownIfCold;
    [Tooltip("Temperature seen as 'cold' (when the movement speed will be effected).")]
    [SerializeField] private float _isColdThreshold;

    public float Speed;
    public float JumpForce;
    public float WallJumpForce;

    public bool IsMoving { get; set; }
    public bool IsFacingRight { get; set; } // Helper property

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private CollisionCheck _groundCheck;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _animator.SetBool("IsGrounded", true);

        if (_slowDownIfCold > Speed)
        {
            _slowDownIfCold = Speed;
            Debug.LogWarning("Slow down if cold must be lower or equal to speed!");
        }
    }

    private void Update()
    {
        var input = new Vector2(Input.GetAxis(InputAxesLiterals.Horizontal), Input.GetAxis(InputAxesLiterals.Vertical));

        // Moving
        var movementSpeed = Speed;
        if (TemperatureManager.Instance.Temperature < _isColdThreshold)
            movementSpeed -= _slowDownIfCold * (1 - TemperatureManager.Instance.Temperature / _isColdThreshold);
        transform.Translate(Vector2.right * input.x * movementSpeed * Time.deltaTime);

        IsMoving = Mathf.Abs(input.x) > 0.2;
        if (IsMoving)
            _spriteRenderer.flipX = !(input.x > 0.2);

        // Update the animator
        _animator.SetFloat("InputX", Mathf.Abs(input.x));
        _animator.SetBool("IsGrounded", _groundCheck.IsColliding);
    }

    private void FixedUpdate()
    {
        var input = new Vector2(Input.GetAxis(InputAxesLiterals.Horizontal), Input.GetAxis(InputAxesLiterals.Vertical));                              

        // Jumping
        if (_groundCheck.IsColliding && Input.GetButton(InputAxesLiterals.Jump))
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpForce);

        // Make the jumping nicer
        if (_rigidbody.velocity.y < 0)
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        else if (_rigidbody.velocity.y > 0 && !Input.GetButton(InputAxesLiterals.Jump))
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;        
    }    
}