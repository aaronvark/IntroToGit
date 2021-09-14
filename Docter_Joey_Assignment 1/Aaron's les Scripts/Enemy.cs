using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const int ENEMY_HEALTH = 100;
    const float ENEMY_SPEED = 5f;

    private GameObject playerInstance;

    void Start()
    {
        playerInstance = FindObjectOfType<Player>().gameObject;
    }

    void Update()
    {
        transform.Translate((playerInstance.transform.position - transform.position).normalized * Time.deltaTime * ENEMY_SPEED);
    }
}
