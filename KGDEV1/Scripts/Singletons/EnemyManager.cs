using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    [SerializeField]private GameObject enemyPrefab;
    private List<Enemy> enemies = new List<Enemy>();
    
    
    private void Update()
    {
        Transform playerTransform = PlayerManager.instance.player.transform;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Move(playerTransform);

            if ( enemies[i].health <= 0)
            {
                enemies.RemoveAt(i);
                enemies[i].Die();
            }
        }
    }

    public void AddEnemy(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            
            enemies.Add(newEnemy.GetComponent<Enemy>());
            newEnemy.transform.position = new Vector3(Random.Range(-100,100), 0, Random.Range(-100,100));
        }
    }
}
