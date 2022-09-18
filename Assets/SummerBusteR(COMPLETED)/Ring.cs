using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RingType
{
    Yellow = 0,
    Green,
    Pink,
    Blue,

}
public class Ring : MonoBehaviour
{
    [SerializeField] private RingType _ringType;


    public RingType ringType
    {
        get
        {
            return _ringType;
        }
    }
}
