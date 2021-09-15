using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources instance;
    public float ammo;

    private void Start()
    {
        instance = this;
    }
}
