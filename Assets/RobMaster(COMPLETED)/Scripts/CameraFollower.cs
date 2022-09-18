using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _posOffset;
    [SerializeField] private Vector3 _rotOffset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
            return;

        transform.position = _target.position + _target.rotation * _posOffset;
        transform.rotation = _target.rotation * Quaternion.Euler(_rotOffset);
    }
}
