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

    [Header("Prefabs")]
    public GameObject[] wallPrefabs;

    // Wall Array
    private List<GameObject> _walls;

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < GameManager.game.difficulty + 2; i++)
        {
            GameObject wallPrefab = wallPrefabs[Random.Range(0, wallPrefabs.Length)];
            GameObject wall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
            //Vector3 wallPos = new Vector3(0f, 0f, (i + 1) * 3f);
            //_walls.Add();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
