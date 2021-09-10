using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    const float BULLET_SPEED = 25f;
    public float currentTimer;

    // Update is called once per frame
    void Update()
    {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, BULLET_SPEED * Time.deltaTime))
            {
                IDamagable damagable = hitInfo.collider.gameObject.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(1);
                }


                //for (int j = 0; j < enemies.Count; ++j)
                //{
                //    if (enemies[j] == hitInfo.collider.gameObject)
                //    {
                //        enemyHealth[j] -= 1;
                //        if (enemyHealth[j] <= 0)
                //        {
                //            enemies.RemoveAt(j);
                //            enemyHealth.RemoveAt(j);
                //        }
                //        break;
                //    }
                //}

                GameObject.Destroy(this);
            }

            else
            {
                currentTimer -= Time.deltaTime;
                if (currentTimer <= 0)
                {
                    GameObject.Destroy(this);
                }
                transform.Translate(transform.forward * BULLET_SPEED * Time.deltaTime, Space.World);
            }
    }
}
