using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float BULLET_SPEED = 25f;
    const float BULLET_LIFE = 5f;

    public GameObject bulletPrefab;

    private List<GameObject> bullets = new List<GameObject>();
    private List<float> bulletTimers = new List<float>();
    private float refireTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        ShootyGame p = new ShootyGame();
    }

    // Update is called once per frame
    void Update()
    {
        refireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && refireTime <= 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            mousePosition.y = 0;
            bullet.transform.position = playerInstance.transform.position + (mousePosition - playerInstance.transform.position).normalized;
            bullet.transform.LookAt(mousePosition);
            bullets.Add(bullet);
            bulletTimers.Add(BULLET_LIFE);
        }

        // Update bullets
        for (int i = 0; i < bullets.Count; ++i)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(bullets[i].transform.position, bullets[i].transform.forward, out hitInfo, BULLET_SPEED * Time.deltaTime))
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

                GameObject.Destroy(bullets[i]);
                bullets.RemoveAt(i--);
                continue;
            }
            else
            {
                bulletTimers[i] -= Time.deltaTime;
                if (bulletTimers[i] <= 0)
                {
                    GameObject.Destroy(bullets[i]);
                    bullets.RemoveAt(i);
                    bulletTimers.RemoveAt(i--);
                    continue;
                }
                bullets[i].transform.Translate(bullets[i].transform.forward * BULLET_SPEED * Time.deltaTime, Space.World);
            }
        }

    }
}
}
