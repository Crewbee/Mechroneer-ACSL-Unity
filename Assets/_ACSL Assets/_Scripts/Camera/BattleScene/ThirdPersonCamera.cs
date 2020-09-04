using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform LookAt;
    public Transform CamTransform;

    private Camera Cam;

    private float Distance = 5.0f;
    private float OffsetY = 2.0f;
    private float CurrentX = 0.0f;
    private float CurrentY = 0.0f;
    private float SensitivityX = 4.0f;
    private float SensitivityY = 1.0f;

    private void Start()
    {
        CamTransform = transform;
        Cam = Camera.main;
    }

    private void Update()
    {
        CurrentX += Input.GetAxis("Mouse X");
        CurrentY += Input.GetAxis("Mouse Y");

        CurrentY = Mathf.Clamp(CurrentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, OffsetY, -Distance);

        Quaternion rotation = Quaternion.Euler(CurrentY * SensitivityY, CurrentX * SensitivityX, 0);

        CamTransform.position = LookAt.position + rotation * dir;

        CamTransform.LookAt(LookAt.position);
    }
}
