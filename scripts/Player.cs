using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    const float PLAYER_SPEED = 10f;

    private float refireTime = 0;

    [SerializeField] private GameObject bulletPrefab;
    
    void Update()
    {
        // Move player
        transform.Translate( Input.GetAxis("Horizontal") * Time.deltaTime * PLAYER_SPEED, 0, Input.GetAxis("Vertical") * Time.deltaTime * PLAYER_SPEED );
        // Fire gun
        refireTime -= Time.deltaTime;
        if ( Input.GetMouseButton(0) && refireTime <= 0 ) {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.GetComponent<Bullet>().Shoot(transform.position);
        }
    }
}
