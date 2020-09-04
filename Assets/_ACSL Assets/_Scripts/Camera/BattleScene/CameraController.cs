using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject TargetObject;
    Transform m_Target;
    void Start()
    {
        m_Target = TargetObject.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(m_Target);
    }
}
