using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, iDamagable
{

    const float ENEMY_SPEED = 5f;
    private int enemy_Health = 100;
    public Transform playerInstance;

    public int ENEMY_HEALTH {
        get{
            return enemy_Health;
        }

        set{
            enemy_Health = value;
        }
    }
    
    public void hit(){
        ENEMY_HEALTH -= 1;
        if(enemy_Health <= 0){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( ( playerInstance.transform.position - transform.position ).normalized * Time.deltaTime * ENEMY_SPEED );
    }
}
