using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public enum PlayerMoveStates
    {
        Idle, Walking, Dashing
    }

    public PlayerMoveStates playerMoveState;

    public float horSpeed;
    public float vertSpeed;
    public Rigidbody rigidBody;
    public float dashSpeed;
    public float dashLenght;
    public float dashDelay;

    //public bool isVulnurable;

    public int sTPerDash = 20;
    private Vector3 dashDir;

    public float staminaIncrease;
    public float stamina = 100;
    private float staminaTime = 0;
    private float staminaRechargeSpeed = 0.2f;
    //wacky dash timer variables
    private float time = 0.0f;
    private float interpolationPeriod = 1;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.playerDead)
        {
            switch (playerMoveState)
            {
                case PlayerMoveStates.Idle: Idle(); break;
                case PlayerMoveStates.Walking: Move(); break;
                case PlayerMoveStates.Dashing: Dash(dashDir); break;
            }
            if (stamina <= 100)
            {
                RegenStamina();
            }

            float vertTrans = Input.GetAxis("Vertical");
            float horTrans = Input.GetAxis("Horizontal");
            dashDir = new Vector3(horTrans, 0, vertTrans);
        }
    }

    private void Idle()
    {
        rigidBody.velocity = Vector3.zero;
        float vertTrans = Input.GetAxis("Vertical");
        float horTrans = Input.GetAxis("Horizontal");

        if (vertTrans != 0 || horTrans != 0)
        {
            playerMoveState = PlayerMoveStates.Walking;
            //animator.SetTrigger("walkTrigger");
            return;
        }
    }

    void Move()
    {
        float vertTrans = Input.GetAxis("Vertical");
        float horTrans = Input.GetAxis("Horizontal");
        if (vertTrans == 0 && horTrans == 0)
        {
            playerMoveState = PlayerMoveStates.Idle;
            //animator.SetTrigger("idleTrigger");
            return;
        }


        Vector3 vel = new Vector3(dashDir.x * horSpeed, 0, dashDir.z * vertSpeed);
        rigidBody.velocity = vel;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (stamina > sTPerDash)
            {
                stamina -= sTPerDash;
                playerMoveState = PlayerMoveStates.Dashing;
                //animator.SetTrigger("dashTrigger");
            }
        }
    }

    void Dash(Vector3 dir)
    {
        // isVulnurable = false;
        var newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(dir * dashLenght, ForceMode.VelocityChange);

        time += Time.deltaTime;
        if (time >= interpolationPeriod)
        {
            time = 0.0f;
            //isVulnurable = true;
            playerMoveState = PlayerMoveStates.Walking;
            //animator.SetTrigger("walkTrigger");
        }
    }

    private void RegenStamina()
    {
        staminaTime += Time.deltaTime;
        if (staminaTime >= staminaRechargeSpeed)
        {
            staminaTime = 0.0f;
            stamina += staminaIncrease;
        }
    }

}
