using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract float damage{ get; set;}
    public abstract float range{ get; set;}
    public abstract void Attack();
}
