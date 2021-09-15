using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Consider generating this from an string to enum tool for easy editing?
public enum EventType
{
    //ADD STUFF
    ON_DAMAGE_TAKEN = 0,
}


public static class EventSystem
{
    private static Dictionary<EventType, System.Action> eventDictionary = new Dictionary<EventType, System.Action>();

    public static void Register(EventType _eventType, System.Action _listener)
    {
        if (!eventDictionary.ContainsKey(_eventType))
        {
            eventDictionary.Add(_eventType, null);
        }
        eventDictionary[_eventType] += _listener;
    }

    public static void UnRegister(EventType _eventType, System.Action _listener)
    {
        if (eventDictionary.ContainsKey(_eventType))
        {
            System.Action result = eventDictionary[_eventType];
            if (result != null)
            {
                result -= _listener;
            }
            else
            {
                Debug.LogWarning("Somehting went wrong with event: " + _eventType);
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