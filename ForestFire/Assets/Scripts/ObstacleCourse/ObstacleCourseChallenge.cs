using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCourseChallenge : MonoBehaviour
{
    // Public Members
    [Header("Challenge Values")]
    public float defaultDistanceApart;
    public float minDistanceApart;
    public float defaultDurationApart;
    public float minDurationApart;

    // Wall Array
    private GameObject[] _walls;

    // Use this for initialization
    void Start ()
    {
        _walls = new GameObject[GameManager.game.difficulty];

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
