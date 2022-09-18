using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public struct RingSpawnData
{
    public Ring ring;
}

public class RootRing : MonoBehaviour
{
    [SerializeField] private RingSpawnData[] _ringSpawnDatas;
    private List<Ring> _inRings;
    [SerializeField] private float _yOffsetFactor;
    [SerializeField] private float _initOffset;
    private Ring _lastSelectedRing;
    [SerializeField] private RingType _rootRingType;
    [SerializeField] private Transform _ghostRing;
    [SerializeField] private float _yJumpOffset;
    private bool _isBusy;

    void Start()
    {
        onInitialize();
    }


    private void onInitialize()
    {
        _inRings = new List<Ring>();

        for (int i = 0; i < _ringSpawnDatas.Length; i++)
        {
            Ring ring = Instantiate(_ringSpawnDatas[i].ring, transform);
            ring.transform.localPosition = getRingPosition();
            _inRings.Add(ring);
            setGhostRing();
        }
        _ghostRing.gameObject.SetActive(false);
    }

    private void setGhostRing()
    {
        _ghostRing.localPosition = getRingPosition();
    }

    public void removeRing(Ring ring)
    {
        _inRings.Remove(ring);
        setGhostRing();
        if (_inRings.Count == 0)
        {
            GameManager.instance.successGame();
            GetComponent<Animator>().CrossFadeInFixedTime("Dance", 0.25f);
        }

    }

    public void addRing(Ring ring)
    {
        _isBusy = true;
        ring.transform.SetParent(transform);
        Vector3 targetPos = getRingPosition();
        ring.transform.localPosition = new Vector3(0f, targetPos.y + _yJumpOffset, 0f);
        _inRings.Add(ring);
        setGhostRing();
        ring.transform.DOLocalMove(targetPos, 0.75f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            _isBusy = false;
        });

    }
    public Ring selectLastRing()
    {
        if (_isBusy)
            return null;

        if (_inRings.Count == 0)
            return null;

        _lastSelectedRing = _inRings[_inRings.Count - 1];
        _lastSelectedRing.transform.position += Vector3.up * _yJumpOffset;
        return _lastSelectedRing;
    }
    public void deselectLastRing()
    {
        _isBusy = true;
        Vector3 targetPos = new Vector3(0f, _initOffset + (_inRings.Count - 1) * _yOffsetFactor, 0f);
        _lastSelectedRing.transform.localPosition = new Vector3(0f, targetPos.y + _yJumpOffset, 0f);
        _lastSelectedRing.transform.DOLocalMove(targetPos, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            _isBusy = false;
        }); ;
    }

    private Vector3 getRingPosition()
    {
        return new Vector3(0f, _initOffset + _inRings.Count * _yOffsetFactor, 0f);
    }

    public Ring getLastRing()
    {
        if (_inRings.Count == 0)
        {
            return null;
        }

        return _inRings[_inRings.Count - 1];
    }

    public void showGhostRing()
    {
        _ghostRing.gameObject.SetActive(true);
    }
    public void hideGhostRing()
    {
        _ghostRing.gameObject.SetActive(false);
    }


    public RingType rootRingType
    {
        get
        {
            return _rootRingType;
        }
    }
    public bool isBusy
    {
        get
        {
            return _isBusy;
        }
    }
}
