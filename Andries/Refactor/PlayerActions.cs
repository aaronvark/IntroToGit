//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerActions : MonoBehaviour
//{
//    public float range;
//    public float currentHealth;
//    public float maxHealth;
//    public float damage;
//    public float stompRange;
//    public float distance = .5f;
//    public Animator Moves;

//    public GameObject RightHand;
//    public GameObject LeftHand;
//    public GameObject StompRange;
//    public Slider slider;
//    public Text Healthtext;

//    private float slapcooldown;
//    private float stompcooldown;
//    private float slapMultiplier;
//    private GameManager gameManager;

//    private void Start() {
//        gameManager = GameManagerHandler.gameManager.GetComponent<GameManager>();

//        if (gameManager.saveData.currentHealth == 0) {
//            Health = maxHealth;
//            currentHealth = maxHealth;
//            slider.value = 150;
//        }
//        else {
//            Health = gameManager.saveData.currentHealth;
//            slider.value = Health;
//        }
//    }

//    private void Update() {
//        SlapAttack();
//        StompAttack();

//        gameManager.saveData.currentHealth = Mathf.RoundToInt(Health);
//        slider.maxValue = maxHealth + (gameManager.saveData.healthIncrease * 10);

//        Healthtext.text = Mathf.RoundToInt(Health).ToString() + " / " + slider.maxValue;

//        if(Health < 1) {
//            gameManager.EndGame();
//        }
//    }

//    private void SlapAttack() {
//        if (Input.GetButton("Fire1") && slapcooldown < 1) {
//            StartCoroutine(Slap(RightHand));

//            slapMultiplier = 1.5f;
//            slapcooldown = 2;

//            Slap();
//        }

//        if (Input.GetButton("Fire1") && slapMultiplier > 0 && slapMultiplier < .7) {
//            StartCoroutine(Slap(LeftHand));

//            slapcooldown = 3;

//            Slap();
//        }

//        if (slapcooldown > 0) slapcooldown -= Time.deltaTime * 5;
//        if (slapMultiplier > 0) slapMultiplier -=  Time.deltaTime * 5;
//    }

//    public void Slap() {
//        if (Physics.Raycast(transform.position, transform.forward, out var hit, range)) {
//            if (hit.transform.CompareTag("Enemy")) {
//                var target = hit.transform.GetComponent<Enemy>();

//                float dealt = Random.Range(damage + gameManager.saveData.damageIncrease, damage + gameManager.saveData.damageIncrease * 2);
//                target.Slapped(dealt);
//            }
//        }
//    }

//    private void StompAttack() {
//        if (Input.GetButton("Fire2") && stompcooldown < 1) {
//            StartCoroutine(Stomp());

//            stompcooldown = 5;
//        }
//        if (stompcooldown > 0) stompcooldown -= Time.deltaTime;
//    }

//    public void GotHit(float Damage) {
//        Health -= Damage;
//    }

//    public float Health {
//        get { return currentHealth; }
//        set {
//            currentHealth = value;
//            slider.value = currentHealth;
//        }
//    }

//    IEnumerator Slap(GameObject ObjHand) {
//        yield return new WaitForSeconds(.05f);

//        ObjHand.SetActive(true);

//        yield return new WaitForSeconds(.05f);

//        ObjHand.SetActive(false);
//    }

//    IEnumerator Stomp() {
//        Moves.SetTrigger("Stomp");

//        yield return new WaitForSeconds(.4f);

//        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stompRange);
//        for (int i = 0; i < hitColliders.Length; i++) {
//            if (hitColliders[i].transform.CompareTag("Enemy")) {
//                var target = hitColliders[i].transform.GetComponent<Enemy>();

//                float dealt = Random.Range(damage * 1.72f - gameManager.saveData.damageIncrease, damage * 1.72f + gameManager.saveData.damageIncrease);
//                target.Stomped(dealt);
//            }
//        }

//        StompRange.SetActive(true);

//        yield return new WaitForSeconds(.2f);

//        StompRange.SetActive(false);
//    }
//}
