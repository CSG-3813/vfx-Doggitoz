/* 
 * Created by Coleton Wheeler
 * 11/2/2022
 * Class to create basic timer objects.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer
{
    private float time;
    private float timerStartValue;
    private bool isRunning;
    private float timerScale;

    public Timer(float timerDuration, bool isTimerRunning = true, float timerScale = 1)
    {
        this.timerStartValue = timerDuration;
        this.isRunning = isTimerRunning;
        this.timerScale = timerScale;
        RestartTimer();
    }

    public void Update(float deltaTime)
    {
        if (!isRunning || time == 0) return;

        this.time -= deltaTime * this.timerScale;
        this.time = Mathf.Clamp(this.time, 0, Mathf.Infinity);
        Debug.Log("Current timer: " + this.time);
    }

    public void RestartTimer()
    {
        this.time = this.timerStartValue;
    }

    public void Pause()
    {
        this.isRunning = false;
    }

    public void Stop()
    {
        this.isRunning = false;
    }

    public void Start()
    {
        this.isRunning = true;
    }

    public void SetDuration(float newDuration, bool restartTimer = true)
    {
        this.timerStartValue = newDuration;
        if (restartTimer) RestartTimer();
    }

    public bool IsRunning()
    {
        return this.isRunning;
    }

    public float RemainingTime()
    {
        return this.time;
    }

    public bool IsDone()
    {
        return (this.time == 0);
    }
}
