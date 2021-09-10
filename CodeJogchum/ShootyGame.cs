using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyGame : MonoBehaviour
{
    public static ShootyGame instance;

    const float PLAYER_SPEED = 10f;
    
    public GameObject playerInstance;
    //public GameObject enemyPrefab;
   // public GameObject bulletPrefab;

    
    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //EnemyManager e = new EnemyManager();
    }


    public void MoveEnemies(List<GameObject> _enemies, float _ENEMY_SPEED)
    {
        // Update enemies
        for (int i = 0; i < _enemies.Count; ++i)
        {
            // Move towards player
            _enemies[i].transform.Translate((playerInstance.transform.position - _enemies[i].transform.position).normalized * Time.deltaTime * _ENEMY_SPEED);
        }
    }
    // Update is called once per frame
    void Update()
    {


        // Move player
        playerInstance.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * PLAYER_SPEED, 0, Input.GetAxis("Vertical") * Time.deltaTime * PLAYER_SPEED);

        // Fire gun
        

    }
}
