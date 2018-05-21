﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightChallenge : MonoBehaviour
{
    [Header("Wall Values")]
	public float wallHeight = 3f;
    public float wallWidth = 0.5f;

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

        float halfWidth = wallWidth / 2;

        _topWall.GetComponent<Transform> ().position = new Vector3 (0f, wallHeight + halfWidth, 0f);
		_topWall.GetComponent<Transform> ().localScale = new Vector3(VRBounds.bounds.length, wallWidth, VRBounds.bounds.width);

		_frontWall.GetComponent<Transform>().position = new Vector3(0f, wallHeight / 2f, VRBounds.bounds.width / 2f + halfWidth);
		_frontWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.length, wallHeight, wallWidth);

		_backWall.GetComponent<Transform>().position = new Vector3(0f, wallHeight / 2f, -VRBounds.bounds.width / 2f - halfWidth);
		_backWall.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.length, wallHeight, wallWidth);

		_leftWall.GetComponent<Transform>().position = new Vector3(-VRBounds.bounds.length / 2f - halfWidth, wallHeight / 2f, 0f);
		_leftWall.GetComponent<Transform>().localScale = new Vector3(wallWidth, wallHeight, VRBounds.bounds.width);

		_rightWall.GetComponent<Transform>().position = new Vector3(VRBounds.bounds.length / 2f + halfWidth, wallHeight / 2f, 0f);
		_rightWall.GetComponent<Transform>().localScale = new Vector3(wallWidth, wallHeight, VRBounds.bounds.width);
	}

	void Update () {
		
	}
}
