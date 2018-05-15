using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityVolume : MonoBehaviour
{
    private Vector3 _lastPos;
    private float _distance;

    void Start()
    {
        _lastPos = GetComponent<Transform>().localPosition;
        _distance = 0f;
    }

    void FixedUpdate()
    {
        _distance = Vector3.Distance(_lastPos, GetComponent<Transform>().transform.localPosition);
        // Debug.Log(distance / 300);
        GetComponent<AudioSource>().volume = _distance / 300f;
    }
}
