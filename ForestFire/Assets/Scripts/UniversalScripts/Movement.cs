using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Private Members
    private bool _showDebug = false;

    public void MoveTo(Vector3 endPos, float seconds, bool ease)
    { 
        object[] parms = new object[4];
        parms[0] = endPos;
        parms[1] = seconds;
        parms[2] = ease;

        StartCoroutine("MoveToEnumerator", parms);

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>Movement</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=magenta>void MoveTo(Vector3 endPos = {0}, float seconds = {1}, bool ease = {2})</color></b>", endPos, seconds, ease) + "\n";
            debugStatement += "<b><color=cyan>Object</color></b>: " + gameObject + "\n";
            debugStatement += "<b><color=cyan>startPos</color></b>: " + gameObject.GetComponent<Transform>().position + "\n";
            debugStatement += "<b><color=cyan>endPos</color></b>: " + endPos + "\n";
            debugStatement += "<b><color=cyan>seconds</color></b>: " + seconds + "\n";
            debugStatement += "<b><color=cyan>ease</color></b>: " + ease + "\n";
            Debug.Log(debugStatement);
        }
    }

    IEnumerator MoveToEnumerator(object[] parms)
    {
        Transform objTransform = gameObject.GetComponent<Transform>();
        Vector3 startPos = objTransform.position;
        Vector3 endPos = (Vector3)parms[0];
        float seconds = (float)parms[1];
        bool ease = (bool)parms[2];
        float step = 0f;
        float t = 0f;

        while (t <= 1f)
        {
            t = t + Time.deltaTime / seconds;
            if (ease)
            {
                step = Mathf.SmoothStep(0f, 1f, t);
            }
            else
            { 
                step = t;
            }
            objTransform.position = Vector3.Lerp(startPos, endPos, step);
            yield return null;
        }
    }
}
