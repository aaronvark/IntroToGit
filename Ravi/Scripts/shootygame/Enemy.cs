using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    protected const float ENEMY_SPEED = 5f;
    protected const int ENEMY_HEALTH = 100;

    private GameObject playerInstance;
    private int myHealth;

    void Start()
    {
        myHealth = ENEMY_HEALTH;
    }

    public void AssignPlayer(GameObject _playerInstance)
    {
        playerInstance = _playerInstance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((playerInstance.transform.position - transform.position).normalized * Time.deltaTime * ENEMY_SPEED);
    }

    public void TakeDamage(int damage)
    {
        myHealth -= damage;
    }
}