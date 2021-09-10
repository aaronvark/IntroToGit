using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooty : MonoBehaviour
{
    private float bulletTimer = 5f;
    public float bulletSpeed = 25f;
    public int bulletDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletTimer -= Time.deltaTime;
        if (bulletTimer <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime, Space.World);

        RaycastHit hitInfo;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hitInfo, bulletSpeed * Time.deltaTime))
        {
            if (hitInfo.collider.GetComponent<IDamageable>() != null)
            {
                hitInfo.collider.GetComponent<IDamageable>().TakeDamage(bulletDamage);
                Destroy(gameObject);
            }

        }
    }
}

