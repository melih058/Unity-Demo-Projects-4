using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ILaserDelegate
{
    [SerializeField] private float _moveSpeed;
    private float _initMoveSpeed;

    void Start()
    {
        onInitialize();
    }

    void Update()
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }
    private void onInitialize()
    {
        _initMoveSpeed = _moveSpeed;
    }
    public void setSlowMotion()
    {
        _moveSpeed = _initMoveSpeed / 10f;
    }
    public void setNormalMotion()
    {
        _moveSpeed = _initMoveSpeed;
    }

    public void onInteract(ILaser laser)
    {
        _moveSpeed = 0;
        GameManager.instance.failGame();
    }
}
