using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon, IUseAmmo
{
    public override float damage { get; set; }
    public override float range { get; set; }
    public float ammoDrain { get; set; }
    
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource shotgunSound;
    
    private PlayerResources playerResc;
    private Camera fpsCam;
    private float spreadSize = 2f;

    private void Start()
    {
        damage = 10f;
        range = 100f;
        ammoDrain = 3f;
        fpsCam = FpsCam.instance.cam;
        playerResc = PlayerResources.instance;
    }
    
    public override void Attack()
    {
        if (playerResc.ammo >= ammoDrain)
        {
            for (int i = 0; i < ammoDrain; i++)
            {
                Shoot();
            }
            playerResc.ammo -= ammoDrain;

            muzzleFlash.Play();
            shotgunSound.Play();
        }
    }
    
    private void Shoot()
    {
        RaycastHit hit;
        Vector3 direction = fpsCam.transform.forward;
        Vector3 spread = direction;
        spread += fpsCam.transform.up * Random.Range(-spreadSize, spreadSize);
        spread += fpsCam.transform.right * Random.Range(-spreadSize, spreadSize);

        direction += spread.normalized * Random.Range(0f, 0.2f);

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            IDamageable target = hit.transform.GetComponent<IDamageable>();
            
            target?.TakeDamage(damage);
            
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 0.5f);
        }
    }
}
