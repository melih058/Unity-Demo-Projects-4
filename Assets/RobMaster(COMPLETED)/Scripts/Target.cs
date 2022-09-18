using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Transform _initTransform;
    [SerializeField] private float _radius;

    void Start()
    {
        onInitialize();
    }



    void Update()
    {

    }
    private void onInitialize()
    {
        _initTransform = new GameObject().transform;
        _initTransform.SetParent(transform.parent);
        _initTransform.localPosition = transform.localPosition;
    }

    public void setPosition(Vector3 targetPos)
    {
        float distance = (targetPos - _initTransform.position).magnitude;
        if (distance < _radius)
        {
            transform.position = targetPos;
        }

    }
}
