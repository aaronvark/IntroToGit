using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour, IInteractable
{
    public string _displayText;
    public Animator PlayerAnimator;
    
    //Alweer fridge class weet niet wat hij aanpast als je ermee interact
    public void Interact()
    {
        EventSystem.RaiseEvent(EventType.ENTER_KITCHEN);
        PlayerAnimator.SetBool("IsActive", true);
        EventSystem.RaiseEvent(EventType.NEXT_TASK);
        gameObject.layer = 0;
        Destroy(this);
    }
    public string displayText()
    {
        return _displayText;
    }
}
