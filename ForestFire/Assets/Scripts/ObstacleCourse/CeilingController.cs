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

    [Header("Name Values")]
    public string defaultName;
    public List<string> trickNames;
    public float trickDifficulty;
    [Tooltip("One out of how many names should be a trick")]
    public int trickFrequency;

    // Object References
    private GameObject _name;

    // Components
    private Transform _wallTransform;

    // Private Members
    private bool _showDebug = true;

    // Use this for initialization
    void Start()
    {
        // Initialize Objects
        _name = gameObject.transform.GetChild(0).gameObject;

        // Initialize Components
        _wallTransform = gameObject.transform.GetChild(1);

        // Calc Gap
        float gapSize = defaultGapSize;
        float gapDifference = defaultGapSize - minGapSize;
        gapSize -= (GameManager.game.difficulty - 1) * (1f / 9f) * gapDifference; // Subtract the percentage of the gapDifference

        // Set Wall Position
        float wallHeight = height - gapSize;
        _wallTransform.position = new Vector3(0f, wallHeight / 2f + gapSize, 0f);

        // Set Wall Scale
        float wallWidth = Random.Range(width, GameManager.game.difficulty * width);
        float wallLength = VRBounds.bounds.length * 2f;
        _wallTransform.localScale = new Vector3(wallLength, wallHeight, wallWidth);

        // Set Name
        _name.GetComponent<TextMesh>().text = defaultName;
        if (GameManager.game.difficulty >= trickDifficulty && Random.Range(0, trickFrequency - 1) == 0)
        {
            _name.GetComponent<TextMesh>().text = trickNames[Random.Range(0, trickNames.Count)];
        }
        _name.GetComponent<Transform>().position = new Vector3(-(wallLength / 2f) + 0.05f, height, -wallWidth / 2);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>TopWallController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>gapSize</color></b>: " + gapSize + "\n";
            debugStatement += "<b><color=cyan>wallWidth</color></b>: " + wallWidth + "\n";
            debugStatement += string.Format("<b><color=cyan>Target Height ({0})</color></b>: ", height) + (gapSize + wallHeight) + "\n";
            debugStatement += "<b><color=cyan>name</color></b>: " + _name.GetComponent<TextMesh>().text + "\n";
            Debug.Log(debugStatement);
        }
    }
}
