using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float BULLET_SPEED = 25f;

    [SerializeField] private GameObject gameManager;
    private ShootyGame shootyGame;
    void Start()
    {
        shootyGame = gameManager.GetComponent<ShootyGame>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < shootyGame.bullets.Count; ++i)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(shootyGame.bullets[i].transform.position, shootyGame.bullets[i].transform.forward, out hitInfo, BULLET_SPEED * Time.deltaTime))
            {

                for (int j = 0; j < shootyGame.enemies.Count; ++j)
                {
                    if (shootyGame.enemies[j] == hitInfo.collider.gameObject)
                    {
                        shootyGame.enemyHealth[j] -= 1;
                        if (shootyGame.enemyHealth[j] <= 0)
                        {
                            shootyGame.enemies.RemoveAt(j);
                            shootyGame.enemyHealth.RemoveAt(j);
                        }
                        break;
                    }
                }

                GameObject.Destroy(shootyGame.bullets[i]);
                shootyGame.bullets.RemoveAt(i--);
                continue;
            }
            else
            {
                shootyGame.bulletTimers[i] -= Time.deltaTime;
                if (shootyGame.bulletTimers[i] <= 0)
                {
                    GameObject.Destroy(shootyGame.bullets[i]);
                    shootyGame.bullets.RemoveAt(i);
                    shootyGame.bulletTimers.RemoveAt(i--);
                    continue;
                }
                shootyGame.bullets[i].transform.Translate(shootyGame.bullets[i].transform.forward * BULLET_SPEED * Time.deltaTime, Space.World);
            }
        }
    }
}
