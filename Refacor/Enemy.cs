using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    private int health = 100;
    private float speed = 5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(GameObject _playerInstance)
    {
        transform.Translate((_playerInstance.transform.position - this.transform.position).normalized * Time.deltaTime * speed);
    }

    public void TakeDamage()
    {

    }
}
