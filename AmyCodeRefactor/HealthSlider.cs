using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    private Slider healthBar;
    private void Start(){
        healthBar = GetComponent<Slider>();
        EventSystem.Subscribe(EventType.HealthChanged,SliderUpdate);
    }

    private object SliderUpdate(object _newNumber){
        healthBar.value = (int)_newNumber/100f;
        return null;
    }
}