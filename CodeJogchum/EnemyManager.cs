using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    const float ENEMY_SPEED = 5f;
    const int ENEMY_HEALTH = 100;

    private List<GameObject> enemies = new List<GameObject>();
    private List<int> enemyHealth = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        // Spawn some enemies
        for (int i = 0; i < 10; ++i)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            enemies.Add(enemy);
            enemyHealth.Add(ENEMY_HEALTH);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShootyGame.instance.MoveEnemies(enemies, ENEMY_SPEED);
    }
}
