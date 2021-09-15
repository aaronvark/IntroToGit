using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public AudioSource radioAudio;
    public Transform radio;
    public Transform newRadio;
    private bool hasEaten;

    //Wordt nu aangestuurd door events
    void Start()
    {
        EventSystem.Subscribe(EventType.ENTER_LIVINGROOM, StartRadio);
        EventSystem.Subscribe(EventType.ENTER_KITCHEN, HasEatenHamburger);
    }

    private void StartRadio()
    {
        radioAudio.Play();
        Debug.Log("activated radio");
    }

    private void HasEatenHamburger()
    {
        hasEaten = true;
    }
    
    private void Update()
    {
        if (hasEaten)
        {
            radio.position = Vector3.Lerp(radio.position, newRadio.position, Time.deltaTime);
        }
    }
}
