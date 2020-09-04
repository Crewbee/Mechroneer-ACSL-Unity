using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpVector2 : LerpBase
{
    private Vector2 _from;
    private Vector2 _to;
    public LerpVector2()
    {

    }

    public void Reset(Vector2 from, Vector2 to, float time, MyTimer.OnComplete function = null)
    {
        _from = from;
        _to = to;
        oldTimePassed = 0;
        timer.StartTimer(time, function);
    }


    public Vector2 GetAccumulated()
    {
        return Vector2.Lerp(_from, _to, timer.timePassed);
    }
    public Vector2 GetDelta()
    {
        return Vector2.Lerp(_from, _to, oldTimePassed) - Vector2.Lerp(_from, _to, timer.timePassed);
    }
}
