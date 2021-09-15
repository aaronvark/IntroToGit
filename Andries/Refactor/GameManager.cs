using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void OnEnable()
    {
        instance = this;
    }

    public float damageIncrease { get; }
}
