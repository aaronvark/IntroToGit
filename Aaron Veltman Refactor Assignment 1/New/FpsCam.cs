using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCam : MonoBehaviour
{
    public static FpsCam instance;
    public Camera cam;

    private void Start()
    {
        instance = this;
    }
}
