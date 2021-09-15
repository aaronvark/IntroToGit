using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    [SerializeField] private float life = 5f;
    [SerializeField] private float damage = 5f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, speed * Time.deltaTime))
        {
            if(hitInfo.collider.GetComponent<IDamageble>() != null)
            {
                hitInfo.collider.GetComponent<IDamageble>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            life -= Time.deltaTime;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }
    }

    public void SetDirection(Vector3 _playerPos)
    {
        Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        mousePosition.y = 0;
        transform.position = _playerPos + (mousePosition - _playerPos).normalized;
        transform.LookAt(mousePosition);
    }
}
