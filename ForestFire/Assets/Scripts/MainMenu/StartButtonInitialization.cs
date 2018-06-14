using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonInitialization : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(0f, 1.5f, (VRBounds.bounds.width / 2f) - 0.5f);
    }
}
