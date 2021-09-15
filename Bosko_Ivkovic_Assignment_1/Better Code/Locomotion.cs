using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This state is an example and not intended do do anything yet
public class Locomotion : State
{
    public override void OnStateEnter(Actor actor)
    {
        // Go to locomotion animation
    }

    public override void OnStateExit(Actor actor)
    {

    }
    
    public override void StateUpdate(Actor actor)
    {
        Movement();
        if (!actor.GroundHitCheck()) // If actor does not touch the ground anymore
        {
            actor.stateMachine.GoToState("InAir");
        }
    }

    void Movement()
    {
        // Do Movement
    }
    
    public override void StateFixedUpdate(Actor actor)
    {
        // Do physics calculations
    }

    public override void StateLateUpdate(Actor actor)
    {

    }
}
