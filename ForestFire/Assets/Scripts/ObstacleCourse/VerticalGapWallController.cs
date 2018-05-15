using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalGapWallController : MonoBehaviour
{
    // Public Members
    [Header("Challenge Values")]
    [Tooltip("Default gap size adds minGapSize (default = defaultGapSize + minGapSize)")]
    public float defaultGapSize = 0.6f;
    public float minGapSize = 0.3f;
    public float height = 3f;

    // Components
    private Transform _topWallTransform;
    private Transform _botWallTransform;

    // Private Members
    private bool _showDebug = true;

    // Use this for initialization
    void Start()
    {
        // Initialize Members
        _topWallTransform = this.gameObject.transform.GetChild(0);
        _botWallTransform = this.gameObject.transform.GetChild(1);

        float halfMaxGap = (defaultGapSize + minGapSize) / 2f;
        float origin = Random.Range(halfMaxGap + 0.5f, halfMaxGap + 1.5f);
        float gapSize = defaultGapSize + minGapSize - (GameManager.game.difficulty * 0.1f * defaultGapSize);
        float botHeight = origin - (gapSize / 2);
        float topHeight = height - (gapSize + botHeight);

        _botWallTransform.position = new Vector3(0f, botHeight / 2, 0f);
        _botWallTransform.localScale = new Vector3(VRBounds.bounds.length * 2f, botHeight, 0.5f);

        _topWallTransform.position = new Vector3(0f, topHeight / 2 + (gapSize + botHeight), 0f);
        _topWallTransform.localScale = new Vector3(VRBounds.bounds.length * 2f, topHeight, 0.5f);

        if (_showDebug)
        {
            Debug.Log("<size=20><b><color=blue>[VerticalWallGapController]</color></b></size>");
            Debug.Log("<b><color=cyan>origin</color>: </b>" + origin);
            Debug.Log("<b><color=cyan>gapSize</color>: </b>" + gapSize);
            Debug.Log("<b><color=cyan>botHeight</color>: </b>" + botHeight);
            Debug.Log("<b><color=cyan>topHeight</color>: </b>" + topHeight);
            Debug.Log(string.Format("<b><color=cyan>Target Height ({0})</color></b>: ", height) + (gapSize + botHeight + topHeight));
        }
    }
}
