using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapController : MonoBehaviour
{
    [SerializeField] private SoapLayer[] _soapLayers;
    private SoapLayer _currentSoapLayer;
    private int _soapLayerIterator;
    [SerializeField] private Transform _knifeTransform;
    [SerializeField] private float _cutSpeed;
    [SerializeField] private ParticleSystem _particle;
    private Vector3 _knifeInitPosition;
    private Vector3 _knifeTargetPosition;
    private float _knifeLerpTime;

    void Start()
    {
        onInitialize();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouch();
        }
        else if (Input.GetMouseButton(0))
        {
            hold();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            release();

        }
    }

    private void onInitialize()
    {
        _soapLayerIterator = 0;
        _knifeInitPosition = _knifeTransform.position;
        _knifeTargetPosition = _knifeTransform.position - _knifeTransform.right * 1.2f;
        setTargetLayer();
    }

    private void release()
    {
        _particle.Stop();
    }

    private void hold()
    {
        _knifeTransform.position = Vector3.Lerp(_knifeInitPosition, _knifeTargetPosition, _knifeLerpTime);
        _currentSoapLayer.setClipValue(_knifeTransform.position);
        _knifeLerpTime += Time.deltaTime * _cutSpeed;
        if (_knifeLerpTime >= 1f)
        {
            _soapLayerIterator++;
            _knifeLerpTime = 0f;
            setTargetLayer();
        }
    }

    private void firstTouch()
    {
        _particle.Play();
    }

    private void setTargetLayer()
    {
        if (_soapLayerIterator >= _soapLayers.Length)
        {
            GameManager.instance.successGame();
            return;
        }
        _currentSoapLayer = _soapLayers[_soapLayerIterator];
        _knifeTransform.position = _knifeInitPosition;
        _particle.GetComponent<ParticleSystemRenderer>().material.color = _currentSoapLayer.GetComponent<Renderer>().material.GetColor("_BaseColor");
    }
}
