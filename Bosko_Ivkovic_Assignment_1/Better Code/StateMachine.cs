using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State currentState; // Made this variable private since there already is a function that returns the state
    private List<State> allStates = new List<State>();

    private Actor ownerOfMachine; // Added variable thats the owner

    public StateMachine(Actor owner) // Made a constructor so the owner is set on instantiation
    {
        ownerOfMachine = owner;
    }

    public void GoToState(string newstate)
    {
        bool stateExists = false;
        foreach (var s in allStates)
        {
            if (s.GetType().ToString() == newstate)
            {
                stateExists = true;
                break;
            }
        }
        if (stateExists == false) // if state doesnt exist throw error
        {
            Debug.LogWarning("State " + "'" + newstate + "'" + " doesnt exist");
            return;
        }
        if (currentState != null) // checks if the new state is the same state its already in, if so return
        {
            if (currentState.GetType().ToString() == newstate)
            {
                Debug.LogWarning("State " + "'" + newstate + "'" + " is already the current state");
                return;
            }
        }
        if (currentState != null) // checks if there is a current state, if so then call the OnStateExit Function
        {
            currentState.OnStateExit(ownerOfMachine);
        }
        foreach (var state in allStates)
        {
            if (state.GetType().ToString() == newstate)
            {
                currentState = state;
                
                state.OnStateEnter(ownerOfMachine);
                Debug.Log("Changed state to " + currentState.GetType().ToString());
                return;
            }
        }
    }

    public void AddState(State addedState) // Made function to add states in state machine
    {
        allStates.Add(addedState);
    }

    public State CurrentState()
    {
        return currentState;
    }

    public bool IsInState(string state)
    {
        if (state == currentState.GetType().ToString())
        {
            return true;
        }
        return false;
    }
}