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

        // Calc Hurdle
        float hurdleSize = defaultHurdleSize;
        float hurdleDifference = maxHurdleSize - defaultHurdleSize;
        hurdleSize += (GameManager.game.difficulty - 1) * (1f / 9f) * hurdleDifference; // Add the percentage of the hurdleDifference

        // Set Wall Position
        _wallTransform.position = new Vector3(0f, hurdleSize / 2, 0f);

        // Set Wall Scale
        float wallWidth = Random.Range(width, GameManager.game.difficulty * width);
        float wallLength = VRBounds.bounds.length * 2f;
        _wallTransform.localScale = new Vector3(wallLength, hurdleSize, wallWidth - 0.01f);

        // Set Name
        _name.GetComponent<TextMesh>().text = defaultName;
        if (GameManager.game.difficulty >= trickDifficulty && Random.Range(0, trickFrequency - 1) == 0)
        {
            _name.GetComponent<TextMesh>().text = trickNames[Random.Range(0, trickNames.Count)];
        }
        _name.GetComponent<Transform>().position = new Vector3(-(wallLength / 2f) + 0.05f, hurdleSize, -wallWidth / 2);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>BotWallController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>hurdleSize</color></b>: " + hurdleSize + "\n";
            debugStatement += "<b><color=cyan>name</color></b>: " + _name.GetComponent<TextMesh>().text + "\n";
            Debug.Log(debugStatement);
        }
    }
}
