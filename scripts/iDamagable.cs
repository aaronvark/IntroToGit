using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iDamagable
{
    public int ENEMY_HEALTH {get;set;}
    public void hit();
}
