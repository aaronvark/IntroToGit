using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouseManager : MonoBehaviour
{
    public InputMouseManager Instance;
    private void OnEnable()
    {
        Instance = this;
    }

    public enum MouseButtonType
    {
        Left = 0,
        Right = 1,
        Center = 2
    }

    private Dictionary<MouseButtonType, ResponseContainer> keyDictionary = new Dictionary<MouseButtonType, ResponseContainer>();

    private void Update()
    {
        foreach (var kv in keyDictionary)
        {
            if (Input.GetMouseButtonDown((int)kv.Key))
            {
                kv.Value.OnKeyDown?.Invoke();
            }

            if (Input.GetMouseButtonUp((int)kv.Key))
            {
                kv.Value.OnKeyUp?.Invoke();
            }

            if (Input.GetMouseButton((int)kv.Key))
            {
                kv.Value.OnKeyPressed?.Invoke();
            }
        }
    }

    public void RegisterKey(
        MouseButtonType _key,
        System.Action _downResponse = null,
        System.Action _pressedResponse = null,
        System.Action _upResponse = null
        )
    {
        if (!keyDictionary.ContainsKey(_key))
        {
            keyDictionary.Add(_key, new ResponseContainer());
        }

        ResponseContainer keyContainer = keyDictionary[_key];
        if (_downResponse != null)
        {
            keyContainer.OnKeyDown += _downResponse;
        }

        if (_upResponse != null)
        {
            keyContainer.OnKeyUp += _upResponse;
        }

        if (_pressedResponse != null)
        {
            keyContainer.OnKeyPressed += _pressedResponse;
        }
    }

    public void UnRegisterKey(
        MouseButtonType _key,
        System.Action _downResponse = null,
        System.Action _pressedResponse = null,
        System.Action _upResponse = null
        )
    {
        if (!keyDictionary.ContainsKey(_key)) { return; }
        ResponseContainer container = keyDictionary[_key];
        if (_pressedResponse != null)
        {
            container.OnKeyPressed -= _pressedResponse;
        }
        if (_upResponse != null)
        {
            container.OnKeyUp -= _upResponse;
        }
        if (_downResponse != null)
        {
            container.OnKeyDown -= _downResponse;
        }
    }
}
