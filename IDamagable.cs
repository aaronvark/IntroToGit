using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    float health { get; }
    float healthLimit { get; }
    float damageTimer { get; set; }
    float damageCooldown { get; set; }
    void TakeDamage(float _amount);
   
    event System.Action<float> ON_DAMAGE_TAKEN;
}
