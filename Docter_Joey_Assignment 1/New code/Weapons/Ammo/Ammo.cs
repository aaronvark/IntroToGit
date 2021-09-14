using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Ammo : Reload
{
    public float maxAmmo;
    public float currentAmmo;

    public TextMeshProUGUI ammoText;

    //display ammo
    public void AmmoDisplay()
    {
        ammoText.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }
}
