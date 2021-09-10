using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float PLAYER_SPEED = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move player
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * PLAYER_SPEED, 0, Input.GetAxis("Vertical") * Time.deltaTime * PLAYER_SPEED);
    }
}
