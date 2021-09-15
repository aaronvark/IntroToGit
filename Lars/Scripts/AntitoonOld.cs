using UnityEngine;

public class Antitoon : Enemy
{
    public GameObject antitoonBody;

    private RaycastHit2D isTouchingLeft;
    private RaycastHit2D isTouchingRight;

    private const float maxMoveSpeed = 3.5f;
    private float currentMoveSpeed = 3.5f;
    private float timeOnGround = 0.0f;
    private const float maxTimeOnGround = 0.15f;
    private float timeBeforeMove = 0;
    private float maxTimeBeforeMove = 0.725f;

    protected override void Reload()
    {
        ResetAntitoon();

        foreach (BoxCollider2D bc2d in GetComponents<BoxCollider2D>())
        {
            bc2d.enabled = true;
        }
        health = maxHealth;
        SR.enabled = true;
        transform.position = spawnPoint;
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        maxHealth = 1;
        health = maxHealth;
        defaultFlip = SR.flipX;
    }
    protected override void OnStart()
    {
        currentMoveSpeed = SR.flipX ? maxMoveSpeed : -maxMoveSpeed;
    }
    private void FixedUpdate()
    {
        TurnOnOff(25, true, spawnPoint);

        if (!isDisabled)
        {
            isTouchingLeft = Physics2D.Linecast(new Vector2(transform.position.x - (GetComponent<BoxCollider2D>().size.x / 2) - 0.2f, transform.position.y - (GetComponent<BoxCollider2D>().size.y / 2)),
                                                transform.position - new Vector3((GetComponent<BoxCollider2D>().size.x / 2) + 0.2f, (GetComponent<BoxCollider2D>().size.y / 2) + 1f, 0), LayerMask.GetMask("Ground"));
            isTouchingRight = Physics2D.Linecast(new Vector2(transform.position.x + (GetComponent<BoxCollider2D>().size.x / 2) + 0.15f, transform.position.y - (GetComponent<BoxCollider2D>().size.y / 2)),
                                                 transform.position - new Vector3(-(GetComponent<BoxCollider2D>().size.x / 2) - 0.15f, (GetComponent<BoxCollider2D>().size.y / 2) + 1f, 0), LayerMask.GetMask("Ground"));
            Debug.DrawLine(new Vector3(transform.position.x - (GetComponent<BoxCollider2D>().size.x / 2) - 0.2f, transform.position.y - (GetComponent<BoxCollider2D>().size.y / 2), transform.position.z),
                           transform.position - new Vector3((GetComponent<BoxCollider2D>().size.x / 2) + 0.2f, (GetComponent<BoxCollider2D>().size.y / 2) + 1f, 0), Color.red);
            Debug.DrawLine(new Vector3(transform.position.x + (GetComponent<BoxCollider2D>().size.x / 2) + 0.15f, transform.position.y - (GetComponent<BoxCollider2D>().size.y / 2), transform.position.z),
                           transform.position - new Vector3(-(GetComponent<BoxCollider2D>().size.x / 2) - 0.15f, (GetComponent<BoxCollider2D>().size.y / 2) + 1f, 0), Color.blue);
            //if antitoon is grounded
            if (isTouchingRight.collider != null || isTouchingLeft.collider != null)
            {
                //move in current direction
                rb2d.velocity = new Vector2(currentMoveSpeed, rb2d.velocity.y);
            }
            //if antitoon reached edge and is alive
            if (isTouchingRight.collider == null && health > 0)
            {
                //turn around
                currentMoveSpeed = -maxMoveSpeed;
                SR.flipX = false;
            }
            //If antitoon reached edge and is alive
            if (isTouchingLeft.collider == null && health > 0)
            {
                //turn around
                currentMoveSpeed = maxMoveSpeed;
                SR.flipX = true;
            }

            if (health <= 0 && Time.time < timeBeforeMove)
            {
                rb2d.velocity = Vector2.zero;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D _coll)
    {
        //Set the timer since the ground was hit
        timeOnGround = Time.time + maxTimeOnGround;
        //If antitoon is colliding with ground and is dead
        if (_coll.gameObject.tag == "Ground" && health <= 0 && Time.time >= timeBeforeMove)
        {
            //bounce the antitoon
            rb2d.AddForce(new Vector2(0, 500));
        }
    }
    private void OnCollisionStay2D(Collision2D _coll)
    {
        //If the time on ground timer expired and the antitoon is still dead and on the ground
        if (Time.time > timeOnGround && _coll.gameObject.tag == "Ground" && health <= 0 && Time.time >= timeBeforeMove)
        {
            //unstuck the antitoon
            rb2d.AddForce(new Vector2(0, 500));
            timeOnGround = Time.time + maxTimeOnGround;
        }
    }
    private void OnTriggerEnter2D(Collider2D _coll)
    {
        OnOnTriggerEnter2D(_coll);
        //If antitoon hits water
        if (_coll.gameObject.tag == "Water")
        {
            SR.enabled = false;
        }
        //If the fist hits the antitoon
        if (_coll.gameObject.tag == "Fist")
        {
            //create a sprite that flies across the screen in the direction the fist was headed
            timeBeforeMove = Time.time + maxTimeBeforeMove;
            GameObject antitoonbody = Instantiate(antitoonBody, transform.position, Quaternion.identity);
            antitoonbody.GetComponent<SpriteRenderer>().flipX = !_coll.gameObject.GetComponent<SpriteRenderer>().flipX;

            if (health <= 0)
            {
                //For every boxcollider on the enemy
                foreach (BoxCollider2D bc2d in GetComponents<BoxCollider2D>())
                {
                    if (bc2d.isTrigger)
                    {
                        bc2d.enabled = false;
                    }
                }
                Anim.SetBool("Dead", true);
            }
        }
    }
    protected override void OnDisableFromCamera()
    {
        base.OnDisableFromCamera();
        ResetAntitoon();
    }
    private void ResetAntitoon()
    {
        Anim.SetBool("Dead", false);
        SR.flipX = defaultFlip;
        currentMoveSpeed = SR.flipX ? maxMoveSpeed : -maxMoveSpeed;
    }
}
