using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightChallenge : MonoBehaviour
{
    [Header("Wall Values")]
	public float wallHeight = 3f;
    public float wallWidth = 0.5f;

    [Header("Timer Values")]
    public float defaultTime = 20f;
    public float minTime = 10f;
    public GameObject timerText;

    [Header("Target Values")]
    public float defaultScale = 0.4f;
    public float minScale = 0.2f;

    [Header("Materials")]
	public Material red;
	public Material green;

    [Header("Controllers")]
    public GameObject leftCont;
	public GameObject rightCont;

    // Private Memebers
	private GameObject _target;
    private float _timer;
    private bool _casting = true;
    private bool _leftGreen = false;
    private bool _rightGreen = false;

    // Object Components
    private Text _timerTextComponent;

	// Wall Objects
	private GameObject _frontWall;
	private GameObject _backWall;
	private GameObject _leftWall;
	private GameObject _rightWall;
	private GameObject _topWall;

	void Start ()
    {
        // Get Wall Objects
		_frontWall = GameObject.Find("Front");
		_backWall = GameObject.Find("Back");
		_leftWall = GameObject.Find("Left");
		_rightWall = GameObject.Find("Right");
		_topWall = GameObject.Find("Top");
        SetWallPositions();

        // Get Text Component
        timerText.SetActive(true);
        _timerTextComponent = timerText.GetComponent<Text>();

        // Target Functions
        _target = GameObject.Find("target");
        SetTargetScale();
        SetTargetPosition();
        

        // Set Timer
        _timer = defaultTime;
        float timeDifference = (defaultTime - minTime);
        _timer -= (GameManager.game.speed - 1) * (1f / 9f) * timeDifference;
	}

    void SetTargetScale()
    {
        float targetScale = defaultScale;
        float scaleDifference = (defaultScale - minScale);
        targetScale -= (GameManager.game.speed - 1) * (1f / 9f) * scaleDifference;
        _target.GetComponent<Transform>().localScale = new Vector3(targetScale, targetScale, targetScale);
    }

    void SetTargetPosition()
    {
        float randX = Random.Range(-VRBounds.bounds.length / 2 + (VRBounds.bounds.length * 0.2f), VRBounds.bounds.length / 2 - (VRBounds.bounds.length * 0.2f));
        float randY = Random.Range(0f + (wallHeight * 0.2f), wallHeight - (wallHeight * 0.2f));
        float randZ = Random.Range(-VRBounds.bounds.width / 2 + (VRBounds.bounds.width * 0.2f), VRBounds.bounds.width / 2 - (VRBounds.bounds.width * 0.2f));

        int randNum = Random.Range(0, 2);
        switch (randNum)
        {
            case 0:
                _target.GetComponent<Transform>().Rotate(new Vector3(0f, 90f, 0f));
                if (Random.Range(0, 1) == 0)
                    randX = -VRBounds.bounds.length / 2;
                else
                    randX = VRBounds.bounds.length / 2;
                break;
            case 1:
                if (Random.Range(0, 1) == 0)
                    randY = 0;
                else
                    randY = wallHeight;
                break;
            case 2:
                if (Random.Range(0, 1) == 0)
                    randZ = -VRBounds.bounds.width / 2;
                else
                    randZ = VRBounds.bounds.width / 2;
                break;
        }

        _target.GetComponent<Transform>().position = new Vector3(randX, randY, randZ);
    }

    void SetWallPositions()
    {
        float halfWidth = wallWidth / 2;

        _topWall.GetComponent<Transform>().position = new Vector3(0f, wallHeight + halfWidth, 0f);
        _topWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.length, wallWidth, VRBounds.bounds.width);

        _frontWall.GetComponent<Transform>().position = new Vector3(0f, wallHeight / 2f, VRBounds.bounds.width / 2f + halfWidth);
        _frontWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.length, wallHeight, wallWidth);

        _backWall.GetComponent<Transform>().position = new Vector3(0f, wallHeight / 2f, -VRBounds.bounds.width / 2f - halfWidth);
        _backWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.length, wallHeight, wallWidth);

        _leftWall.GetComponent<Transform>().position = new Vector3(-VRBounds.bounds.length / 2f - halfWidth, wallHeight / 2f, 0f);
        _leftWall.GetComponent<Transform>().localScale = new Vector3(wallWidth, wallHeight, VRBounds.bounds.width);

        _rightWall.GetComponent<Transform>().position = new Vector3(VRBounds.bounds.length / 2f + halfWidth, wallHeight / 2f, 0f);
        _rightWall.GetComponent<Transform>().localScale = new Vector3(wallWidth, wallHeight, VRBounds.bounds.width);
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

    void Update ()
    {
        if (_casting)
        {
            castRays();

            if (CountDown(Time.deltaTime))
            {
                SetTimerDisplay(0f);
                EventManager.TriggerEvent("Death");
            }
        }
	}

    void castRays()
    {
        RaycastHit hit1;
        RaycastHit hit2;

        Debug.DrawRay(leftCont.transform.position, leftCont.transform.TransformDirection(Vector3.forward), Color.green);
        Debug.DrawRay(rightCont.transform.position, rightCont.transform.TransformDirection(Vector3.forward), Color.green);

        if (Physics.Raycast(leftCont.transform.position, leftCont.transform.TransformDirection(Vector3.forward), out hit1))
        {
            if (hit1.transform.tag == "Target")
            {
                _target.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = green;
                
                if (!_leftGreen)
                {
                    StartCoroutine("PulseLeft");
                }
                _leftGreen = true;
            }
            else
            {
                _target.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = red;
                _leftGreen = false;
                StopCoroutine("PulseLeft");
            }
        }

        if (Physics.Raycast(rightCont.transform.position, rightCont.transform.TransformDirection(Vector3.forward), out hit2))
        {
            if (hit2.transform.tag == "Target")
            {
                _target.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = green;
                if (!_rightGreen)
                {
                    StartCoroutine("PulseRight");
                }
                _rightGreen = true;
            }
            else
            {
                _target.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = red;
                _rightGreen = false;
                StopCoroutine("PulseRight");
            }
        }

        if (_leftGreen && _rightGreen)
        {
            _casting = false;
            StartCoroutine("WinChallenge");
        }
    }

    IEnumerator WinChallenge()
    {
        yield return new WaitForSeconds(1f);
        EventManager.TriggerEvent("ChallengeCompleted");
    }

    IEnumerator PulseLeft()
    {
        while (true)
        {
            EventManager.TriggerEvent("PulseLeft");
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator PulseRight()
    {
        while (true)
        {
            EventManager.TriggerEvent("PulseRight");
            yield return new WaitForSeconds(0.2f);
        }
    }
}