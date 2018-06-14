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
    public string promptText;
    
    public List<string> challenges;
    public List<string> promptTexts;

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
    }

    private void Start()
    {
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
        EventManager.StartListening("StartGame", StartGame);
        EventManager.StartListening("ChallengeCompleted", StartNewChallenge);
        EventManager.StartListening("Death", EndGame);
        EventManager.StartListening("Restart", GoToStartMenu);
        
    }

    void StartGame()
    {
        StartCoroutine("LoadFirstScene");

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>GameController</color></b></size>" + "\n";
            debugStatement += "<b><color=magenta>void StartGame()</color></b>" + "\n";
            debugStatement += "<b><color=cyan>difficulty</color></b>: " + difficulty + "\n";
            debugStatement += "<b><color=cyan>speed</color></b>: " + speed + "\n";
            debugStatement += "<b><color=cyan>score</color></b>: " + score + "\n";
            Debug.Log(debugStatement);
        }
    }

    void StartNewChallenge()
    {
        IncreaseScore();
        IncreaseDifficulty();
        StartCoroutine("ChangeScene");

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

    IEnumerator ChangeScene()
    {
        string[] winText = new string[] { "Good Job", "Wowee", "Nice", "Nice Moves", "Great", "Fantastic", "Mackin Dirty On Em", "Nice Job", "Challenge Complete", "Challenge Passed", "Nice Wow", "Passed", "More Points", "Awesome", "Congratulations", "I Like 8 Year Old Boys"};
        promptText = winText[Random.Range(0, winText.Length)] + "!";
        EventManager.TriggerEvent("WinPrompt");
        yield return new WaitForSeconds(2f);


        int randCount = Random.Range(0, challenges.Count);
        string newSceneName = challenges[randCount];
        promptText = promptTexts[randCount];

        EventManager.TriggerEvent("Prompt");
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(newSceneName);
    }

    IEnumerator LoadFirstScene()
    {
        promptText = "Starting Game!";
        EventManager.TriggerEvent("StartPrompt");
        yield return new WaitForSeconds(3f);

        int randCount = Random.Range(0, challenges.Count);
        string newSceneName = challenges[randCount];
        promptText = promptTexts[randCount];

        EventManager.TriggerEvent("Prompt");
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(newSceneName);
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

    void EndGame()
    {
        StartCoroutine("MoveToEndScene");
        
        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>GameController</color></b></size>" + "\n";
            debugStatement += "<b><color=magenta>void EndGame()</color></b>" + "\n";
            debugStatement += "<b><color=cyan>difficulty</color></b>: " + difficulty + "\n";
            debugStatement += "<b><color=cyan>speed</color></b>: " + speed + "\n";
            debugStatement += "<b><color=cyan>score</color></b>: " + score + "\n";
            Debug.Log(debugStatement);
        }
    }

    IEnumerator MoveToEndScene()
    {
        EventManager.TriggerEvent("LosePrompt");
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("EndScene");
    }

    private void OnGUI()
    {
        GUI.Label(new Rect( 10, 10, 100, 30 ), "Difficulty: " + difficulty);
        GUI.Label(new Rect(10, 30, 100, 30), "Speed: " + speed);
        GUI.Label(new Rect(10, 50, 100, 30), "Score: " + score);
    }

    void GoToStartMenu()
    {
        StartCoroutine("MoveToStartScene");
        difficulty = 1;
        speed = 1;
        score = 0;

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>GameController</color></b></size>" + "\n";
            debugStatement += "<b><color=magenta>void GoToStartMenu()</color></b>" + "\n";
            debugStatement += "<b><color=cyan>difficulty</color></b>: " + difficulty + "\n";
            debugStatement += "<b><color=cyan>speed</color></b>: " + speed + "\n";
            debugStatement += "<b><color=cyan>score</color></b>: " + score + "\n";
            Debug.Log(debugStatement);
        }
    }

    IEnumerator MoveToStartScene()
    {
        EventManager.TriggerEvent("RestartPrompt");
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainMenu");
    }
}