using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpVector3 : LerpBase
{
    private Vector3 _from;
    private Vector3 _to;

    public LerpVector3()
    {

    }
    
    public void Reset(Vector3 from, Vector3 to, float time, MyTimer.OnComplete function = null)
    {
        _from = from;
        _to = to;
        oldTimePassed = 0;
        timer.StartTimer(time, function);
    }

    public Vector3 GetAccumulated()
    {
        return Vector3.Lerp(_from, _to, timer.timePassed);
    }
    public Vector3 GetDelta()
    {
        return Vector3.Lerp(_from, _to, oldTimePassed) - Vector3.Lerp(_from, _to, timer.timePassed);
    }
}
