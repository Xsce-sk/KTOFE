using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Believer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Obstacle" || tag == "BelieverObstacle")
        {
            EventManager.TriggerEvent("Death");
        }
    }
}
