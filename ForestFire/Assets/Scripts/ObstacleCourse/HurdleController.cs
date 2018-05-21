using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleController : MonoBehaviour
{
    // Public Members
    [Header("Hurdle Values")]
    public float defaultHurdleSize = 0.5f;
    public float maxHurdleSize = 1f;

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

        // Calc Hurdle
        float hurdleSize = defaultHurdleSize;
        float hurdleDifference = maxHurdleSize - defaultHurdleSize;
        hurdleSize += (GameManager.game.difficulty - 1) * (1f / 9f) * hurdleDifference; // Add the percentage of the hurdleDifference

        // Set Wall Position
        _wallTransform.position = new Vector3(0f, hurdleSize / 2, 0f);

        // Set Wall Scale
        float wallWidth = Random.Range(width, width * 4f);
        float wallLength = VRBounds.bounds.length * 2f;
        _wallTransform.localScale = new Vector3(wallLength, hurdleSize, wallWidth - 0.01f);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>BotWallController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>hurdleSize</color></b>: " + hurdleSize + "\n";
            Debug.Log(debugStatement);
        }
    }
}
