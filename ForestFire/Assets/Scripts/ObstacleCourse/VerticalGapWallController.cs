using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalGapWallController : MonoBehaviour
{
    // Public Members
    [Header("Gap Values")]
    public float defaultGapSize = 1f;
    public float minGapSize = 0.3f;
    [Tooltip("Gap Height is calculated as the bottom of the gap being at the min")]
    public float minGapHeight = 1f;
    [Tooltip("Gap Height is calculated as the bottom of the gap being at the max")]
    public float maxGapHeight = 0.5f;

    [Header("Wall Values")]
    public float height = 3f;
    public float width = 0.5f;

    // Object References
    private GameObject _name;

    // Components
    private Transform _topWallTransform;
    private Transform _botWallTransform;

    // Private Members
    private bool _showDebug = false;

    void Awake()
    {
        // Initialize Components
        _topWallTransform = gameObject.transform.GetChild(0);
        _botWallTransform = gameObject.transform.GetChild(1);
        
        // Calc Gap
        float gapSize = defaultGapSize;
        float gapDifference = defaultGapSize - minGapSize;
        gapSize -= (GameManager.game.difficulty - 1) * (1f / 9f) * gapDifference; // Subtract the percentage of the gapDifference

        // Calc Heights
        float origin = gapSize / 2 + Random.Range(minGapHeight, maxGapHeight);
        float botHeight = origin - (gapSize / 2);
        float topHeight = height - (gapSize + botHeight);

        // Set Wall Positions
        _botWallTransform.position = new Vector3(0f, botHeight / 2, 0f);
        _topWallTransform.position = new Vector3(0f, topHeight / 2 + (gapSize + botHeight), 0f);


        // Set Wall Scales
        float wallWidth = Random.Range(width, width * 4f);
        float wallLength = VRBounds.bounds.length * 2f;
        _topWallTransform.localScale = new Vector3(wallLength, topHeight, wallWidth);
        _botWallTransform.localScale = new Vector3(wallLength, botHeight, wallWidth);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>VerticalWallGapController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>origin</color></b>: " + origin + "\n";
            debugStatement += "<b><color=cyan>gapSize</color></b>: " + gapSize + "\n";
            debugStatement += "<b><color=cyan>botHeight</color></b>: " + botHeight + "\n";
            debugStatement += "<b><color=cyan>topHeight</color></b>: " + topHeight + "\n";
            debugStatement += string.Format("<b><color=cyan>Target Height ({0})</color></b>: ", height) + (gapSize + botHeight + topHeight) + "\n";
            Debug.Log(debugStatement);
        }
    }
}
