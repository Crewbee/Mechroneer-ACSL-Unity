using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpin : MonoBehaviour
{
    public float spinSpeed = 1f;
    void Update()
    {
        transform.Rotate(-Vector3.forward * Time.deltaTime * spinSpeed);
    }
}
