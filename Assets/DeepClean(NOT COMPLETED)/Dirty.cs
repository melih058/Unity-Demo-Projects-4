using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirty : MonoBehaviour
{
    private Material _material;
    void Start()
    {
        onInitialize();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void onInitialize()
    {
        _material = GetComponent<Renderer>().material;
    }
}
