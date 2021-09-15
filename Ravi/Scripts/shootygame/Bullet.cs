using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float BULLET_SPEED = 25f;
    const float BULLET_LIFE = 5f;

    private float bulletTimer = BULLET_LIFE;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, BULLET_SPEED * Time.deltaTime))
        {
            IDamagable hitObj = hitInfo.collider.GetComponent<IDamagable>();
            if (hitObj != null)
            {
                hitObj.TakeDamage(1);
                Destroy(gameObject);
            }
        }
        else
        {
            bulletTimer -= Time.deltaTime;
            if (bulletTimer <= 0)
            {
                Destroy(gameObject);
            }
            transform.Translate(transform.forward * BULLET_SPEED * Time.deltaTime, Space.World);
        }
    }
}