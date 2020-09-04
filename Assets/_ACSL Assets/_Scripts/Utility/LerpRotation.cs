using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotation : MonoBehaviour
{
    LerpFloat _lerp = new LerpFloat();
    Vector3 _axis;
    Vector3 _pivot;

    // Update is called once per frame
    void Update()
    {
        if (_lerp.Active())
        {
            gameObject.transform.RotateAround(_pivot, _axis, _lerp.GetDelta());
        }
        _lerp.Update();
    }

    public void Lerp(Vector3 axis, Vector3 pivot, float amount, float time, MyTimer.OnComplete function = null)
    {
        if (_lerp.Active())
            return;

        _lerp.Reset(0, amount, time, function);
        _axis = axis;
        _pivot = pivot;
    }
}
