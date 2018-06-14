using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RestartButtonController : MonoBehaviour
{
    private bool _onButton;
    private bool _started;

    private void Start()
    {
        GetComponent<VRTK_InteractTouch>().ControllerStartTouchInteractableObject += new ObjectInteractEventHandler(OnControllerTouchInteractableObject);
        GetComponent<VRTK_InteractTouch>().ControllerStartUntouchInteractableObject += new ObjectInteractEventHandler(OnControllerUntouchInteractableObject);
        GetComponent<VRTK_ControllerEvents>().TriggerClicked += new ControllerInteractionEventHandler(DoTriggerClicked);
    }

    private void OnControllerTouchInteractableObject(object o, ObjectInteractEventArgs e)
    {
        string tag = e.target.gameObject.tag;
        if (tag == "RestartButton")
        {
            _onButton = true;
        }
    }

    private void OnControllerUntouchInteractableObject(object o, ObjectInteractEventArgs e)
    {
        string tag = e.target.gameObject.tag;
        if (tag == "RestartButton")
        {
            _onButton = false;
        }
    }

    private void DoTriggerClicked(object o, ControllerInteractionEventArgs e)
    {
        if (_onButton && !_started)
        {
            EventManager.TriggerEvent("Restart");
            _started = true;
        }
    }
}
