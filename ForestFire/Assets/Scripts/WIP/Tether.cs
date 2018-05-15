using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : MonoBehaviour
{

	// Public Members
	public GameObject _leftController;
	public GameObject _rightController;

	// Private Members
	private float _distance;
	private Transform _leftControllerTransform;
	private Transform _rightControllerTransform;


	// Use this for initialization
	void Start ()
	{
		_leftControllerTransform = _leftController.GetComponent<Transform> ();
		_rightControllerTransform = _rightController.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_distance = CalculateDistance ();
		VibrateController (_distance);
	}

	float CalculateDistance()
	{
		return 1.0f;
	}

	void VibrateController( float strength )
	{
		Debug.Log("Pass");
	}

}
