using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightChallenge : MonoBehaviour {

	public float wallHeight = 3f;

	// Wall Objects
	private GameObject _frontWall;
	private GameObject _backWall;
	private GameObject _leftWall;
	private GameObject _rightWall;
	private GameObject _topWall;

	void Start () {
		_frontWall = GameObject.Find("Front");
		_backWall = GameObject.Find("Back");
		_leftWall = GameObject.Find("Left");
		_rightWall = GameObject.Find("Right");
		_topWall = GameObject.Find("Top");

		_topWall.GetComponent<Transform> ().position = new Vector3 (0f, 0f, wallHeight);
		_topWall.GetComponent<Transform> ().localScale = new Vector3(VRBounds.bounds.width, 0.5f, VRBounds.bounds.length);

		_frontWall.GetComponent<Transform>().position = new Vector3(0f, wallHeight / 2f, VRBounds.bounds.length / 2f);
		_frontWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.width, wallHeight, 0.5f);

		_backWall.GetComponent<Transform>().position = new Vector3(0f, wallHeight / 2f, -VRBounds.bounds.length / 2f);
		_backWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.width, wallHeight, 0.5f);

		_leftWall.GetComponent<Transform>().position = new Vector3(-VRBounds.bounds.width / 2f, wallHeight / 2f, 0f);
		_leftWall.GetComponent<Transform>().localScale = new Vector3(0.5f, wallHeight, VRBounds.bounds.length);

		_rightWall.GetComponent<Transform>().position = new Vector3(VRBounds.bounds.width / 2f, wallHeight / 2f, 0f);
		_rightWall.GetComponent<Transform>().localScale = new Vector3(0.5f, wallHeight, VRBounds.bounds.length);
	}

	void Update () {
		
	}
}
