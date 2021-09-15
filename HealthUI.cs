using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private IDamagable PlayerHealth;

    private void OnEnable()
    {
        PlayerHealth = GetComponent<IDamagable>();
        PlayerHealth.ON_DAMAGE_TAKEN += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerHealth.ON_DAMAGE_TAKEN -= UpdateUI;
    }

    private void UpdateUI(float _value)
    {
        healthBar.value = _value / PlayerHealth.healthLimit;
    }
}
