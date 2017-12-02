using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;

    public float Speed;
    public float JumpForce;    

    public bool IsMoving { get; set; }

    private Vector3 _lastPosition;

    private Rigidbody2D _rigidbody;
    private GroundCheck _groundCheck;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
    }

    private void Update()
    {
        _lastPosition = transform.position;

        var input = new Vector2(Input.GetAxis(InputAxesLiterals.Horizontal), Input.GetAxis(InputAxesLiterals.Vertical));

        // Moving
        transform.Translate(new Vector2(input.x, 0) * Speed * Time.deltaTime);

        // Jumping
        if (_groundCheck.IsGrounded && Input.GetButton(InputAxesLiterals.Jump))
            _rigidbody.velocity = Vector2.up * JumpForce;

        // Make the jumping nicer
        if (_rigidbody.velocity.y < 0)
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        else if (_rigidbody.velocity.y > 0 && !Input.GetButton(InputAxesLiterals.Jump))
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;

        IsMoving = _lastPosition != transform.position;

        Debug.Log(_groundCheck.IsGrounded);
    }
}