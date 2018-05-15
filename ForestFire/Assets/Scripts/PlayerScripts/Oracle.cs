using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Oracle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Obstacle" || tag == "OracleObstacle")
        {
            EventManager.TriggerEvent("Death");
        }
    }
}
