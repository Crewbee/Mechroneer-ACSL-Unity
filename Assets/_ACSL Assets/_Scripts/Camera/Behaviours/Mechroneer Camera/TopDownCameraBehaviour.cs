using UnityEngine;

[System.Serializable]
public class TopDownCameraBehaviour : CameraBehaviour
{
    #region PUBLIC
    [Header("Input Settings")]
    public bool InvertHorizontalAxis = false;
    public float InputSensitivity = 1f;

    [Header("Zoom Settings")]
    public float ZoomSensitivity = 5f;
    public bool InvertZoom = false;
    public float ZoomMin = 5f;
    public float ZoomMax = 20f;

    [Header("Target Settings")]
    public Vector3 TargetOffset = Vector3.zero;

    [Header("Position Settings")]
    public Vector3 PositionOffset = Vector3.zero;
    public float CameraSpeed = 1.0f;
    #endregion

    #region PRIVATE
    // Camera
    public Vector2 m_Input = Vector2.zero;

    // Physics
    private Vector3 m_Velocity = Vector3.zero;
    private Vector3 m_LastFrameVelocity = Vector3.zero;
    #endregion

    public TopDownCameraBehaviour()
    {
    }

    public override void Init(CameraDriver camera, Transform target)
    {
        base.Init(camera, target);
    }

    public override void Activate()
    {
        base.Activate();

        // If no player
        if (!m_Target)
        {
            Deactivate();
        }

        // Look at target on first frame
        m_CameraDriver.transform.LookAt(m_Target.TransformPoint(TargetOffset));

        // Get Camera offset
        PositionOffset = m_Target.transform.position - m_CameraDriver.transform.position;
        PositionOffset.z = ZoomMax;

        // Reset Camera Velocity
        m_LastFrameVelocity = Vector3.zero;
        m_Velocity = Vector3.zero;

        // Assign initial angle
        //m_Input.x = m_CameraDriver.transform.rotation.eulerAngles.x;
        //m_Input.y = m_CameraDriver.transform.rotation.eulerAngles.y;
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public override void FixedUpdate()
    {
        // Store current velocity
        m_Velocity = (m_CameraDriver.transform.position - m_LastFrameVelocity) / Time.deltaTime;

        // Calculate camera transform velocity
        m_LastFrameVelocity = m_CameraDriver.transform.position;
    }

    public override void LateUpdate()
    {
        ApplyTranslation();
        ApplyRotation();
    }

    private void ApplyTranslation()
    {
        // Orbit Camera & apply damping
        if (CameraSpeed > 0)
        {
            m_CameraDriver.transform.position = Vector3.LerpUnclamped(m_CameraDriver.transform.position, m_Target.transform.TransformPoint(TargetOffset) + new Vector3(0, PositionOffset.z, 0), Time.deltaTime * CameraSpeed);
        }
        else
        {
            m_CameraDriver.transform.position = m_Target.transform.position + new Vector3(0, PositionOffset.z, 0);
        }
    }

    private void ApplyRotation()
    {
        // Ensure camera is always looking down
        m_CameraDriver.transform.rotation = Quaternion.LerpUnclamped(m_CameraDriver.transform.rotation, Quaternion.Euler(88, m_Input.y, 0), Time.deltaTime * CameraSpeed);
    }

    public override void SetFacingDirection(Vector3 lookDirection)
    {
        m_CameraDriver.transform.LookAt(lookDirection);
    }

    public override void UpdateRotation(float yaw, float pitch)
    {
        // Cache mouse input
        //m_Input.y -= pitch * InputSensitivity * (InvertHorizontalAxis ? -1 : 1);
        
        m_Input.y -= pitch * GameOptions.instance.m_CameraSensitivity * (InvertHorizontalAxis ? -1 : 1);

        ///DEBUG: Receive zoom input (bad change later)
        PositionOffset.z -= Input.GetAxisRaw("Mouse ScrollWheel") * ZoomSensitivity * (InvertZoom ? -1 : 1);

        // Clamp zoom
        PositionOffset.z = Mathf.Clamp(PositionOffset.z, ZoomMin, ZoomMax);

        // Reset rotation (prevent rotation beyond 360)
        if (Mathf.Abs(m_Input.y) > 360f)
        {
            m_Input.y = 0f;
        }
    }

    public override bool UsesStandardControlRotation()
    {
        return false;
    }
}

