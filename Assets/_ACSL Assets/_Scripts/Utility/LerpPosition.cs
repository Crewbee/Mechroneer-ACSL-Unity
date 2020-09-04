using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    LerpVector3 _lerp = new LerpVector3();

    // Update is called once per frame
    void Update()
    {
        if (_lerp.Active())
        {
            gameObject.transform.position = _lerp.GetAccumulated();
        }
        _lerp.Update();
    }

    public void Lerp(Vector3 from, Vector3 to, float time, MyTimer.OnComplete function = null)
    {
        if (_lerp.Active())
            return;
        _lerp.Reset(from, to, time, function);
    }
}
