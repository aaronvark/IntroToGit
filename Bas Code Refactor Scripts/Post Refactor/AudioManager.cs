using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playerHitSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EventManager.Subscribe(EventType.PLAYER_HIT, PlayHitSound);
    }

    private void OnDisable()
    {
        EventManager.UnSubscribe(EventType.PLAYER_HIT, PlayHitSound);

    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(playerHitSound[UnityEngine.Random.Range(0, playerHitSound.Length)]);

    }
}
