using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Private Members
    private GameObject playButton;

	void Start ()
    {
        playButton = GameObject.Find("PlayButton");
        playButton.GetComponent<Transform>().position = new Vector3(0f, 1f, VRBounds.bounds.width / 2.5f);
        playButton.GetComponent<Transform>().localScale = new Vector3(VRBounds.bounds.length - 0.1f, 0.3f, 0.25f);
    }

	void Update ()
    {
		
	}
}
