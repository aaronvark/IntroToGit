using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 4f;

    [Header("Jumping")]
    public float jumpVelocity = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    [Space]
    public float groundCheckRadius = 0.1f;
    public LayerMask excludePlayer;
    
    private Rigidbody2D rb;

    [HideInInspector] public bool isGrounded;
    private bool isJumping;

    void OnEnable()
    {
        PlayerInput.input += DoMove;
        PlayerInput.jump += DoJump;
        
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDisable()
    {
        PlayerInput.input -= DoMove;
        PlayerInput.jump -= DoJump;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, excludePlayer);
        Player.instance.InvokeGroundedAction(isGrounded);

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        } else if (rb.velocity.y > 0 && !isJumping)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }

    private void DoMove(float _movement)
    {
        rb.velocity = new Vector2(_movement * speed, rb.velocity.y);
    }

    private void DoJump(bool _isJumping)
    {
        isJumping = _isJumping;
        
        if (isGrounded && isJumping) 
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
    }

    public void DoJump(Vector2 _direction, float _velocity)
    {
        rb.velocity = _direction * _velocity;
    }
}
