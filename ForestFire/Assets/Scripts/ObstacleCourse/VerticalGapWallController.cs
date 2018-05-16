using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalGapWallController : MonoBehaviour
{
    // Public Members
    [Header("Gap Values")]
    public float defaultGapSize;
    public float minGapSize;
    [Tooltip("Gap Height is calculated as the bottom of the gap being at the min")]
    public float minGapHeight;
    [Tooltip("Gap Height is calculated as the bottom of the gap being at the max")]
    public float maxGapHeight;

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
    private Transform _topWallTransform;
    private Transform _botWallTransform;

    // Private Members
    private bool _showDebug = true;

    // Use this for initialization
    void Start()
    {
        // Initialize Objects
        _name = gameObject.transform.GetChild(0).gameObject;

        // Initialize Components
        _topWallTransform = gameObject.transform.GetChild(1);
        _botWallTransform = gameObject.transform.GetChild(2);
        
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
        float wallWidth = Random.Range(width, GameManager.game.difficulty * width);
        float wallLength = VRBounds.bounds.length * 2f;
        _topWallTransform.localScale = new Vector3(wallLength, topHeight, wallWidth);
        _botWallTransform.localScale = new Vector3(wallLength, botHeight, wallWidth);

        // Set Name
        _name.GetComponent<TextMesh>().text = defaultName;
        if (GameManager.game.difficulty >= trickDifficulty && Random.Range(0, trickFrequency - 1) == 0)
        {
            _name.GetComponent<TextMesh>().text = trickNames[Random.Range(0, trickNames.Count)];
        }
        _name.GetComponent<Transform>().position = new Vector3(-(wallLength / 2f) + 0.05f, height, -wallWidth / 2);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>VerticalWallGapController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>origin</color></b>: " + origin + "\n";
            debugStatement += "<b><color=cyan>gapSize</color></b>: " + gapSize + "\n";
            debugStatement += "<b><color=cyan>botHeight</color></b>: " + botHeight + "\n";
            debugStatement += "<b><color=cyan>topHeight</color></b>: " + topHeight + "\n";
            debugStatement += string.Format("<b><color=cyan>Target Height ({0})</color></b>: ", height) + (gapSize + botHeight + topHeight) + "\n";
            debugStatement += "<b><color=cyan>name</color></b>: " + _name.GetComponent<TextMesh>().text + "\n";
            Debug.Log(debugStatement);
        }
    }
}
