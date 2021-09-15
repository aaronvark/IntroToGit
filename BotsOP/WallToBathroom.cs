using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nieuwe class zodat hamburger niet de muur uit hoeft te doen
public class WallToBathroom : MonoBehaviour
{
    void Start()
    {
        EventSystem.Subscribe(EventType.EAT_HAMBURGER, Dissapear);
    }

    private void Dissapear()
    {
        gameObject.SetActive(false);
    }
}
