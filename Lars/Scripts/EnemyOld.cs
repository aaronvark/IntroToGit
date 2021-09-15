using System;

using UnityEngine;

public class Enemy : RayEntity
{
    public Engine.Difficulty difficultyRequirement = Engine.Difficulty.Normal;

    private Engine engine;

    protected SoundManager sounds;

    protected GameObject otherAnim;
    protected SetOtherAnim setOtherAnim;

    protected AudioSource[] effectHandlers;

    protected Rigidbody2D rb2d;
    protected Animator Anim;
    protected SpriteRenderer SR;

    protected Vector2 spawnPoint;

    protected int maxHealth;
    protected int health;

    protected bool defaultFlip;

    public void PlaySoundEffect(SoundManager.SoundEffectNames _effectName)
    {
        if (Vector2.Distance(transform.position, rayman.transform.position) < 25)
        {
            if (!effectHandlers[0].isPlaying)
            {
                sounds.SetSound(_effectName, effectHandlers[0]);
                sounds.PlaySound(effectHandlers[0]);
            }
            //if the first audio handler is not already playing the same effect
            else if (effectHandlers[0].clip != sounds.soundClips[_effectName].effect)
            {
                sounds.SetSound(_effectName, effectHandlers[1]);
                sounds.PlaySound(effectHandlers[1]);
            }
        }
    }
    protected virtual void Reload() { }
    protected override void OnAwake()
    {
        engine = GameObject.Find("Engine").GetComponent<Engine>();

        if(difficultyRequirement == Engine.Difficulty.None)
		{
            Debug.LogWarning("Enemy: " + gameObject.name + " does not have a set difficulty") ;
		}

        sounds = GameObject.Find("AudioSystem").GetComponent<SoundManager>();
        effectHandlers = GetComponents<AudioSource>();
        //If the object has children (extra animation)
        if (transform.childCount > 0)
        {
            //find extra animation in object and set it to false
            otherAnim = transform.GetChild(0).gameObject;
            setOtherAnim = otherAnim.GetComponent<SetOtherAnim>();
            otherAnim.SetActive(false);
        }

        if (null != GetComponent<Rigidbody2D>())
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        Anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();

        spawnPoint = transform.position;

        if (engine.currentsaveGame.difficulty < difficultyRequirement)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LevelManager.reload.AddListener(Reload);

        OnStart();
    }
    protected virtual void OnStart() { }

    private void OnEnable()
    {
        if (null != setOtherAnim && Vector2.Distance(transform.position, rayman.transform.position) < 25 && !name.Contains("(Clone)"))
        {
            setOtherAnim.StartAnim(3, SoundManager.SoundEffectNames.otherObjectAppear);
        }
    }
    /// <summary>
    /// Add anything here that should be specific to the object
    /// </summary>
    protected virtual void OnOnTriggerEnter2D(Collider2D _coll)
    {
        //If the enemy hits water
        if (_coll.gameObject.tag == "Water")
        {
            //play water splash
            setOtherAnim.StartAnim(2, SoundManager.SoundEffectNames.otherWaterSplash);
        }
        //If fist hits the enemy
        if (_coll.gameObject.tag == "Fist")
        {
            //remove health according to fist damage
            health -= _coll.gameObject.GetComponent<RayFist>().damage;
        }
    }
    protected override void OnEnableFromCamera()
    {
        SR.enabled = true;
        Anim.enabled = true;
        if (null != rb2d)
        {
            rb2d.simulated = true;
        }
        //for every boxcollider on the enemy
        foreach (BoxCollider2D bc2d in gameObject.GetComponents<BoxCollider2D>())
        {
            //turn it on
            bc2d.enabled = true;
        }
        isDisabled = false;
    }
    protected override void OnDisableFromCamera()
    {
        SR.enabled = false;
        Anim.enabled = false;
        //for every boxcollider on the enemy
        foreach (BoxCollider2D bc2d in gameObject.GetComponents<BoxCollider2D>())
        {
            //turn it on
            bc2d.enabled = false;
        }
        //reset enemy
        transform.position = spawnPoint;

        if (null != rb2d)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.simulated = false;
        }
        health = maxHealth;

        isDisabled = true;
    }
}
