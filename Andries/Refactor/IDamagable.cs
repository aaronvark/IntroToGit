using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float health { get; }
    float maxHealth { get; }
    public void TakeDamage(float _damage);
    event System.Action<float> OnHealthChanged;
}