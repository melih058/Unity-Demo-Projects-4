using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpeedDecrease : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() is Player player)
        {
            player.setSlowMotion();
        }
    }
}
