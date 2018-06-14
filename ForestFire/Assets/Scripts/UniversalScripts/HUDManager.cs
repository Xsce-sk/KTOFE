using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    // Private Members
    private GameObject _timer;
    private GameObject _promptText;
    private GameObject _scoreText;

    // Private Components
    private Text _promptTextComponent;
    private Text _scoreTextComponent;

    private void Start()
    {
        // Get Objects
        _timer = gameObject.transform.GetChild(0).gameObject;
        _promptText = gameObject.transform.GetChild(1).gameObject;
        _scoreText = gameObject.transform.GetChild(2).gameObject;

        // Get Components
        _promptTextComponent = _promptText.GetComponent<Text>();
        _scoreTextComponent = _scoreText.GetComponent<Text>();

        // Disable Game Objects
        _timer.SetActive(false);
        _promptText.SetActive(false);
        _scoreText.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.StartListening("StartPrompt", EnablePromptText);
        EventManager.StartListening("Prompt", EnableText);
        EventManager.StartListening("WinPrompt", EnablePromptText);
        EventManager.StartListening("LosePrompt", DisplayLossText);
        EventManager.StartListening("RestartPrompt", DisplayRestartText);
    }

    public void EnablePromptText()
    {
        _timer.SetActive(false);
        _scoreText.SetActive(false);

        _promptTextComponent.text = GameManager.game.promptText;
        _promptText.SetActive(true);
    }

    public void EnableText()
    {
        _promptTextComponent.text = GameManager.game.promptText;
        _promptText.SetActive(true);

        _scoreTextComponent.text = "Current Score: " + GameManager.game.score;
        _scoreText.SetActive(true);
    }

    public void DisplayLossText()
    {
        _scoreText.SetActive(false);

        _promptTextComponent.text = "Game Over!";
        _promptText.SetActive(true);
    }

    public void DisplayRestartText()
    {
        _scoreText.SetActive(false);

        _promptTextComponent.text = "Restarting Game";
        _promptText.SetActive(true);
    }
}