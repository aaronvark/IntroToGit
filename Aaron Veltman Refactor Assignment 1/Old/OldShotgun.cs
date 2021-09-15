using UnityEngine;

public class OldShotgun : MonoBehaviour
{
    public int pellets = 3;
    public float spreadSize = 2f;
    public float damage = 10f;
    public float range = 100f;
    public float ammoDrain = 3f;

    public Camera fpsCam;
    public PlayerResources playerResc;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource shotgunSound;

    // drain ammo on shoot and disable fire when ammo is depleted
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerResc.playerAmmo >= ammoDrain)
        {
            for (int i = 0; i < pellets; i++)
            {
                Shoot();
            }
            playerResc.playerAmmo -= ammoDrain;

            muzzleFlash.Play();
            shotgunSound.Play();
        }
    }

    // shoot raycast three times in a random postion inside spreadSize. Damage if object has enemy component
    public void Shoot()
    {
        RaycastHit hit;
        Vector3 direction = fpsCam.transform.forward;
        Vector3 spread = direction;
        spread += fpsCam.transform.up * Random.Range(-spreadSize, spreadSize);
        spread += fpsCam.transform.right * Random.Range(-spreadSize, spreadSize);

        direction += spread.normalized * Random.Range(0f, 0.2f);

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
        }
    }
}
