using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GunManager gunManager;

    void Start()
    {

    }

    private void OnTriggerStay(Collider collision)
    {
        Debug.Log(collision.gameObject);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.GetComponent<IInteractable>() != null)
            {
               collision.gameObject.GetComponent<IInteractable>().Action(gameObject);
            }
        }
    }



    


}
