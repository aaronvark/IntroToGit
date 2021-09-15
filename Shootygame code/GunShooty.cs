using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooty : MonoBehaviour
{
    private List<GameObject> bullets = new List<GameObject>();
    private List<float> bulletTimers = new List<float>();

    public GameObject bulletPrefab;
    public float bulletSpeed = 25f;
    public float bulletLife = 5f;

    public float AttackSpeed;
    private float refireTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {






    }

    public void PullTrigger()
    {
        refireTime -= Time.deltaTime;
        // Fire gun
        if (refireTime <= 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            mousePosition.y = 0;
            bullet.transform.position = transform.position + (mousePosition - transform.position).normalized;
            bullet.transform.LookAt(mousePosition);
            bullets.Add(bullet);
            bulletTimers.Add(bulletLife);
            refireTime = AttackSpeed;
        }

    }

}
