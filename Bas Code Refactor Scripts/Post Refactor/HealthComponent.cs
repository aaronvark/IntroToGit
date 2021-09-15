using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        if (!GameManager.Instance.playerDead)
        {
            EventManager.Invoke(EventType.PLAYER_HIT);
            EventManager<int>.Invoke(EventType.HEALTH_CHANGED, GameManager.Instance.playerHealth);
            //health later nog verplaatsen, anders breekt alles
            GameManager.Instance.playerHealth -= damage;
            Debug.Log("player Damaged");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
