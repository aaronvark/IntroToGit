using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;
 
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
    
    public void TakeDamage(float _amount)
    {
        if (playerHealth.damageTimer >= playerHealth.damageCooldown)
        {
            playerHealth.health -= _amount;
        }
    }
}
