using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CeilingController : MonoBehaviour
{
    // Public Members
    [Header("Gap Values")]
    public float defaultGapSize;
    public float minGapSize;

    [Header("Wall Values")]
    public float height;
    public float width;

    // Components
    private Transform _wallTransform;

    // Private Members
    private bool _showDebug = false;

    void Awake()
    {

        // Initialize Components
        _wallTransform = gameObject.transform.GetChild(0);

        // Calc Gap
        float gapSize = defaultGapSize;
        float gapDifference = defaultGapSize - minGapSize;
        gapSize -= (GameManager.game.difficulty - 1) * (1f / 9f) * gapDifference; // Subtract the percentage of the gapDifference

        // Set Wall Position
        float wallHeight = height - gapSize;
        _wallTransform.position = new Vector3(0f, wallHeight / 2f + gapSize, 0f);

        // Set Wall Scale
        float wallWidth = Random.Range(width, width * 4f);
        float wallLength = VRBounds.bounds.length * 2f;
        _wallTransform.localScale = new Vector3(wallLength, wallHeight, wallWidth);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>TopWallController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>gapSize</color></b>: " + gapSize + "\n";
            debugStatement += "<b><color=cyan>wallWidth</color></b>: " + wallWidth + "\n";
            debugStatement += string.Format("<b><color=cyan>Target Height ({0})</color></b>: ", height) + (gapSize + wallHeight) + "\n";
            Debug.Log(debugStatement);
        }
    }
}
