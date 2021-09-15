using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int initialEnemyCount = 15;
    public List<GameObject> enemies = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(initialEnemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count <= 2) SpawnEnemies(3);
        
    }

    public void SpawnEnemies(int count)
    {
        // Spawn some enemies
        for (int i = 0; i < (count * ShootyGame.Instance.difficultylevel); ++i)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            enemies.Add(enemy);
        }
    }
}
