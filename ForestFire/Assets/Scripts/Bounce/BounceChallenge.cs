using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceChallenge : MonoBehaviour
{
    // Public Members
    public GameObject prefab;
    public float gravity;
    public float wallHeight;
    [Tooltip("Time between balls dropping")]
    public float duration;

    // Wall Objects
    private GameObject _frontWall;
    private GameObject _backWall;
    private GameObject _leftWall;
    private GameObject _rightWall;

    private void Start()
    {
        // Set Gravity
        Physics.gravity = new Vector3(0f, gravity, 0f);

        // Initialize Objects
        _frontWall = GameObject.Find("Front");
        _backWall = GameObject.Find("Back");
        _leftWall = GameObject.Find("Left");
        _rightWall = GameObject.Find("Right");

        _frontWall.GetComponent<Transform>().position = new Vector3(0f, wallHeight / 2f, VRBounds.bounds.length);
        _frontWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.width * 2f, wallHeight, 0.5f);

        _backWall.GetComponent<Transform>().position = new Vector3(0f, wallHeight / 2f, -VRBounds.bounds.length);
        _backWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.width * 2f, wallHeight, 0.5f);

        _leftWall.GetComponent<Transform>().position = new Vector3(-VRBounds.bounds.width, wallHeight / 2f, 0f);
        _leftWall.GetComponent<Transform>().localScale = new Vector3(0.5f, wallHeight, VRBounds.bounds.length * 2);

        _rightWall.GetComponent<Transform>().position = new Vector3(VRBounds.bounds.width, wallHeight / 2f, 0f);
        _rightWall.GetComponent<Transform>().localScale = new Vector3(0.5f, wallHeight, VRBounds.bounds.length * 2);

        StartCoroutine("BallTimer");
    }

    IEnumerator BallTimer()
    {
        while (true)
        {
            RandomSpawnLocation();
            yield return new WaitForSeconds(3f);
        }
    }


    void RandomSpawnLocation()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-VRBounds.bounds.length / 2, VRBounds.bounds.length / 2), 9f, Random.Range(-VRBounds.bounds.width / 2, VRBounds.bounds.width / 2));
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
