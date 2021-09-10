using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject graphicPrefab;
    const float speed = 5f;
    const int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToward(Transform target)
    {
        transform.Translate((target.transform.position - transform.position).normalized * Time.deltaTime * speed);
    }
}
