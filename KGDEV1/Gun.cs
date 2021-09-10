using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;

    const float BULLET_SPEED = 25f;
    const float BULLET_LIFE = 5f;
    private float refireTime = 0;

    private List<GameObject> bullets = new List<GameObject>();
    private List<float> bulletTimers = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Fire gun
        refireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && refireTime <= 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            mousePosition.y = 0;
            bullet.transform.position = ShootyGame.Instance.playerInstance.transform.position + (mousePosition - ShootyGame.Instance.playerInstance.transform.position).normalized;
            bullet.transform.LookAt(mousePosition);
            bullets.Add(bullet);
            bulletTimers.Add(BULLET_LIFE);
        }
    }
}
