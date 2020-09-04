using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LerpBase
{
    protected MyTimer timer = new MyTimer();
    protected float oldTimePassed = 0.0f;
    public void Update()
    {
        oldTimePassed = timer.timePassed;
        timer.Update();
    }
    public bool Active()
    {
        return timer.active;
    }

    public void Stop()
    {
        timer.StopTimer();
    }
}
