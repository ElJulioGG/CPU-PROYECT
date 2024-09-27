using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float bpm = 120f; // Beats per minute
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private BeatInterval[] beatIntervals;

    private void Update()
    {
        float currentSongTime = audioSource.time; // Time in seconds
        foreach (BeatInterval beatInterval in beatIntervals)
        {
            beatInterval.CheckForBeat(currentSongTime, bpm);
        }
    }
}

[System.Serializable]
public class BeatInterval
{
    [SerializeField] private float intervalMultiplier = 1f; // How many beats per interval (e.g., 1 = every beat, 0.5 = every half-beat)
    [SerializeField] private UnityEvent onBeatTriggered;

    private float lastTriggerTime = -1f;

    public void CheckForBeat(float songTime, float bpm)
    {
        float intervalLength = 60f / (bpm * intervalMultiplier);
        int currentBeatIndex = Mathf.FloorToInt(songTime / intervalLength);

        if (currentBeatIndex != lastTriggerTime)
        {
            lastTriggerTime = currentBeatIndex;
            onBeatTriggered?.Invoke(); // Trigger the event
        }
    }
}
