using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerInstance;
    public GameObject bulletPrefab;

    [SerializeField]
    private EnemyManager enemyManager;

    private float refireTime = 0;

    void Start()
    {
        enemyManager.Initialize(playerInstance);
    }

    void Update()
    {
        // Fire gun
        refireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && refireTime <= 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            mousePosition.y = 0;
            bullet.transform.position = playerInstance.transform.position + (mousePosition - playerInstance.transform.position).normalized;
            bullet.transform.LookAt(mousePosition);
        }
    }
}