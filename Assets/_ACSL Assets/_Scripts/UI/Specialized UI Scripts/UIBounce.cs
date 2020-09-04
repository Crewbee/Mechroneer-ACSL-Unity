using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBounce : MonoBehaviour
{
    public bool scale = true;
    public Vector2 scaleRange = Vector2.one * 0.5f;
    public float scaleSpeed = 1;
    public bool rotation = true;
    public float rotateSpeed = 1;
    public float rotateRange = 45f;

    void Update()
    {
        if (scale)
        transform.localScale = Vector3.one * Mathf.SmoothStep(scaleRange.x, scaleRange.y, Mathf.PingPong(Time.time, 1) * scaleSpeed);

        if (rotation)
        transform.rotation = Quaternion.Euler(0, 0, rotateRange * Mathf.Sin(Time.time * rotateSpeed));
    }
}
