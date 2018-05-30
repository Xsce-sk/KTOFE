using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartButtonController : MonoBehaviour
{
    // Components
    private VRTK_InteractableObject interactableObject;

    // Use this for initialization
    void Start()
    {
        interactableObject = GetComponent<VRTK_InteractableObject>();
    }

    private void Update()
    {
        Debug.Log("Touching: " + interactableObject.IsTouched());
        Debug.Log("Using: " + interactableObject.IsUsing());
        if (interactableObject.IsUsing())
        {
            StartChallenges();
        }
    }

    void StartChallenges()
    {
        Debug.Log("Starting Game");
    }
}
