using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _health = 100; //health value
    public int health{ //custom setter voor het aanzetten van gameover scherm
        get{return _health;}
        set{
            EventSystem.RaiseEvent(EventType.HealthChanged,value);
            if(value <= 0){
                Time.timeScale = 0;
                //gameOver.SetActive(true);
                //Cursor.lockState = CursorLockMode.None;
            }
            _health = value;}
    }
}