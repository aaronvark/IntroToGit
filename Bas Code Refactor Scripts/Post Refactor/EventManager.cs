using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    GAME_OVER,
    PLAYER_HIT,
    ENTERED_PORTAL,
    HEALTH_CHANGED,
    MAXHEALTH_CHANGED,
    AMMO_CHANGED,
    MONEY_CHANGED,

}

public static class EventManager 
{

    private static Dictionary<EventType, System.Action> eventDictionary = new Dictionary<EventType, System.Action>();

    public static void Subscribe(EventType _eventType, System.Action _listener)
    {
        if (!eventDictionary.ContainsKey(_eventType))
        {
            eventDictionary.Add(_eventType, null);
        }

        eventDictionary[_eventType] += _listener;
    }

    public static void UnSubscribe(EventType _eventType, System.Action _listener)
    {
        if (eventDictionary.ContainsKey(_eventType))
        {
            System.Action result = eventDictionary[_eventType];
            if (result != null)
            {
                result -= _listener;
            }
        }
    }

    public static void Invoke(EventType _eventType)
    {
        if (eventDictionary.ContainsKey(_eventType))
        {
            eventDictionary[_eventType]?.Invoke();
        }
    }
}

public static class EventManager<T>
{
    private static Dictionary<EventType, System.Action<T>> eventDictionary = new Dictionary<EventType, System.Action<T>>();

    public static void Register(EventType _eventType, System.Action<T> _listener)
    {
        if (!eventDictionary.ContainsKey(_eventType))
        {
            eventDictionary.Add(_eventType, null);
        }
        eventDictionary[_eventType] += _listener;
    }

    public static void UnRegister(EventType _eventType, System.Action<T> _listener)
    {
        if (eventDictionary.ContainsKey(_eventType))
        {
            System.Action<T> result = eventDictionary[_eventType];
            if (result != null)
            {
                result -= _listener;
            }
        }
    }

    public static void Invoke(EventType _eventType, T _argument)
    {
        if (eventDictionary.ContainsKey(_eventType))
        {
            eventDictionary[_eventType]?.Invoke(_argument);
        }
    }


}


