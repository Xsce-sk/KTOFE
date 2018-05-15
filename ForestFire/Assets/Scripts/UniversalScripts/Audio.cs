using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Audio : MonoBehaviour
{

    // Private Members
    private bool _showDebug = false;

    public void PlayClip(AudioClip clip)
    {
        AudioSource objAudioSource = gameObject.GetComponent<AudioSource>();
        objAudioSource.clip = clip;
        objAudioSource.Play();

        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=magenta>Audio</color></b></size>" + "\n";
            debugStatement += string.Format("<b><color=magenta>void PlayClip(AudioClip clip = {0})</color></b>", clip) + "\n";
            debugStatement += "<b><color=cyan>Object</color></b>: " + gameObject + "\n";
            debugStatement += "<b><color=cyan>clip</color></b>: " + clip + "\n";
            Debug.Log(debugStatement);
        }
    }

    public IEnumerator PlayMultipleClips(AudioClip[] clips, float gapTime = 0f, UnityAction callback = null)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            Debug.Log(gapTime);
            PlayClip(clips[i]);
            yield return new WaitForSeconds(clips[i].length);
            yield return new WaitForSeconds(gapTime);
        }

        callback();
    }
}
