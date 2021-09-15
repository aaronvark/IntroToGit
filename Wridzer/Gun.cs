using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float timeToFire = 0.1f;
    private float fireTimer;
    public GameObject bulletPrefab;
    private GameObject playerInstance;

    // Start is called before the first frame update
    void Start()
    {
        fireTimer = timeToFire;
    }

    // Update is called once per frame
    void Update()
    {
        timeToFire -= Time.deltaTime;
    }

    public void TryShoot(GameObject _playerInstance)
    {
        if(timeToFire <= 0)
        {
            playerInstance = _playerInstance;
            Fire();
            fireTimer = timeToFire;
        }
    }

    private void Fire()
    {
        Vector3 playerPos = playerInstance.transform.position;
        GameObject bullet = GameObject.Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().SetDirection(playerPos);
    }
}
