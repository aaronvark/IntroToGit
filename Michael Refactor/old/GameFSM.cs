using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM
{
    public GameManager owner { get; private set; }
    private Dictionary<GameStateType, GameState> states;
    public GameStateType CurrentStateType { get; private set; }
    private GameState currentState;
    private GameState previousState;

    public void Initialize(GameManager _owner)
    {
        owner = _owner;
        states = new Dictionary<GameStateType, GameState>();
    }

    public void AddState(GameStateType _newType, GameState _newState)
    {
        states.Add(_newType, _newState);
        //states[_newType].Initialize(this);
    }

    public void UpdateState()
    {
        currentState?.OnUpdate();
    }

    public void GotoState(GameStateType _key)
    {
        if (!states.ContainsKey(_key))
        {
            return;
        }

        //currentState?.Exit();

        previousState = currentState;
        CurrentStateType = _key;
        currentState = states[CurrentStateType];

        currentState.OnEnter();
    }

    public GameState GetState(GameStateType _type)
    {
        if (!states.ContainsKey(_type))
        {
            return null;
        }
        return states[_type];
    }
}
