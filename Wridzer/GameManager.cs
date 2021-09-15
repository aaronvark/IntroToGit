using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerInstance;
    [SerializeField] private float numberOfEnims = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfEnims; ++i)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemy.GetComponent<EnemyFollow>().GetPlayer(playerInstance);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        }
    }

}
