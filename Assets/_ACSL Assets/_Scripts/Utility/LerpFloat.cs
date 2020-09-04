using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFloat : LerpBase
{
    private float _from;
    private float _to;

    public LerpFloat()
    {

    }
    
    public void Reset(float from, float to, float time, MyTimer.OnComplete function = null)
    {
        _from = from;
        _to = to;
        oldTimePassed = 0;
        timer.StartTimer(time, function);
    }

    public float GetAccumulated()
    {
        return Mathf.Lerp(_from, _to, timer.timePassed);
    }
    public float GetDelta()
    {
        return Mathf.Lerp(_from, _to, oldTimePassed) - Mathf.Lerp(_from, _to, timer.timePassed);
    }
}
