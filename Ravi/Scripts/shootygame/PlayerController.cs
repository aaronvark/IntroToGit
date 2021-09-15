using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float PLAYER_SPEED = 10f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * PLAYER_SPEED;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * PLAYER_SPEED;
        transform.Translate(x, 0, z);
    }
}