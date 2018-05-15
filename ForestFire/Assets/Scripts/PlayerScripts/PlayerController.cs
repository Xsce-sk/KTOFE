using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerController : MonoBehaviour
{
    // Object References
    private VRTK_ControllerReference _leftController;
    private VRTK_ControllerReference _rightController;

    // Private Members
    private bool _showDebug = true;

    private void Start()
    {
        StartCoroutine("GetControllers");
    }

    IEnumerator GetControllers()
    {
        yield return new WaitForSeconds(1f);
        _leftController = VRTK_ControllerReference.GetControllerReference(gameObject.transform.GetChild(0).gameObject);
        _rightController = VRTK_ControllerReference.GetControllerReference(gameObject.transform.GetChild(1).gameObject);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>PlayerController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>_leftController</color></b>: " + _leftController + "\n";
            debugStatement += "<b><color=cyan>_rightController</color></b>: " + _rightController + "\n";
            Debug.Log(debugStatement);
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("Death", VibrateControllers);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Death", VibrateControllers);
    }

    void VibrateControllers()
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(_leftController, 1f, 2.5f, 0.01f);
        VRTK_ControllerHaptics.TriggerHapticPulse(_rightController, 1f, 2.5f, 0.01f);
    }
}
