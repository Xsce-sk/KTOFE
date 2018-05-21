using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalGapWallController : MonoBehaviour
{
    [Header("Wall Values")]
    public float height;
    public float width;

    [Header("Name Values")]
    public string defaultName;
    public List<string> trickNames;
    public float trickDifficulty;
    [Tooltip("One out of how many names should be a trick")]
    public int trickFrequency;

    // Components
    private Transform _wallTransform;

    // Private Members
    private bool _showDebug = false;
    private string _wallType;

    void Awake()
    {
        // Initialize Components
        _wallTransform = gameObject.transform.GetChild(1);

        // Get Wall Scales
        float wallLength = VRBounds.bounds.length * 2f;

        if (Random.Range(0, 1) == 0) // Make Left Wall
        {
            _wallType = "Left";
            _wallTransform.localScale = new Vector3(wallLength / 2f, height, width);
            _wallTransform.position = new Vector3(-wallLength / 4f, height / 2f, 0f);
        }
        else // Make Right Wall
        {
            _wallType = "Right";
        }

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>VerticalWallGapController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>WallType</color></b>: " + origin + "\n";
            Debug.Log(debugStatement);
        }
    }
}
