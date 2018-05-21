using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCourseChallenge : MonoBehaviour
{
    // Public Members
    [Header("Challenge Values")]
    public float distanceApart;
    public float defaultDuration;
    public float minDuration;
    public float distanceBelowFloor;

    [Header("Prefabs")]
    public GameObject[] wallPrefabs;

    // Wall Array
    private List<GameObject> _walls = new List<GameObject>();

    // Private Members
    private bool _showDebug = true;

    // Use this for initialization
    void Start ()
    {
        // For creating a unique wall prefab
        int lastWallNum = 0;
        int wallNum = 0;

        for (int i = 0; i < GameManager.game.difficulty + 2; i++)
        {
            // Create wall that is not the same as the one before it in the list
            while (wallNum == lastWallNum)
            {
                wallNum = Random.Range(0, wallPrefabs.Length);
            }
            lastWallNum = wallNum;

            // Creating the wall
            GameObject wallPrefab = wallPrefabs[wallNum];
            GameObject wall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity).gameObject;

            wall.GetComponent<Transform>().position = new Vector3(0f, -distanceBelowFloor, (i + 1) * distanceApart);

            // Adding wall to list
            _walls.Add(wall);
        }

        StartCoroutine("StartChallenge");

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>ObstacleCourseChallenge</color></b></size>" + "\n";
            for (int i = 0; i < _walls.Count; i++)
            {
                debugStatement += string.Format("<b><color=cyan>Wall [{0}]</color></b>", i + 1);
                debugStatement += "\t" + "<b><color=cyan>wall</color></b>: " + _walls[i] + "\n";
                debugStatement += "\t" + "<b><color=cyan>position</color></b>: " + _walls[i].transform.position + "\n";
            }
            Debug.Log(debugStatement);
        }
    }

    IEnumerator StartChallenge()
    {
        yield return new WaitForSeconds(1f);

        float seconds = 2.5f;
        Rise(seconds);
        yield return new WaitForSeconds(seconds);

        // Calc Duration
        seconds = defaultDuration * _walls.Count;
        float durationDifference = (defaultDuration * _walls.Count) - (minDuration * _walls.Count);
        seconds -= (GameManager.game.speed - 1) * (1f / 9f) * durationDifference; // Subtract the percentage of the durationDifference

        Approach(seconds);
        yield return new WaitForSeconds(seconds);

        seconds = 2.5f;
        Fall(seconds);
        yield return new WaitForSeconds(seconds);

        EventManager.TriggerEvent("ChallengeCompleted");
    }

    void Rise(float sec)
    {
        for (int i = 0; i < _walls.Count; i++)
        {
            Vector3 startPos = _walls[i].GetComponent<Transform>().position;
            Vector3 endPos = new Vector3(startPos.x, startPos.y + distanceBelowFloor, startPos.z);
            float seconds = sec;
            bool ease = true;
            _walls[i].gameObject.GetComponent<Movement>().MoveTo(endPos, seconds, ease);
        }

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>ObstacleCourseChallenge.Rise(float sec)</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=cyan>void Rise(float sec = {0})</color></b>", sec) + "\n";
            debugStatement += "<b><color=cyan>riseDistance</color></b>: " + distanceBelowFloor + "\n";
            Debug.Log(debugStatement);
        }
    }

    void Approach(float sec)
    {
        // Calc Distance
        float distance = distanceApart * (_walls.Count + 1);

        for (int i = 0; i < _walls.Count; i++)
        {
            Vector3 startPos = _walls[i].GetComponent<Transform>().position;
            Vector3 endPos = new Vector3(startPos.x, startPos.y, startPos.z - distance);
            float seconds = sec;
            bool ease = true;
            _walls[i].gameObject.GetComponent<Movement>().MoveTo(endPos, seconds, ease);
        }

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>ObstacleCourseChallenge.Approach(float sec)</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=cyan>void Approach(float sec = {0})</color></b>", sec) + "\n";
            debugStatement += "<b><color=cyan>distance</color></b>: " + distance + "\n";
            Debug.Log(debugStatement);
        }
    }

    void Fall(float sec)
    {
        for (int i = 0; i < _walls.Count; i++)
        {
            Vector3 startPos = _walls[i].GetComponent<Transform>().position;
            Vector3 endPos = new Vector3(startPos.x, startPos.y - distanceBelowFloor, startPos.z);
            float seconds = sec;
            bool ease = true;
            _walls[i].gameObject.GetComponent<Movement>().MoveTo(endPos, seconds, ease);
        }

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>ObstacleCourseChallenge.Fall(float sec)</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=cyan>void Fall(float sec = {0})</color></b>", sec) + "\n";
            debugStatement += "<b><color=cyan>fallDistance</color></b>: " + distanceBelowFloor + "\n";
            Debug.Log(debugStatement);
        }
    }
}
