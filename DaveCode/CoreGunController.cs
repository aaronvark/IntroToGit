using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Guns
{
    public class CoreGunController : MonoBehaviour
    {
        private float nextTimeToFire = 0f;
        private IGun currentGun;
        [SerializeField] private Text ammoText;
        [SerializeField] private int ammoInUse => currentGun.ammoInUse;

        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / currentGun.fireRate;
                currentGun.Shoot();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentGun.Reload();
            }
            ammoText.text = ammoInUse.ToString() + "/" + currentGun.ammo.ToString();
        }
    }
}

