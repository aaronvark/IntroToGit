using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    const float ENEMY_SPEED = 5f;
    const int ENEMY_HEALTH = 100;

    private Player playerInstance;
    // Start is called before the first frame update
    void Start()
    {
        playerInstance = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate((playerInstance.transform.position - transform.position).normalized * Time.deltaTime * ENEMY_SPEED);
    }

    public void TakeDamage(int damage)
    {
        ENEMY_HEALTH -= damage;
        if(ENEMY_HEALTH <= 0)
        {
            Destroy(gameObject);
        }
    }
}
