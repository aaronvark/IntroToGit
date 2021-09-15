using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float BULLET_SPEED = 25f;
    const float BULLET_LIFE = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, BULLET_LIFE);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bullets.Count; ++i)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(bullets[i].transform.position, bullets[i].transform.forward, out hitInfo, BULLET_SPEED * Time.deltaTime))
            {
                if(hitInfo.collider.gameObject.GetComponent<IDamagable>() != null)
                {
                    hitInfo.collider.gameObject.GetComponent<IDamagable>().TakeDamage(1);
                }
            }
        }
        transform.Translate(transform.forward * BULLET_SPEED * Time.deltaTime, Space.World);
    }
}
