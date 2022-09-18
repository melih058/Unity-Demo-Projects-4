using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private GameObject _failPanel;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        onInitialize();
    }

    public void successGame()
    {
        _successPanel.SetActive(true);
    }
    public void failGame()
    {
        _failPanel.SetActive(true);

    }

    private void onInitialize()
    {
        _successPanel.SetActive(false);
        _failPanel.SetActive(false);
    }

}
