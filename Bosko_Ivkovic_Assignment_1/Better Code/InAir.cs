using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This state is an example and not intended do do anything yet
public class InAir : State
{
    public override void OnStateEnter(Actor actor)
    {
        // Go to fall animation
        // Disable Root Motion
    }

    public override void OnStateExit(Actor actor)
    {
        // Enable Root Motion
    }

    public override void StateUpdate(Actor actor)
    {
        if (actor.GroundHitCheck()) // If actor hits ground go to locomotion statre
        {
            actor.stateMachine.GoToState("Locomotion");
        }
    }

    public override void StateFixedUpdate(Actor actor)
    {
        // Calculate Fall Physics
    }

    public override void StateLateUpdate(Actor actor)
    {

    }
}
