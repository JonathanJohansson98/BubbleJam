using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SafeZoneManager : MonoBehaviour
{
    [SerializeField] public int thisSafeZone;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }


}
