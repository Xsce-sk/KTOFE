using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WallClosingChallenge : MonoBehaviour
{
    // Public Members
    [Header("Challenge Values")]
    public float height = 3f;
    public float defaultGapSize = 1.5f;
    public float minGapSize = 0.5f;
    public float defaultDuration = 6f;
    public float minDuration = 1f;

    // Wall Values
    private GameObject _wallOne;
    private GameObject _wallTwo;
    private Vector3 _size;
    private float _offset;

    // Stored Positions
    private Vector3[] _initialPositions = new Vector3[2];

    // Components
    private Transform _wallOneTransform;
    private Transform _wallTwoTransform;
    private Movement _wallOneMovement;
    private Movement _wallTwoMovement;

    // Private Members
    private bool _showDebug = true;

    void Start()
    {
        // Initialize Components
        _wallOne = GameObject.Find("WallOne");
        _wallTwo = GameObject.Find("WallTwo");
        _wallOneTransform = _wallOne.GetComponent<Transform>();
        _wallTwoTransform = _wallTwo.GetComponent<Transform>();
        _wallOneMovement = _wallOne.GetComponent<Movement>();
        _wallTwoMovement = _wallTwo.GetComponent<Movement>();

        // Initialize Members
        string coroutineName = "";
        if (Random.Range(0, 2) == 0)
        {
            _offset = Random.Range(-VRBounds.bounds.width / 2f + 0.5f, VRBounds.bounds.width / 2f - 0.5f);
            ResetSidePositions();
            coroutineName = "StartSideChallenge";
        }
        else
        {
            _offset = Random.Range(-VRBounds.bounds.length / 2f + 0.5f, VRBounds.bounds.length / 2f - 0.5f);
            ResetFrontPositions();
            coroutineName = "StartFrontChallenge";
        }
        StartCoroutine(coroutineName);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>WallClosingChallenge</color></b></size>" + "\n";
            debugStatement += "<b><color=cyan>height</color></b>: " + height + "\n";
            debugStatement += "<b><color=cyan>minGapSize</color></b>: " + minGapSize + "\n";
            debugStatement += "<b><color=cyan>defaultDuration</color></b>: " + defaultDuration + "\n";
            debugStatement += "<b><color=cyan>minDuration</color></b>: " + minDuration + "\n";
            debugStatement += "<b><color=cyan>wallLeft</color></b>: " + _wallOne + "\n";
            debugStatement += "<b><color=cyan>wallRight</color></b>: " + _wallTwo + "\n";
            debugStatement += "<b><color=cyan>_offset</color></b>: " + _offset + "\n";
            debugStatement += "<b><color=cyan>coroutineName</color></b>: " + coroutineName + "\n";
            Debug.Log(debugStatement);
        }
    }

    void ResetSidePositions()
    {
        _size = new Vector3(0.5f, height, VRBounds.bounds.length * 3f);
        _wallOneTransform.localScale = _size;
        _wallTwoTransform.localScale = _size;

        float startDistance = VRBounds.bounds.width + 0.5f;
        float startHeight = -height / 2f - 1f;
        _initialPositions[0] = new Vector3(-startDistance + _offset, startHeight, 0f);
        _initialPositions[1] = new Vector3(startDistance + _offset, startHeight, 0f);
        _wallOneTransform.position = _initialPositions[0];
        _wallTwoTransform.position = _initialPositions[1];
    }

    void ResetFrontPositions()
    {
        _size = new Vector3(VRBounds.bounds.width * 3f, height, 0.5f);
        _wallOneTransform.localScale = _size;
        _wallTwoTransform.localScale = _size;

        float startDistance = VRBounds.bounds.length + 0.5f;
        float startHeight = -height / 2f - 1f;
        _initialPositions[0] = new Vector3(0f, startHeight, startDistance + _offset);
        _initialPositions[1] = new Vector3(0f, startHeight, -startDistance + _offset);
        _wallOneTransform.position = _initialPositions[0];
        _wallTwoTransform.position = _initialPositions[1];
    }

    IEnumerator StartSideChallenge()
    {
        yield return new WaitForSeconds(1f);

        float seconds = 2.5f;
        Rise(_wallOne, seconds);
        Rise(_wallTwo, seconds);
        yield return new WaitForSeconds(seconds);

        // Calc Duration
        seconds = defaultDuration;
        float durationDifference = (defaultDuration - minDuration);
        seconds -= (GameManager.game.speed - 1) * (1f / 9f) * durationDifference; // Subtract the percentage of the durationDifference

        // Calc Gap
        float gapSize = defaultGapSize;
        float gapDifference = defaultGapSize - minGapSize;
        gapSize -= (GameManager.game.difficulty - 1) * (1f / 9f) * gapDifference; // Subtract the percentage of the gapDifference

        SideContract(gapSize, seconds);
        yield return new WaitForSeconds(seconds);

        // Hold Position
        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>WallClosingChallenge.Hold()</color></b></size>" + "\n";
            debugStatement += "<b><color=magenta>Holding()</color></b>" + "\n";
            debugStatement += "<b><color=cyan>gapSize</color></b>: " + gapSize + "\n";
            debugStatement += "<b><color=cyan>seconds</color></b>: " + 1f + "\n";
            Debug.Log(debugStatement);
        }
        yield return new WaitForSeconds(1f);

        seconds = 5f;
        Expand(seconds);
        yield return new WaitForSeconds(seconds);

        seconds = 2.5f;
        Fall(_wallOne, seconds);
        Fall(_wallTwo, seconds);
        yield return new WaitForSeconds(seconds);

        EventManager.TriggerEvent("ChallengeCompleted");
    }

    IEnumerator StartFrontChallenge()
    {
        yield return new WaitForSeconds(1f);

        float seconds = 2.5f;
        Rise(_wallOne, seconds);
        Rise(_wallTwo, seconds);
        yield return new WaitForSeconds(seconds);

        // Calc Duration
        seconds = defaultDuration;
        float durationDifference = (defaultDuration - minDuration);
        seconds -= (GameManager.game.speed - 1) * (1f / 9f) * durationDifference; // Subtract the percentage of the durationDifference

        // Calc Gap
        float gapSize = defaultGapSize;
        float gapDifference = defaultGapSize - minGapSize;
        gapSize -= (GameManager.game.difficulty - 1) * (1f / 9f) * gapDifference; // Subtract the percentage of the gapDifference

        FrontContract(gapSize, seconds);
        yield return new WaitForSeconds(seconds);

        // Hold Position
        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>WallClosingChallenge.Hold()</color></b></size>" + "\n";
            debugStatement += "<b><color=magenta>Holding()</color></b>" + "\n";
            debugStatement += "<b><color=cyan>gapSize</color></b>: " + gapSize + "\n";
            debugStatement += "<b><color=cyan>seconds</color></b>: " + 1f + "\n";
            Debug.Log(debugStatement);
        }
        yield return new WaitForSeconds(1f);

        seconds = 5f;
        Expand(seconds);
        yield return new WaitForSeconds(seconds);

        seconds = 2.5f;
        Fall(_wallOne, seconds);
        Fall(_wallTwo, seconds);
        yield return new WaitForSeconds(seconds);

        EventManager.TriggerEvent("ChallengeCompleted");
    }

    void Rise(GameObject wall, float sec)
    {
        Vector3 startPos = wall.GetComponent<Transform>().position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y + height + 1f, startPos.z);
        float seconds = sec;
        bool ease = true;

        wall.GetComponent<Movement>().MoveTo(endPos, seconds, ease);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>WallClosingChallenge.Rise(GameObject wall, float sec)</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=cyan>void Rise(GameObject wall = {0}, float sec = {1})</color></b>", wall, sec) + "\n";
            debugStatement += "<b><color=cyan>startPos</color></b>: " + startPos + "\n";
            debugStatement += "<b><color=cyan>endPos</color></b>: " + endPos + "\n";
            debugStatement += "<b><color=cyan>seconds</color></b>: " + seconds + "\n";
            debugStatement += "<b><color=cyan>ease</color></b>: " + ease + "\n";
            Debug.Log(debugStatement);
        }
    }

    void SideContract(float gapSize, float sec)
    {
        Vector3 startPosL = _wallOneTransform.position;
        Vector3 endPosL = new Vector3(-gapSize / 2f - _size.x / 2f + _offset, startPosL.y, startPosL.z);
        float secondsL = sec;
        bool easeL = false;

        _wallOneMovement.MoveTo(endPosL, secondsL, easeL);

        Vector3 startPosR = _wallTwoTransform.position;
        Vector3 endPosR = new Vector3(gapSize / 2f + _size.x / 2f + _offset, startPosR.y, startPosR.z);
        float secondsR = sec;
        bool easeR = false;

        _wallTwoMovement.MoveTo(endPosR, secondsR, easeR);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>WallClosingChallenge.Contract(float gapSize, float sec)</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=cyan>void Contract(float gapSize = {0}, float sec = {1})</color></b>", gapSize, sec) + "\n";
            debugStatement += "<b><color=cyan>(left) startPos</color></b>: " + startPosL + "\n";
            debugStatement += "<b><color=cyan>(left) endPos</color></b>: " + endPosL + "\n";
            debugStatement += "<b><color=cyan>(left) seconds</color></b>: " + secondsL + "\n";
            debugStatement += "<b><color=cyan>(left) ease</color></b>: " + easeL + "\n";
            debugStatement += "\n";
            debugStatement += "<b><color=cyan>(right) startPos</color></b>: " + startPosR + "\n";
            debugStatement += "<b><color=cyan>(right) endPos</color></b>: " + endPosR + "\n";
            debugStatement += "<b><color=cyan>(right) seconds</color></b>: " + secondsR + "\n";
            debugStatement += "<b><color=cyan>(right) ease</color></b>: " + easeR + "\n";
            Debug.Log(debugStatement);
        }
    }

    void FrontContract(float gapSize, float sec)
    {
        Vector3 startPosF = _wallOneTransform.position;
        Vector3 endPosL = new Vector3(startPosF.x, startPosF.y, gapSize / 2f + _size.z / 2f + _offset);
        float secondsL = sec;
        bool easeL = false;

        _wallOneMovement.MoveTo(endPosL, secondsL, easeL);

        Vector3 startPosB = _wallTwoTransform.position;
        Vector3 endPosR = new Vector3(startPosB.x, startPosB.y, -gapSize / 2f - _size.z / 2f + _offset);
        float secondsR = sec;
        bool easeR = false;

        _wallTwoMovement.MoveTo(endPosR, secondsR, easeR);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>WallClosingChallenge.Contract(float gapSize, float sec)</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=cyan>void Contract(float gapSize = {0}, float sec = {1})</color></b>", gapSize, sec) + "\n";
            debugStatement += "<b><color=cyan>(front) startPos</color></b>: " + startPosF + "\n";
            debugStatement += "<b><color=cyan>(front) endPos</color></b>: " + endPosL + "\n";
            debugStatement += "<b><color=cyan>(front) seconds</color></b>: " + secondsL + "\n";
            debugStatement += "<b><color=cyan>(front) ease</color></b>: " + easeL + "\n";
            debugStatement += "\n";
            debugStatement += "<b><color=cyan>(back) startPos</color></b>: " + startPosB + "\n";
            debugStatement += "<b><color=cyan>(back) endPos</color></b>: " + endPosR + "\n";
            debugStatement += "<b><color=cyan>(back) seconds</color></b>: " + secondsR + "\n";
            debugStatement += "<b><color=cyan>(back) ease</color></b>: " + easeR + "\n";
            Debug.Log(debugStatement);
        }
    }

    void Expand(float sec)
    {
        Vector3 endPosL = new Vector3(_initialPositions[0].x, _initialPositions[0].y + height + 1f, _initialPositions[0].z);
        float secondsL = sec;
        bool easeL = true;

        _wallOneMovement.MoveTo(endPosL, secondsL, easeL);

        Vector3 endPosR = new Vector3(_initialPositions[1].x, _initialPositions[1].y + height + 1f, _initialPositions[1].z);
        float secondsR = sec;
        bool easeR = true;

        _wallTwoMovement.MoveTo(endPosR, secondsR, easeR);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>WallClosingChallenge.Expand(float sec)</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=cyan>void Expand(float sec = {0})</color></b>", sec) + "\n";
            debugStatement += "<b><color=cyan>endPos</color></b>: " + endPosL + "\n";
            debugStatement += "<b><color=cyan>seconds</color></b>: " + secondsL + "\n";
            debugStatement += "<b><color=cyan>ease</color></b>: " + easeL + "\n";
            debugStatement += "\n";
            debugStatement += "<b><color=cyan>endPos</color></b>: " + endPosR + "\n";
            debugStatement += "<b><color=cyan>seconds</color></b>: " + secondsR + "\n";
            debugStatement += "<b><color=cyan>ease</color></b>: " + easeR + "\n";
            Debug.Log(debugStatement);
        }
    }

    void Fall(GameObject wall, float sec)
    {
        Vector3 startPos = wall.GetComponent<Transform>().position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y - height - 1f, startPos.z);
        float seconds = sec;
        bool ease = true;

        wall.GetComponent<Movement>().MoveTo(endPos, seconds, ease);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>WallClosingChallenge.Fall(GameObject wall, float sec)</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=cyan>void Fall(GameObject wall = {0}, float sec = {1})</color></b>", wall, sec) + "\n";
            debugStatement += "<b><color=cyan>startPos</color></b>: " + startPos + "\n";
            debugStatement += "<b><color=cyan>endPos</color></b>: " + endPos + "\n";
            debugStatement += "<b><color=cyan>seconds</color></b>: " + seconds + "\n";
            debugStatement += "<b><color=cyan>ease</color></b>: " + ease + "\n";
            Debug.Log(debugStatement);
        }
    }
}