using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IAttack
{
    const float BULLET_LIFE = 5f;

    public GameObject bulletPrefab;
    private Player playerInstance;

    public List<GameObject> bullets = new List<GameObject>();
    public List<float> bulletTimers = new List<float>();

    private float refireTime = 0;


    void Start()
    {
        playerInstance = FindObjectOfType<Player>();
    }

    void Update()
    {
        refireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && refireTime <= 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            mousePosition.y = 0;
            bullet.transform.position = playerInstance.transform.position + (mousePosition - playerInstance.transform.position).normalized;
            bullet.transform.LookAt(mousePosition);
            bullets.Add(bullet);
            bulletTimers.Add(BULLET_LIFE);
        }
    }
}
