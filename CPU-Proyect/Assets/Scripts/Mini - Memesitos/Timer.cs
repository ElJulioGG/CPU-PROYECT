using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOver;
    public GameObject gameWin;
    private TMP_Text _timerText;
    enum TimerType {Countdown, Stopwatch}
    [SerializeField] private TimerType timerType;
    [SerializeField] private Goal goal;
    [SerializeField] private float timeToDisplay = 60.0f;
    private bool _isRunning;

    private void Awake()
    {
       _timerText = GetComponent<TMP_Text>();
        player = GameObject.Find("Player");

    }

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdate;
        EventManager.TimerPause += EventManagerOnTimerPause;
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
        EventManager.TimerPause -= EventManagerOnTimerPause;
    }

    private void EventManagerOnTimerStart()
    {
        _isRunning = true;
    }

    private void EventManagerOnTimerStop()
    {
        _isRunning = false;
        //gameOver.SetActive(true);
        //GameObject.Destroy(player);
    }

    private void EventManagerOnTimerUpdate(float value)
    {
        timeToDisplay += value;
    }

    private void EventManagerOnTimerPause()
    {
        timeToDisplay = 9999f;
    }

    private void Update()
    {
        if (!_isRunning) return;
        if (timerType == TimerType.Countdown && timeToDisplay < 0.0f && goal.win == false)
        {
            EventManager.OnTimerStop();
            gameOver.SetActive(true);
            GameObject.Destroy(player);
            return;
            
            if (gameWin == true && timerType == TimerType.Countdown && timeToDisplay > 0.0f)
            {
                _isRunning = false; 
                //gameObject.SetActive(false);
            }
        }
        
        //if (gameWin == true && timerType == TimerType.Countdown && timeToDisplay > 0.0f)
        //{
        //    //timeToDisplay = 50.0f;
        //    gameObject.SetActive(false);
        //}

        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);

        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");

    }
}
