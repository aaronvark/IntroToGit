using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldStateMachine
{
    public OldState currentState;
    public List<OldState> allStates = new List<OldState>();

    public void GoToState(OldPlayerBehaviour pb, string newstate)
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
        if (stateExists == false)
        {
            Debug.LogWarning("State " + "'" + newstate + "'" + " doesnt exist");
            return;
        }
        if (currentState != null)
        {
            if (currentState.GetType().ToString() == newstate)
            {
                Debug.LogWarning("State " + "'" + newstate + "'" + " is already the current state");
                return;
            }
        }
        if (currentState != null)
        {
            currentState.OnStateExit(pb);
        }
        foreach (var s in allStates)
        {
            if (s.GetType().ToString() == newstate)
            {
                currentState = s;

                pb.ChangeState(currentState);

                s.OnStateEnter(pb);
                Debug.Log("Changed state to " + currentState.GetType().ToString());
                return;
            }
        }
    }

    public OldState CurrentState()
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