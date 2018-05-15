using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotWallController : MonoBehaviour
{
    // Public Members
    [Header("Challenge Values")]
    public float defaultHurdleSize = 0.5f;
    [Tooltip("Max hurdle size adds to the default (max = maxHurdleSize + defaultHurdleSize)")]
    public float maxHurdleSize = 1f;

    // Components
    private Transform _wallTransform;

    // Private Members
    private bool _showDebug = true;

    // Use this for initialization
    void Start()
    {
        // Initialize Members
        _wallTransform = this.gameObject.GetComponent<Transform>();

        float hurdleSize = defaultHurdleSize + (GameManager.game.difficulty * 0.1f * maxHurdleSize);
        _wallTransform.position = new Vector3(0f, hurdleSize / 2, 0f);
        _wallTransform.localScale = new Vector3(VRBounds.bounds.length * 2f, hurdleSize, 0.5f);

        if (_showDebug)
        {
            Debug.Log("<size=20><b><color=blue>[BotWallController]</color></b></size>");
            Debug.Log("<b><color=cyan>hurdleSize</color>: </b>" + hurdleSize);
        }
    }
}
