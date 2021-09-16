using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerFSM
{
    public CrawlerOld owner { get; private set; }
    private Dictionary<EnemyStateType, CrawlerState> states;
    public EnemyStateType CurrentStateType { get; private set; }
    private CrawlerState currentState;
    private CrawlerState previousState;

    public void Initialize(CrawlerOld _owner)
    {
        owner = _owner;
        states = new Dictionary<EnemyStateType, CrawlerState>();
    }

    public void AddState(EnemyStateType _newType, CrawlerState _newState)
    {
        states.Add(_newType, _newState);
        states[_newType].Initialize(this);
    }

    public void UpdateState()
    {
        currentState?.Update();
    }

    public void GotoState(EnemyStateType _key)
    {
        if (!states.ContainsKey(_key))
        {
            return;
        }

        currentState?.Exit();

        previousState = currentState;
        CurrentStateType = _key;
        currentState = states[CurrentStateType];

        currentState.Enter();
    }

    public CrawlerState GetState(EnemyStateType _type)
    {
        if (!states.ContainsKey(_type))
        {
            return null;
        }
        return states[_type];
    }
}
