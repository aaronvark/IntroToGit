using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerInstance;
    
    void Start()
    {
        // Spawn some enemies
        for( int i = 0; i < 10; ++i ) {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(-100,100), 0, Random.Range(-100,100));
            enemy.GetComponent<Enemy>().playerInstance = playerInstance;
        }
    }
}
