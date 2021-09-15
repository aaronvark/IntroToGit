using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need monoBehaviour?
public class EnemyManager : MonoBehaviour
{
    public GameObject target;
    public GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 10; ++i)
        {
            SpawnEnemy();
            Debug.Log("Enemy" + i);
        }
    }
    // Update is called once per frame

    void Update()
    {
        foreach(GameObject _enemy in enemies)
        {
            //MOVE TO TARGET
            //reference to move in enemy class?
            _enemy.Move(/*player instance target*/);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = GameObject.Instantiate(enemyPrefab);
        enemy.AddComponent<Enemy>();
        enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        enemies.Add(enemy);
        //enemyHealth.Add(ENEMY_HEALTH); IN ENEMY CLASS
    }

 
}
