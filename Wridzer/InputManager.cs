using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerInstance;
    private Gun currentWeapon;

    private void Start()
    {
        currentWeapon = FindObjectOfType<Gun>();
    }

    private void Update()
    {
        GetMovement();
        GetShootKey();
    }

    private void GetMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hor, 0f, ver);
        playerInstance.GetComponent<Movement>().Move(movement);
    }

    private void GetShootKey()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.GetComponent<Gun>().TryShoot(playerInstance);
        }
    }
}
