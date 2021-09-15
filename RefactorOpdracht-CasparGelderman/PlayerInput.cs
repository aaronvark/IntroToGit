using System;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
    public static event Action<bool> jump;
    public static event Action<float> input;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        jump?.Invoke(Input.GetButton("Jump"));
        input?.Invoke(Input.GetAxisRaw("Horizontal"));
    }
}
