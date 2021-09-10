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
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        Vector3 newPos = new Vector3(horizontalAxis, 0, verticalAxis) * Time.deltaTime * PLAYER_SPEED;
        transform.Translate(newPos);
    }

    
}
