using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    //protected FSM owner;
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
