using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Ball Values")]
    public float defaultSize = 0.25f;
    public float maxSize = 1f;

    // Components
    private Transform _objTransform;

    void Start()
    {
        _objTransform = GetComponent<Transform>();

        float size = defaultSize;
        float sizeDifference = (maxSize - defaultSize);
        size += (GameManager.game.difficulty - 1) * (1f / 9f) * sizeDifference;

        _objTransform.localScale = new Vector3(size, size, size);
    }
}
