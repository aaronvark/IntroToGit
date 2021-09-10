using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour, IDamagable
{
    const float ENEMY_SPEED = 5f;
    public int currentHealth;

    // Update is called once per frame
    void Update()
    {
        // Move towards player
        transform.Translate((ShootyGame.Instance.playerInstance.transform.position - transform.position).normalized * Time.deltaTime * ENEMY_SPEED);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth < 1)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
