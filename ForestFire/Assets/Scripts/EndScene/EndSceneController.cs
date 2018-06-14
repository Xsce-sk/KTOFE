using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour
{
    // Private Members
    public GameObject _gameOverText;
    public GameObject _scoreText;
    public GameObject _diffText;

    // Private Components
    private TextMesh _scoreTextComponent;
    private TextMesh _diffTextComponent;
    private Transform _gameOverTextTransform;
    private Transform _scoreTextTransform;
    private Transform _diffTextTransform;

    void Start ()
    {
        // Get Objects
        _gameOverText = GameObject.Find("GameOverText");
        _scoreText = GameObject.Find("ScoreText");
        _diffText = GameObject.Find("DifficultyText");

        // Get Components
        _scoreTextComponent = _scoreText.GetComponent<TextMesh>();
        _diffTextComponent = _diffText.GetComponent<TextMesh>();
        _gameOverTextTransform = _gameOverText.GetComponent<Transform>();
        _scoreTextTransform = _scoreText.GetComponent<Transform>();
        _diffTextTransform = _diffText.GetComponent<Transform>();

        // Set Positions
        _gameOverTextTransform.position = new Vector3(0f, 2.35f, VRBounds.bounds.width + 0.5f);
        _scoreTextTransform.position = new Vector3(0f, 1.5f, VRBounds.bounds.width + 0.5f);
        _diffTextTransform.position = new Vector3(0f, 0.7f, VRBounds.bounds.width + 0.5f);

        // Set Text


        _scoreTextComponent.text = "Score: " + GameManager.game.score;
        _diffTextComponent.text = "Difficulty: " + GameManager.game.difficulty + "/10" + System.Environment.NewLine + "Speed: " + GameManager.game.speed + "/10";
    }
}
