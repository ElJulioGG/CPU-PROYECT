using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] public float activeMoveSpeed; //
    [SerializeField] public float moveSpeed; //
    public GameObject gameOver;
    private TMP_Text _timerText;
    enum TimerType {Countdown, Stopwatch}
    [SerializeField] private TimerType timerType;

    [SerializeField] private float timeToDisplay = 60.0f;

    private bool _isRunning;

    private void Awake()
    {
       _timerText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdate;
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
    }

    private void EventManagerOnTimerStart()
    {
        _isRunning = true;
    }

    private void EventManagerOnTimerStop() //
    {
        _isRunning = false;
        //gameOver.SetActive(true);
        activeMoveSpeed = 0f;
    }

    private void EventManagerOnTimerUpdate(float value)
    {
        timeToDisplay += value;
    }
    
    private void Update()
    {
        if (!_isRunning) return;
        if (timerType == TimerType.Countdown && timeToDisplay < 0.0f)
        {
            EventManager.OnTimerStop();
            return;
        }
        
        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);

        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");

    }
}
