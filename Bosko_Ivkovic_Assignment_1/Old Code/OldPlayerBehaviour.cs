using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerBehaviour : MonoBehaviour
{
    OldState currentState;

    OldStateMachine stateMachine;
    
    private void Start()
    {
        stateMachine = new OldStateMachine();
        SetupStates();
    }

    void SetupStates()
    {
        OldExampleState es = new OldExampleState();

        stateMachine.allStates.Add(es);

        stateMachine.GoToState(this , "ExampleState");
    }

    private void Update()
    {
        currentState.StateUpdate(this);
    }

    public void ChangeState(OldState newState)
    {
        currentState = newState;
    }

    public void GetState()
    {
        stateMachine.CurrentState();
    }

}
