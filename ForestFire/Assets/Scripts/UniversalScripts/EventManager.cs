using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    // Object Reference
	private Dictionary <string, UnityEvent> eventDictionary;
	private static EventManager eventManager;

    // Private Members
    private static bool _showDebug = false;

    public static EventManager instance
	{
		get
		{ 
			if (!eventManager)
			{
				eventManager = FindObjectOfType (typeof(EventManager)) as EventManager;

				if (!eventManager)
				{
					Debug.LogError ("There needs to be one active EventManager script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init ();
				}
			}
            DontDestroyOnLoad(eventManager);
            return eventManager;
		}
	}

    void Init()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, UnityEvent> ();
		}
	}

    private void Start()
    {
        StartCoroutine("ShowDictionary");
    }

    IEnumerator ShowDictionary()
    {
        yield return new WaitForSeconds(1f);
        DisplayEvents();
    }

    public static void StartListening(string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
			thisEvent.AddListener (listener);
		}
		else
		{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener)
	{
		if (eventManager == null) return;
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent(string eventName)
	{
        UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke ();
            if (_showDebug)
            {
                string debugStatement = "<size=22><b><color=magenta>EventManager.TriggerEvent(string eventName)</color></b></size>" + "\n";
                debugStatement += string.Format("<b><color=magenta>void TriggerEvent(string eventName = {0})</color></b>", eventName) + "\n";
                debugStatement += "<b><color=cyan>Triggered</color></b>: " + instance.eventDictionary.TryGetValue(eventName, out thisEvent) + "\n";
                Debug.Log(debugStatement);
            }
        }
	}

    void DisplayEvents()
    {
        if (_showDebug)
        {
            string debugStatement = "<size=22><b><color=black>EventManager</color></b></size>" + "\n";
            foreach(KeyValuePair<string, UnityEvent> entry in eventDictionary)
            {
                debugStatement += "<b><color=cyan>EventName</color>: </b>" + entry.Key + "\n";
            }
            Debug.Log(debugStatement);
        }
    }

}