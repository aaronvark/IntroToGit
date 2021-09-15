using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthBad : MonoBehaviour
{
    public static PlayerHealthBad instance;
    public GameObject DeadScreen;

    private void Awake()
    {
        instance = this;
    }

    public float health = 50f;
    public float healthLimit;
    public Slider healthBar;

    public float damageCooldown = 3;
    public float damageTimer;
    public float regeneration = 2;
    public float regenerationTimer = 10;

    private void Start()
    {
        healthLimit = health;
    }

    private void Update()
    {
        damageTimer = damageTimer + Time.deltaTime;

        if (damageTimer >= regenerationTimer && health < healthLimit)
        {
            health += regeneration;
            healthBar.value = health;

            if (health >= healthLimit)
            {
                health = healthLimit;
            }
        }
    }

    public void increaseHealth(int HPUpgrade)
    {
        healthLimit = healthLimit + HPUpgrade;
        Debug.Log("Healt Limit is:" + healthLimit);
        healthBar.maxValue = healthLimit;
    }

    public void TakeDamage(float amount)
    {
        if (damageTimer >= damageCooldown)
        {
            health -= amount;
            healthBar.value = health;
            damageTimer = 0;
            if (health <= 0f)
            {
                Die();
            }
        }



    }

    void Die()
    {
        Debug.Log("You Died!");
        DeadScreen.SetActive(true);
        Time.timeScale = 0f;
        //PauseMenu.GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }
}

