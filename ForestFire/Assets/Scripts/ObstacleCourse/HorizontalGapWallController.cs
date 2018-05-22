using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalGapWallController : MonoBehaviour
{
    [Header("Wall Values")]
    public float height = 3f;
    public float width = 0.5f;

    // Components
    private Transform _wallTransform;

    // Private Members
    private bool _showDebug = false;
    private string _wallType;

    void Awake()
    {
        // Initialize Components
        _wallTransform = gameObject.transform.GetChild(0);

        // Set Wall Scale
        float wallLength = VRBounds.bounds.length * 2f;
        _wallTransform.localScale = new Vector3(wallLength / 2f, height, width);

        if (Random.Range(0, 2) == 0) // Make Left Wall
        {
            _wallType = "Left";
            _wallTransform.position = new Vector3(-wallLength / 4f, height / 2f, 0f);
        }
        else // Make Right Wall
        {
            _wallType = "Right";
            _wallTransform.position = new Vector3(wallLength / 4f, height / 2f, 0f);
        }

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>HorizontalWallGapController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>_wallType</color></b>: " + _wallType + "\n";
            Debug.Log(debugStatement);
        }
    }
}
