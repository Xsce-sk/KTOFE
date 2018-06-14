using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    // Public
	public float duration;
	public float height;

    // Private
    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _halfDuration;

    // Components
    private Transform objTransform;

	private void Start()
    {
        // Get Components
        objTransform = GetComponent<Transform>();

        // Get Members
        _startPos = objTransform.position;
        _endPos = new Vector3(_startPos.x, _startPos.y + height, _startPos.z);
        _halfDuration = duration / 2f;

        StartCoroutine("StartHover");
    }

	private IEnumerator StartHover()
    {
		while (true)
        {
            yield return MoveToPosition(_startPos, _endPos, _halfDuration);
            yield return MoveToPosition(_endPos, _startPos, _halfDuration);
        }
	}

	private IEnumerator MoveToPosition(Vector3 startPos, Vector3 endPos, float time)
    {
		float percentageTravelled = 0.0f;

		while (percentageTravelled <= 1.0f)
        {
			percentageTravelled += Time.deltaTime / time;
            objTransform.position = Vector3.Lerp (startPos, endPos, Mathf.SmoothStep (0.0f, 1.0f, percentageTravelled));
			yield return null;
		}
	}
}
