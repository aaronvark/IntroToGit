using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Player player;

    private List<Bullet> bullets = new List<Bullet>();
    private List<Enemy> enemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemies();
    }

    void SpawnEnemies()
    {
        // Spawn some enemies
        for (int i = 0; i < 10; ++i)
        {
            Enemy enemy = GameObject.Instantiate(enemyPrefab).transform.GetComponent<Enemy>();
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            enemies.Add(enemy);
        }
    }

    void MoveEnemies()
    {
        for (int i = 0; i < enemies.Count; ++i)
        {
            // Move towards player
            enemies[i].MoveToward(player.transform);
        }
    }

}
