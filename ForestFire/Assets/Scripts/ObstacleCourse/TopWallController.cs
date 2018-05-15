using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TopWallController : MonoBehaviour
{
    // Public Members
    [Header("Challenge Values")]
    public float defaultGapSize = 0.6f;
    public float minGapSize = 0.3f;
    public float height = 3f;

    // Components
    private Transform _wallTransform;

    // Private Members
    private bool _showDebug = true;

    // Use this for initialization
    void Start()
    {
        // Initialize Members
        _wallTransform = this.gameObject.GetComponent<Transform>();

        float gapSize = defaultGapSize + minGapSize - (GameManager.game.difficulty * 0.1f * defaultGapSize);
        float wallHeight = height - gapSize;
        _wallTransform.position = new Vector3(0f, wallHeight / 2f + gapSize, 0f);
        _wallTransform.localScale = new Vector3(VRBounds.bounds.length * 2f, wallHeight, 0.5f);

        if (_showDebug)
        {
            Debug.Log("<size=20><b><color=blue>[BotWallController]</color></b></size>");
            Debug.Log("<b><color=cyan>gapSize</color>: </b>" + gapSize);
            Debug.Log(string.Format("<b><color=cyan>Target Height ({0})</color></b>: ", height) + (gapSize + wallHeight));
        }
    }
}
