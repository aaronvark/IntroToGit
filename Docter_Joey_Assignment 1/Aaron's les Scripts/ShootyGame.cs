using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyGame : MonoBehaviour
{
    public GameObject enemyPrefab;

    private List<GameObject> enemies = new List<GameObject>();
    private List<int> enemyHealth = new List<int>();

    void Start()
    {
        for (int i = 0; i < 10; ++i)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            enemies.Add(enemy);
            enemyHealth.Add(ENEMY_HEALTH);
        }
    }
}
