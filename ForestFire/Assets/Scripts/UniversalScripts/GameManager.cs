using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Object Reference
    public static GameManager game;

    // Global Variables
    public int difficulty = 1;
    public int speed = 1;
    public int score = 0;

    // Private Members
    private bool _showDebug = true;

    private void Awake()
    {
        if (game == null)
        {
            DontDestroyOnLoad(gameObject);
            game = this;
        }
        else if (game != this)
        {
            Destroy(gameObject);
        }

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>GameController</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>difficulty</color></b>: " + difficulty + "\n";
            debugStatement += "<b><color=cyan>speed</color></b>: " + speed + "\n";
            debugStatement += "<b><color=cyan>score</color></b>: " + score + "\n";
            Debug.Log(debugStatement);
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("ChallengeCompleted", StartNewChallenge);
    }

    private void OnDisable()
    {
        EventManager.StopListening("ChallengeCompleted", StartNewChallenge);
    }

    void StartNewChallenge()
    {
        IncreaseScore();
        IncreaseDifficulty();
        ChangeScene();

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>GameController</color></b></size>" + "\n";
            debugStatement += "<b><color=magenta>void StartNewChallenge()</color></b>" + "\n";
            debugStatement += "<b><color=cyan>difficulty</color></b>: " + difficulty + "\n";
            debugStatement += "<b><color=cyan>speed</color></b>: " + speed + "\n";
            debugStatement += "<b><color=cyan>score</color></b>: " + score + "\n";
            Debug.Log(debugStatement);
        }
    }

    void IncreaseScore()
    {
        score = score + 1;
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void IncreaseDifficulty()
    {
        if (difficulty == 10 && speed == 10) { return; }

        if (difficulty == 10)
        {
            speed = speed + 1;
        }
        else if (speed == 10)
        {
            difficulty = difficulty + 1;
        }
        else
        {
            bool incDifficulty = Random.Range(0, 2) == 0;
            if (incDifficulty)
            {
                difficulty = difficulty + 1;
            }
            else
            {
                speed = speed + 1;
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect( 10, 10, 100, 30 ), "Difficulty: " + difficulty);
        GUI.Label(new Rect(10, 30, 100, 30), "Speed: " + speed);
        GUI.Label(new Rect(10, 50, 100, 30), "Score: " + score);
    }
}