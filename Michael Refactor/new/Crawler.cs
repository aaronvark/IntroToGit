using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Enemy
{
    public Crawler() : base()
    {
        fsm.AddState(new EnemyIdleState(fsm));
        fsm.AddState(new EnemyChaseState(fsm));
        fsm.SwitchState(typeof(EnemyIdleState));
    }
}
