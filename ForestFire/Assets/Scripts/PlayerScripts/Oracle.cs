using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Oracle : MonoBehaviour
{
    private void Start()
    {
        GetComponent<VRTK_HeadsetCollision>().HeadsetCollisionDetect += new HeadsetCollisionEventHandler(OnHeadsetCollisionDetect);
    }

    private void OnHeadsetCollisionDetect(object o, HeadsetCollisionEventArgs e)
    {
        string tag = e.collider.gameObject.tag;
        if (tag == "Obstacle" || tag == "OracleObstacle")
        {
            EventManager.TriggerEvent("Death");
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Obstacle" || tag == "OracleObstacle")
        {
            EventManager.TriggerEvent("Death");
        }
    }*/
}
