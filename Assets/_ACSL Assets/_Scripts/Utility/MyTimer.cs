using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer
{
    public float timePassed { get => timePassedSeconds / _timeToComplete;}
    public float timePassedSeconds { get; protected set; }
    public float timeLeft { get => 1 - timePassed; }
    public float timeLeftSeconds { get =>  _timeToComplete - timePassedSeconds; }
    public bool active { get; protected set; }

    public delegate void OnComplete();

    protected OnComplete _onComplete;
    protected float _timeToComplete;

    public MyTimer()
    {
        active = false;
        timePassedSeconds = 0.0f;
        _timeToComplete = 0.0f;
    }

    public void StartTimer(float time)
    {
        active = true;
        timePassedSeconds = 0.0f;
        _timeToComplete = time;
        _onComplete = null;
    }

    public void StartTimer(float time, OnComplete function)
    {
        active = true;
        timePassedSeconds = 0.0f;
        _timeToComplete = time;
        _onComplete = function;
    }

    // Update is called once per frame
    public void Update()
    {
        if (active)
        {
            if (timePassedSeconds >= _timeToComplete)
            {
                active = false;
                timePassedSeconds = _timeToComplete;
                _onComplete?.Invoke();
            }
            timePassedSeconds += Time.deltaTime;
        }
    }

    public void StopTimer()
    {
        active = false;
        timePassedSeconds = 0;
    }
}
