using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int health = 100;
    public bool isDead = false;

    public StateMachine stateMachine;

    public virtual void Start()
    {
        stateMachine = new StateMachine(this);
    }

    public virtual void Update()
    {
        stateMachine.CurrentState().StateUpdate(this);
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState().StateFixedUpdate(this);
    }

    public virtual void LateUpdate()
    {
        stateMachine.CurrentState().StateLateUpdate(this);
    }

    public bool GroundHitCheck()
    {
        if (true) // If Hit ground (probably check with a Physics.Spherecast)
        {
            return true;
        }
        //return false; Commented out cause it always returns true with that current if statement ^^
    }
}
