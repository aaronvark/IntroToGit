using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    ON_PLAYER_SLAP = 0,
    ON_PLAYER_STOMP = 1,
}

public static class Eventmanager 
{
    static Dictionary<EventType, System.Action> EventDictionary = new Dictionary<EventType, System.Action>();

    public static void Subscribe(EventType _eventType, System.Action _action)
    {
        if (!EventDictionary.ContainsKey(_eventType))
        {
            EventDictionary.Add(_eventType, null);
        }
        EventDictionary.Add(_eventType, _action);
    }

    public static void UnSubscribe(EventType _eventType, System.Action _action)
    {
        if(EventDictionary.ContainsKey(_eventType))
        {
            System.Action result = EventDictionary[_eventType];
            if (result != null)
            {
                result -= _action;
            }
            else
            {
                Debug.LogWarning("Somehting went wrong with event: " + _eventType);
            }
        }
    }

    public static void Invoke(EventType _eventType)
    {
        if (EventDictionary.ContainsKey(_eventType))
        {
            EventDictionary[_eventType]?.Invoke();
        }
    }
}
public static class Eventmanager<T>
{
    static Dictionary<EventType, System.Action<T>> EventDictionary = new Dictionary<EventType, System.Action<T>>();

    public static void Subscribe(EventType _eventType, System.Action<T> _action)
    {
        if (!EventDictionary.ContainsKey(_eventType))
        {
            EventDictionary.Add(_eventType, null);
        }
        EventDictionary.Add(_eventType, _action);
    }

    public static void UnSubscribe(EventType _eventType, System.Action<T> _action)
    {
        if (EventDictionary.ContainsKey(_eventType))
        {
            System.Action<T> result = EventDictionary[_eventType];
            if (result != null)
            {
                result -= _action;
            }
            else
            {
                Debug.LogWarning("Somehting went wrong with event: " + _eventType);
            }
        }
    }

    public static void Invoke(EventType _eventType, T _arg1)
    {
        if (EventDictionary.ContainsKey(_eventType))
        {
            EventDictionary[_eventType]?.Invoke(_arg1);
        }
    }
}