using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : Actor
{
    public override void Start()
    {
        base.Start();
        SetupStates();
    }

    void SetupStates()
    {
        Locomotion lm = new Locomotion();
        InAir ia = new InAir();

        stateMachine.AddState(lm);
        stateMachine.AddState(ia);

        stateMachine.GoToState("Locomotion");
    }

    public override void Update()
    {
        base.Update();
    }
}
