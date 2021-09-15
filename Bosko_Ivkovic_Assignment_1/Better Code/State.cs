using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public virtual void OnStateEnter(Actor actor){}

    public virtual void OnStateExit(Actor actor){}

    public virtual void StateUpdate(Actor actor){}

    public virtual void StateLateUpdate(Actor actor){}

    public virtual void StateFixedUpdate(Actor actor){}
}