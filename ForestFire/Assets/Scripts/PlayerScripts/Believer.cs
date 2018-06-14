using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Believer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<VRTK_InteractTouch>().ControllerStartTouchInteractableObject += new ObjectInteractEventHandler(OnControllerTouchInteractableObject);
    }

    private void OnControllerTouchInteractableObject(object o, ObjectInteractEventArgs e)
    {
        string tag = e.target.gameObject.tag;
        if (tag == "Obstacle" || tag == "BelieverObstacle")
        {
            EventManager.TriggerEvent("Death");
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Obstacle" || tag == "BelieverObstacle")
        {
            EventManager.TriggerEvent("Death");
        }
    }*/
}
