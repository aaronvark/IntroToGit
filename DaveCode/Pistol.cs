using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Guns
{
    public class Pistol : MonoBehaviour, IGun
    {
    public float damage
    {
        get; set;
    }
    public float fireRate
    {
        get; set;
    }
    public float range
    {
        get; set;
    }
    public float impactForce
    {
        get; set;
    }
    public int clips
    {
        get; set;
    }
    public int maxClips
    {
        get; set;
    }
    public int clipSize
    {
        get; set;
    }
    public int ammo
    {
        get; set;
    }
    public int ammoInUse
    {
        get; set;
    }

    [SerializeField] private Camera cam;

    [SerializeField] private GameObject impactEffect;

    [SerializeField] private ParticleSystem muzzleFlash;

    private bool exec = false;
    private bool _isReloading = false;
    private void Start()
    {
        ammo = clips * clipSize;
        ammoInUse = clipSize;
    }
    public void Shoot()
    {
        if (ammo + ammoInUse <= 0)
            return;
        if (ammoInUse > 0)
        {
            ammoInUse--;
            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                if (hit.transform.tag == "Enemy")
                {
                    var tempEnemy = hit.transform.gameObject;
                    enemyHealth = tempEnemy.GetComponent<EnemyHealth>();
                    enemyHealth.Health -= damage;
                }
                Destroy(impact, 2f);

            }

        }
        else
        {
            Reload();
        }
    }
    public void Reload()
    {
        if (!exec)
        {
            ammo -= clipSize;
            clips--;
            exec = true;
        }
        _isReloading = true;
        ammoInUse = clipSize;
        exec = false;
        _isReloading = false;
    }
}
}

