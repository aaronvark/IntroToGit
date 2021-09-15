using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooty : MonoBehaviour
{
    public float playerSpeed = 10;

    public GunShooty currentGun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed, 0, Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed);

        if (Input.GetMouseButton(0)) currentGun.PullTrigger();

    }
}
