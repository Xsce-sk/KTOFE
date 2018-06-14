using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceChallenge : MonoBehaviour
{
    // Public Members
    public GameObject prefab;
    public float wallHeight = 3f;

    [Header("Timer Values")]
    public float defaultTime = 10f;
    public float maxTime = 20f;
    public GameObject timerText;

    [Header("Gravity Values")]
    public float defaultGravity = -2f;
    public float maxGravity = -5f;

    [Tooltip("Time between balls dropping")]
    public float ballTimeDifference = 3f;

    // Wall Objects
    private GameObject _frontWall;
    private GameObject _backWall;
    private GameObject _leftWall;
    private GameObject _rightWall;

    // Private Members
    private float _timer;

    // Object Components
    private Text _timerTextComponent;

    private bool _won = false;

    private void Start()
    {
        // Set Gravity
        float gravity = defaultGravity;
        float gravityDistance = (defaultGravity - maxGravity);
        gravity -= (GameManager.game.speed - 1) * (1f / 9f) * gravityDistance;
        Physics.gravity = new Vector3(0f, gravity, 0f);

        // Initialize Objects
        _frontWall = GameObject.Find("Front");
        _backWall = GameObject.Find("Back");
        _leftWall = GameObject.Find("Left");
        _rightWall = GameObject.Find("Right");

        // Get Text Component
        timerText.SetActive(true);
        _timerTextComponent = timerText.GetComponent<Text>();

        // Set Timer
        _timer = defaultTime;
        float timeDifference = (maxTime - defaultTime);
        _timer += (GameManager.game.speed - 1) * (1f / 9f) * timeDifference;

        SetPositions();
        StartCoroutine("StartChallenge");
    }

    bool CountDown(float deltaT)
    {
        // Decrement Timer
        _timer -= deltaT;

        // Set Timer Text
        SetTimerDisplay(_timer);

        return (_timer <= 0);
    }

    void SetTimerDisplay(float t)
    {
        _timerTextComponent.text = "Time Left\n" + t.ToString("0.00") + "s";
    }

    void Update()
    {
        if (CountDown(Time.deltaTime) && !_won)
        {
            SetTimerDisplay(0f);
            EventManager.TriggerEvent("ChallengeCompleted");
            StopCoroutine("BallTimer");
            _won = true;
        }
    }

    void SetPositions()
    {
        _frontWall.GetComponent<Transform>().position = new Vector3(0f, -wallHeight / 2f - 1f, VRBounds.bounds.length);
        _frontWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.width * 2f, wallHeight, 0.5f);

        _backWall.GetComponent<Transform>().position = new Vector3(0f, -wallHeight / 2f - 1f, -VRBounds.bounds.length);
        _backWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.width * 2f, wallHeight, 0.5f);

        _leftWall.GetComponent<Transform>().position = new Vector3(-VRBounds.bounds.width, -wallHeight / 2f - 1f, 0f);
        _leftWall.GetComponent<Transform>().localScale = new Vector3(0.5f, wallHeight, VRBounds.bounds.length * 2);

        _rightWall.GetComponent<Transform>().position = new Vector3(VRBounds.bounds.width, -wallHeight / 2f - 1f, 0f);
        _rightWall.GetComponent<Transform>().localScale = new Vector3(0.5f, wallHeight, VRBounds.bounds.length * 2);
    }

    IEnumerator StartChallenge()
    {
        _frontWall.GetComponent<Movement>().MoveTo(new Vector3(0f, wallHeight / 2f, VRBounds.bounds.length), 2f, true);
        _backWall.GetComponent<Movement>().MoveTo(new Vector3(0f, wallHeight / 2f, -VRBounds.bounds.length), 2f, true);
        _leftWall.GetComponent<Movement>().MoveTo(new Vector3(-VRBounds.bounds.width, wallHeight / 2f, 0f), 2f, true);
        _rightWall.GetComponent<Movement>().MoveTo(new Vector3(VRBounds.bounds.width, wallHeight / 2f, 0f), 2f, true);
        yield return new WaitForSeconds(2f);

        StartCoroutine("BallTimer");

    }

    IEnumerator BallTimer()
    {
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(ballTimeDifference);
        }
    }


    void RandomSpawn()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-VRBounds.bounds.length / 2, VRBounds.bounds.length / 2), 9f, Random.Range(-VRBounds.bounds.width / 2, VRBounds.bounds.width / 2));
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
