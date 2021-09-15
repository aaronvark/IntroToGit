using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamagable
{
    [SerializeField] private float Health;
    public float health
    {
        get
        {
            return Health;
        }
        protected set
        {
            if (value != Health)
            {
                OnHealthChanged?.Invoke(value);
            }
            Health = value;
        }
    }

    [SerializeField] protected float MaxHealth;
    public float maxHealth => MaxHealth;

    public event System.Action<float> OnHealthChanged;

    private void OnEnable()
    {
        health = MaxHealth;
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;
    }
}
