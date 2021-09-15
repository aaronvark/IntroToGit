using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : IDamagable, IRegenable
{
    public float health
    {
        get
        {
            return health;
        }
        set
        {
            if(value != health)
            {
                ON_DAMAGE_TAKEN?.Invoke(value);
            }
            health = value;
        }
    }

    public float healthLimit { get; }
    public float damageTimer { get; set; }

    public float regeneration { get; set; }
    public float regenerationTimer { get; set; }
    public float damageCooldown { get; set; }

    public event System.Action<float> ON_DAMAGE_TAKEN;

    void Start()
    {
        health = healthLimit;
    }


    void Update()
    {
        damageTimer = damageTimer + Time.deltaTime;

        if (damageTimer >= regenerationTimer && health < healthLimit)
        {
            health += regeneration;
        }
    }

    public virtual void TakeDamage(float _amount)
    {
        health -= _amount;
    }

    protected virtual void Die()
    {
        Debug.Log("YOU DIED!");
    }
   
}
