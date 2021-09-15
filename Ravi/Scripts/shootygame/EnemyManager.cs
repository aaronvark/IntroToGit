using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    protected const float ENEMY_SPEED = 5f;
    protected const int ENEMY_HEALTH = 100;

    public GameObject enemyPrefab;
    private GameObject playerInstance;
    private List<GameObject> enemies = new List<GameObject>();
    private List<int> enemyHealth = new List<int>();

    public void Initialize(GameObject _playerInstance)
    {
        playerInstance = _playerInstance;

        // Spawn some enemies
        for (int i = 0; i < 10; ++i)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().AssignPlayer(playerInstance);
            enemies.Add(enemy);
            enemyHealth.Add(ENEMY_HEALTH);
        }
    }

    public void RemoveEnemy(GameObject _enemy)
    {
        enemies.Remove(_enemy);
    }
}