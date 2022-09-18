using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    private RootRing[] _rootRings;
    private RootRing _selectedRootRing;
    private Ring _selectedRing;
    private float _zPos;
    private Vector3 _offset;
    private Camera _mainCamera;
    private RootRing _closestRootRing;

    void Start()
    {
        onInitialize();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _selectedRing == null)
        {
            trySelectRing();
        }
        else if (Input.GetMouseButton(0) && _selectedRing != null)
        {
            moveSelectedRing();
        }
        else if (Input.GetMouseButtonUp(0) && _selectedRing != null)
        {
            dropSelectedRing();

        }

    }

    private void onInitialize()
    {
        _mainCamera = Camera.main;

        _rootRings = GetComponentsInChildren<RootRing>();
    }
    private void dropSelectedRing()
    {
        _closestRootRing?.hideGhostRing();
        Ring closestLastRing = _closestRootRing.getLastRing();
        if (closestLastRing == null || _selectedRing.ringType == closestLastRing.ringType)
        {
            _selectedRootRing.removeRing(_selectedRing);
            _closestRootRing.addRing(_selectedRing);
        }
        else
        {
            _selectedRootRing.deselectLastRing();
        }

        _closestRootRing = null;
        _selectedRing = null;
        _selectedRootRing = null;
    }

    private void moveSelectedRing()
    {
        _selectedRing.transform.position = _offset + getMouseWorld();

        _closestRootRing?.hideGhostRing();
        _closestRootRing = findClosestRingRoot();
        Ring closestLastRing = _closestRootRing.getLastRing();
        if (closestLastRing == null || closestLastRing.ringType == _selectedRing.ringType)
        {
            _closestRootRing.showGhostRing();
        }
    }

    private void trySelectRing()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, 1 << 8))
        {
            _selectedRootRing = hit.collider.GetComponent<RootRing>();
            if (_selectedRootRing.isBusy)
            {
                _selectedRootRing = null;
                return;
            }
            _selectedRing = _selectedRootRing.selectLastRing();
            if (_selectedRing == null)
            {
                return;
            }
            Vector3 ringPosition = _selectedRing.transform.position;
            _zPos = _mainCamera.WorldToScreenPoint(ringPosition).z;
            _offset = ringPosition - getMouseWorld();
        }
    }

    private RootRing findClosestRingRoot()
    {
        float minDistance = float.MaxValue;
        int minIndex = 0;
        for (int i = 0; i < _rootRings.Length; i++)
        {
            RootRing rootRing = _rootRings[i];
            if (_selectedRootRing == rootRing)
            {
                continue;
            }

            float distance = (_selectedRing.transform.position - rootRing.transform.position).magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }
        return _rootRings[minIndex];
    }

    private Vector3 getMouseWorld()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _zPos;
        return _mainCamera.ScreenToWorldPoint(mousePos);
    }

}
