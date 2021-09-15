using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamburger : MonoBehaviour, IInteractable
{
    public GameObject[] hamburgerRend;
    public string _displayText;
    public AudioSource audio;
    private bool hasEaten;

    private void Start()
    {
        EventSystem.Subscribe(EventType.ENTER_LIVINGROOM, StartHamburger);
    }

    private void StartHamburger()
    {
        foreach (var hamburger in hamburgerRend)
        {
            hamburger.SetActive(true);
        }
    }
    // Eerst heel veel FindObjectOfType nu alleen maar events
    // Heb transform.getchild weggehaalt
    // Heb code voor radio naar de radio class verplaatst
    public void Interact()
    {
        if (!hasEaten)
        {
            hasEaten = true;
            EventSystem.RaiseEvent(EventType.NEXT_TASK);
            EventSystem.RaiseEvent(EventType.EAT_HAMBURGER);
            audio.Play();
            
            foreach (var hamburger in hamburgerRend)
            {
                hamburger.SetActive(false);
            }
            gameObject.layer = 0;
        }
    }
    
    public string displayText()
    {
        return _displayText;
    }
}
