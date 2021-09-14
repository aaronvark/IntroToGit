using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public static Reload instance;

    public bool isReloading = false;

    public Animator animator;

    private float reloadTime = 1f;

    private Ammo ammo;

    private void Awake()
    {
        instance = this;

        ammo = GetComponent<Ammo>();
    }

    //reload weapon
    public IEnumerator ReloadAnimation()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        ammo.currentAmmo = ammo.maxAmmo;

        isReloading = false;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
}
