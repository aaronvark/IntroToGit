using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooty : MonoBehaviour, IDamageable
{
    public float enemyHealth = 20;
    public float enemySpeed = 5f;

    public int difficultylevel = 1;


    public void TakeDamage(int damage)
    {
        Debug.Log("ouch! " + name + " was hit");
        enemyHealth -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( ( ShootyGame.Instance.playerInstance.transform.position - transform.position ).normalized * Time.deltaTime * enemySpeed );

        if (enemyHealth <= 0) Die();

    }

    private void Die()
    {
        ShootyGame.Instance.killCount++;
        Destroy(gameObject);
    }
}
