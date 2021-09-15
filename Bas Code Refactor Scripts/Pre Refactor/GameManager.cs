using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider staminaBar;
    public Text damageText;
    public Text ammoText;
    public Text deadText;
    public int money;
    public Text moneyText;
    public float timeScale;

    public int dungeonLayer;
    //public TestGun gun;

    public GameObject playerObject;
    public bool playerDead;
    public float playerHealth = 100;
    public Slider playerHealthBar;
    public Slider playeStartHealth;
    public Text hpText;

    private float timeElapsed;
    public float lerpDuration;

    public Animator quadAnimator;

    public AudioSource audioSource;
    public AudioClip gameOverSound;
    public AudioClip enterPortalSound;
    public AudioClip exitPortalSound;
    public AudioClip heartPickupSound;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerHealthBar.maxValue = playerHealth;
        playerHealthBar.value = playerHealth;
        audioSource = GetComponent<AudioSource>();

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
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }


    private void FixedUpdate()
    {
        playerHealthBar.value = playerHealth;
        hpText.text = "HEALTH: " + playerHealth;

        moneyText.text = "GOLD: " + money;
        if(playerDead && Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
        }

        if(playerHealth <= 0 && !playerDead)
        {
            EventManager.Invoke(EventManager.EventKind.GAME_OVER);
            StartCoroutine(PlayerDead());
        }
    }


    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //DungeonGeneration.DungeonGenerator.Instance.GenerateDungeon();
        //maak UI en link deze shit eraan
        //moneyText = GameObject.Find("Score").GetComponent<Text>();
        //deadText = GameObject.Find("restarttext").GetComponent<Text>();
        //damageText = GameObject.Find("DamageText").GetComponent<Text>();
        //staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        //playerHealthbar = GameObject.Find("HealthBar").GetComponent<Slider>();

    }

    public IEnumerator PlayerDead()
    {
        playerDead = true;
        quadAnimator.SetTrigger("playerDead");
        audioSource.PlayOneShot(gameOverSound);

        yield return new WaitForSeconds(3f);
        
    }

    public void ResetGame()
    {
        DungeonGeneration.DungeonGenerator.Instance.ResetDungeon();
        dungeonLayer = 1;
        if (GunManager.Instance.currentGun)
        {
            GunManager.Instance.currentGun.DropWeapon();
            GunManager.Instance.currentGun = null;
        }

        playerDead = false;
        playerHealthBar.maxValue = 100;
        money = 0;
        playerHealth = playerHealthBar.maxValue;
        quadAnimator.SetTrigger("resetAnim");
        SceneManager.LoadScene(1);


    }



}
