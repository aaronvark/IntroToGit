using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum States {Idle,Walking, Dashing }

public class PlayerController : MonoBehaviour, IDamageable
{
    public States state;
    public float horSpeed;
    public float vertSpeed;
    public Rigidbody rigidBody;
    public GameObject playerObject;
    public float dashSpeed;
    public float dashLenght;
    public float dashDelay;

    Vector3 playerScreenSpace;
    //public bool isVulnurable;

    public int sTPerDash = 20;
    public float staminaIncrease;
    public float stamina = 100;
    private float staminaTime = 0;
    private float staminaRechargeSpeed = 0.2f;

    public Animator animator;
    private float time = 0.0f;
    public float interpolationPeriod = 1;

    public SpriteRenderer spriteRenderer;
    public Vector3 dashDir;

    public Animator quadAnimator;

    public AudioSource audioSource;
    public AudioClip[] playerHitSound;

    public GameObject hitIndicatorCamera;

    public GunManager gunManager;

    void Awake()
    {

    }

    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!GameManager.Instance.playerDead)
        {

                
            switch (state)
            {
                case States.Idle: Idle(); break;
                case States.Walking: Move(); break;
                case States.Dashing: Dash(dashDir); break;
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

    private void RegenStamina()
    {
        staminaTime += Time.deltaTime;
        if (staminaTime >= staminaRechargeSpeed)
        {
            staminaTime = 0.0f;
            stamina += staminaIncrease;
        }
    }



    private IEnumerator indicateDamage()
    {
        hitIndicatorCamera.SetActive(true);
        yield return new WaitForSeconds(0.09f);
        hitIndicatorCamera.SetActive(false);

    }

    private void Idle()
    {
        rigidBody.velocity = Vector3.zero;
        float vertTrans = Input.GetAxis("Vertical");
        float horTrans = Input.GetAxis("Horizontal");

        if(vertTrans != 0 || horTrans != 0)
        {
            state = States.Walking;
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
            state = States.Idle;
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
                state = States.Dashing;
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
            state = States.Walking;
            //animator.SetTrigger("walkTrigger");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //waarom staat deze code hier?? fix IDamageable
        if (collision.gameObject.GetComponent<Bullet>() && collision.gameObject.CompareTag("EnemyBullet"))
        {
            audioSource.PlayOneShot(playerHitSound[UnityEngine.Random.Range(0, playerHitSound.Length)]);
            StartCoroutine(indicateDamage());

           GameManager.Instance.playerHealth -= collision.gameObject.GetComponent<Bullet>().bulletDamage;
            GameManager.instance.playerHealthText.text = "HEALTH :" + GameManager.instance.playerHealth;

        }
    }

    public void TakeDamage(int damage)
    {
        //if (!GameManager.Instance.playerDead)
        //{
        audioSource.PlayOneShot(playerHitSound[UnityEngine.Random.Range(0, playerHitSound.Length)]);
        StartCoroutine(indicateDamage());

        Debug.Log("player Damaged");
        GameManager.Instance.playerHealth -= damage;
        GameManager.instance.playerHealthText.text = "HEALTH :" + GameManager.instance.playerHealth;


        //}
    }


    private void OnTriggerStay(Collider collision)
    {
        Debug.Log(collision.gameObject);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.GetComponent<IInteractable>() != null)
            {
               collision.gameObject.GetComponent<IInteractable>().Action(gameObject);
            }
        }
    }



    


}
