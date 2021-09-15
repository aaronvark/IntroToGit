using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable
{
    int damage { get; set; }
    void Hit(int _damage);
}
