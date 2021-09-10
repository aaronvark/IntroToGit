using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health { get; set; }
    [SerializeField]private float speed;

    public void Move(Transform target)
    {
        transform.Translate( ( target.position - transform.position ).normalized * Time.deltaTime * speed );
    }

    public void TakeDamage()
    {
        health -= 1;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
