using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILaser
{

}

public interface ILaserDelegate
{
    public void onInteract(ILaser laser);
}

public class Laser : MonoBehaviour, ILaser
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ILaserDelegate>() is ILaserDelegate laserDelegate)
        {
            laserDelegate.onInteract(this);
        }
    }
}
