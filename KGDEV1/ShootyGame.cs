using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyGame : MonoBehaviour
{
    private static ShootyGame _instance;
    public static ShootyGame Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    const int ENEMY_HEALTH = 100;

    public GameObject enemyPrefab;
    public GameObject playerInstance;

    private List<GameObject> enemies = new List<GameObject>();
    private List<int> enemyHealth = new List<int>();

    private void Start()
    {
        // Spawn some enemies
        for (int i = 0; i < 10; ++i)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            enemy.GetComponent<enemy>().currentHealth = ENEMY_HEALTH;
            enemies.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
