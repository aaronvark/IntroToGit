using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text healthtext;
    [SerializeField] private IDamagable healthComponent;

    private void OnEnable()
    {
        healthComponent = GetComponent<IDamagable>();
        healthComponent.OnHealthChanged += UpdateUI;
    }
    private void OnDisable()
    {
        healthComponent.OnHealthChanged -= UpdateUI;
    }

    public void UpdateUI(float _newValue)
    {
        healthSlider.value = _newValue / healthComponent.maxHealth;
        healthtext.text = Mathf.RoundToInt(_newValue).ToString() + " / " + healthComponent.maxHealth;
    }
}
