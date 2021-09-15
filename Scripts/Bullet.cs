using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float BULLET_SPEED = 25f;
    private float BULLET_LIFE = 5f;
    void Update()
    {
        RaycastHit hitInfo;
            if ( Physics.Raycast(transform.position, transform.forward, out hitInfo, BULLET_SPEED * Time.deltaTime)) {
                if(hitInfo.collider.gameObject.GetComponent<iDamagable>() != null){
                    hitInfo.collider.gameObject.GetComponent<iDamagable>().hit();
                }
            }
            else {
                BULLET_LIFE -= Time.deltaTime;
                if ( BULLET_LIFE <= 0 ) {
                    Destroy(gameObject);
                }
                transform.Translate(transform.forward * BULLET_SPEED * Time.deltaTime, Space.World);
            }
    }

    public void Shoot(Vector3 origin){
        Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        mousePosition.y = 0;
        transform.position = origin + ( mousePosition - origin).normalized;
        transform.LookAt(mousePosition);
    }
}
