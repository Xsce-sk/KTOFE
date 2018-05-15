using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRBounds : MonoBehaviour
{
    // Object Reference
    public static VRBounds bounds;
    public float length;
    public float width;

    // Play Area
    private GameObject _cameraRig;
    private Vector3[] _vertices;

    // Private Members
    private bool _showDebug = true;

    protected virtual void Awake()
    {
        if (bounds == null)
        {
            bounds = this;
        }
        else if (bounds != this)
        {
            Destroy(gameObject);
        }

        _cameraRig = GameObject.Find("[VRTK_SDKManager]/SDKSetups/SteamVR/[CameraRig]");
        VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
        _vertices = _cameraRig.GetComponent<SteamVR_PlayArea>().vertices;
        GetBounds();

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>VRBounds</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>length</color></b>: " + length + "\n";
            debugStatement += "<b><color=cyan>width</color></b>: " + width + "\n";
            Debug.Log(debugStatement);
        }
    }

    protected virtual void OnDestroy()
    {
        VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
    }

    void GetBounds()
    {
        length = Vector3.Distance(_vertices[0], _vertices[1]);
        width = Vector3.Distance(_vertices[1], _vertices[2]);
    }
}
