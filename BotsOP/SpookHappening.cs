using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookHappening : MonoBehaviour
{
    //Roept nu alleen maar een event aan inplaats van dat die weet wat die moet aanpassen
    void OnTriggerEnter()
    {
        EventSystem.RaiseEvent(EventType.ENTER_LIVINGROOM);
        Destroy(this);
    }
}
