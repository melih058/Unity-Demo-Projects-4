using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapLayer : MonoBehaviour
{
    private Material _material;

    void Start()
    {
        onInitialize();
    }

    private void onInitialize()
    {
        _material = GetComponent<Renderer>().material;
    }

    public void setClipValue(Vector3 targetPos)
    {
        float value = transform.InverseTransformPoint(targetPos).x;
        _material.SetFloat("_ClipValue", value);
    }

    public void finish()
    {
        Destroy(gameObject);
    }
   
}
