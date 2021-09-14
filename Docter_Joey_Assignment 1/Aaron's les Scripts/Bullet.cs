using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float BULLET_SPEED = 25f;

    private Gun gunBullet;

    private List<GameObject> enemies = new List<GameObject>();
    private List<int> enemyHealth = new List<int>();

    void Start()
    {
        gunBullet = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {

        // Update bullets
        for (int i = 0; i < gunBullet.bullets.Count; ++i)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(gunBullet.bullets[i].transform.position, gunBullet.bullets[i].transform.forward, out hitInfo, BULLET_SPEED * Time.deltaTime))
            {
                for (int j = 0; j < enemies.Count; ++j)
                {
                    if (enemies[j] == hitInfo.collider.gameObject)
                    {
                        enemyHealth[j] -= 1;
                        if (enemyHealth[j] <= 0)
                        {
                            enemies.RemoveAt(j);
                            enemyHealth.RemoveAt(j);
                        }
                        break;
                    }
                }

                GameObject.Destroy(gunBullet.bullets[i]);
                gunBullet.bullets.RemoveAt(i--);
                continue;
            }
            else
            {
                gunBullet.bulletTimers[i] -= Time.deltaTime;
                if (gunBullet.bulletTimers[i] <= 0)
                {
                    GameObject.Destroy(gunBullet.bullets[i]);
                    gunBullet.bullets.RemoveAt(i);
                    gunBullet.bulletTimers.RemoveAt(i--);
                    continue;
                }
                gunBullet.bullets[i].transform.Translate(gunBullet.bullets[i].transform.forward * BULLET_SPEED * Time.deltaTime, Space.World);
            }
        }
    }
}
