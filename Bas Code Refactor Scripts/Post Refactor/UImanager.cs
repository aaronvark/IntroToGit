using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Slider staminaBar;
    public Text damageText;
    public Text deadText;

    public Text ammoText;
    public Text moneyText;
    public Text healthText;

    public Slider playerHealthBar;
    //public Slider playerStartHealth;

    private static UImanager _instance;

    public static UImanager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
        //SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnEnable()
    {
        EventManager<int>.Register(EventType.HEALTH_CHANGED, UpdateHealthUI);
        EventManager<int>.Register(EventType.MAXHEALTH_CHANGED, UpgradeMaxHealth);
        EventManager<int>.Register(EventType.AMMO_CHANGED, UpdateAmmoUI);
        EventManager<int>.Register(EventType.MONEY_CHANGED, UpdateMoneyUI);


    }

    private void OnDisable()
    {
        EventManager<int>.UnRegister(EventType.HEALTH_CHANGED, UpdateHealthUI);
        EventManager<int>.UnRegister(EventType.MAXHEALTH_CHANGED, UpgradeMaxHealth);
        EventManager<int>.UnRegister(EventType.AMMO_CHANGED, UpdateAmmoUI);
        EventManager<int>.UnRegister(EventType.MONEY_CHANGED, UpdateMoneyUI);

    }

    //vervangen met Dedicated Health en Money managers?
    private void Start()
    {
        playerHealthBar.maxValue = GameManager.Instance.playerHealth;
        EventManager<int>.Invoke(EventType.AMMO_CHANGED, 0);
        EventManager<int>.Invoke(EventType.HEALTH_CHANGED, GameManager.Instance.playerHealth);
        EventManager<int>.Invoke(EventType.MONEY_CHANGED, 0);


    }

    private void Update()
    {

    }

    public void ResetUIvalues()
    {

    }

    public void UpdateHealthUI(int _health)
    {
        playerHealthBar.value = _health;
        healthText.text = "HEALTH: " + _health;
    }

    public void UpgradeMaxHealth(int _extraHealth)
    {
        playerHealthBar.maxValue += _extraHealth;
    }

    public void UpdateAmmoUI(int _ammo)
    {
        ammoText.text = "AMMO: " + _ammo; 
    }

    public void UpdateMoneyUI(int _money)
    {
        moneyText.text = "MONEY: " +_money;

    }



}
