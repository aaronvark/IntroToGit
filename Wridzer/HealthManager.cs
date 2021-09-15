using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageble
{
    [SerializeField] private float maxHealth = 100f;
    public float Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;
        if(Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
