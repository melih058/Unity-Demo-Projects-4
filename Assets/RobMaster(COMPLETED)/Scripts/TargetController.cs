using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private Camera _mainCamera;
    private Target _target;
    private float _zPos;
    private Vector3 _offset;

    void Start()
    {
        onInitialize();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _target == null)
        {
            tryGetTarget();
        }
        else if (Input.GetMouseButton(0) && _target != null)
        {
            moveTarget();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            releaseTarget();
        }
    }
    private void onInitialize()
    {
        _mainCamera = Camera.main;
    }

    private void releaseTarget()
    {
        _target = null;
    }

    private void moveTarget()
    {
        _target.setPosition(_offset + getMouseWorld());
    }

    private void tryGetTarget()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20f, 1 << 6))
        {
            Transform target = hit.collider.transform;
            _target = target.GetComponent<Target>();
            _zPos = _mainCamera.WorldToScreenPoint(target.position).z;
            _offset = target.position - getMouseWorld();
        }
    }

    private Vector3 getMouseWorld()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _zPos;
        return _mainCamera.ScreenToWorldPoint(mousePoint);
    }


}
