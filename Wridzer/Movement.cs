using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;

    public void Move(Vector3 movement)
    {
        transform.Translate(movement * Time.deltaTime * speed);
    }
}
