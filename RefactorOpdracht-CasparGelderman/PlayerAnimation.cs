using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sr;
    
    private Rigidbody2D rb;

    void OnEnable()
    {
        rb = Player.instance.GetComponent<Rigidbody2D>();
        animator = Player.instance.GetComponent<Animator>();
        sr = Player.instance.GetComponent<SpriteRenderer>();

        Player.grounded += CheckIfJumping;
        PlayerInput.input += AnimateMovement;
    }

    private void OnDisable()
    {
        Player.grounded -= CheckIfJumping;
    }

    void Update()
    {
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
    }

    void AnimateMovement(float _input)
    {
        animator.SetFloat("Speed", Mathf.Abs(_input));
    }

    void CheckIfJumping(bool _isJumping)
    {
        animator.SetBool("IsJumping", _isJumping);
    }
}
