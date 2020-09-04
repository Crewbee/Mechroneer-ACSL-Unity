using UnityEngine;

[System.Serializable]
public class RobotCameraBehaviour : CameraBehaviour
{
    #region PUBLIC
    [Header("Camera Clamping Settings")]
    public Vector2 ClampInDegrees = new Vector2(-89, 89);

    [Header("Input Settings")]
    public bool InvertVerticalAxis = false;
    public bool InvertHorizontalAxis = false;
    public Vector2 InputSensitivity = Vector2.one;

    [Header("Zoom Settings")]
    public float ZoomSensitivity = 5f;
    public bool InvertZoom = false;
    public float ZoomMin = 1f;
    public float ZoomMax = 7.5f;

    [Header("Look Settings")]
    public Vector3 TargetOffset = Vector3.zero;
    public bool RigidLookAt = false;

    [Header("Position Settings")]
    public Vector3 PositionOffset = Vector3.zero;
    public float CameraSpeed = 1.0f;

    [Header("Roll Settings")]
    public bool EnableRoll = true;
    public float RollSpeed = 1.0f;
    public float RollIntensity = 1.0f;

    [Header("Look Ahead Settings")]
    public bool EnableLookAhead = true;
    public float LookAheadSpeed = 1.0f;
    public float LookAheadIntensity = 1.0f;

    [Header("Camera Collision Settings")]
    public bool EnableCameraCollision = true;
    public float ObstacleCheckRadius = 0.5f;
    public Vector3 PlayerLocalObstructionMovePos = Vector3.zero;
    #endregion

    #region PRIVATE
    // Camera
    public Vector2 m_Input = Vector2.zero;

    // Physics
    private Vector3 m_Velocity = Vector3.zero;
    private Vector3 m_LastFrameVelocity = Vector3.zero;
    private Vector3 m_SmoothVelocity = Vector3.zero;
    private int m_RaycastHitMask;
    private Vector3 shit;
    #endregion

    public RobotCameraBehaviour()
    {
    }

    public override void Init(CameraDriver camera, Transform target)
    {
        base.Init(camera, target);

        m_RaycastHitMask = ~LayerMask.GetMask("Robot", "IgnoreRaycast");
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

        //If Camera is moving
        if (m_Velocity.sqrMagnitude > Mathf.Epsilon)
        {
            // Prevent camera from clipping through objects
            if (EnableCameraCollision)
            {
                HandleCollision();
            }

            // Apply velocity based tilt
            if (EnableRoll)
            {
                ApplyRoll();
            }

            // Apply velocity based lookahead
            if (EnableLookAhead)
            {
                ApplyLookAhead();
            }
        }
    }

    private void ApplyTranslation()
    {
        // Orbit Camera & apply damping
        Quaternion rotation = Quaternion.Euler(m_Input.x, m_Input.y, 0);
        if (CameraSpeed > 0)
        {
            m_CameraDriver.transform.position = Vector3.Lerp(m_CameraDriver.transform.position, m_Target.transform.TransformPoint(TargetOffset) - (rotation * new Vector3(0, 0, PositionOffset.z)), Time.deltaTime * CameraSpeed);
        }
        else
        {
            m_CameraDriver.transform.position = m_Target.transform.position - (rotation * new Vector3(0, 0, PositionOffset.z));
        }
    }

    private void ApplyRotation()
    {
        // Ensure camera is always facing target
        if (RigidLookAt)
        {
            m_CameraDriver.transform.LookAt(m_Target.transform.TransformPoint(TargetOffset));
            return;
        }
        else
        {
            Quaternion targetDir = Quaternion.LookRotation(m_Target.transform.TransformPoint(TargetOffset) - m_CameraDriver.transform.position);
            m_CameraDriver.transform.rotation = Quaternion.Lerp(m_CameraDriver.transform.rotation, targetDir, Time.deltaTime * 100f);
            return;
        }
    }
    public void LeanCamera(float axis)
    {
        m_CameraDriver.transform.rotation = Quaternion.Lerp(m_CameraDriver.transform.rotation, Quaternion.Euler(m_CameraDriver.transform.rotation.x, m_CameraDriver.transform.rotation.y, axis * 15), Time.deltaTime * 1);
    }

    private void ApplyRoll()
    {
        // Calculate smoothing of local X velocity
        float rollValue = Mathf.Lerp(m_CameraDriver.transform.InverseTransformDirection(m_Velocity).x * RollIntensity, 0f, Time.deltaTime * RollSpeed);
        rollValue = Mathf.Clamp(rollValue, -15f, 15f);

        // Prevent small floating point math
        if (Mathf.Approximately(rollValue, 0))
        {
            rollValue = 0;
        }

        //Apply final roll value
        m_CameraDriver.transform.rotation *= Quaternion.Euler(0, 0, m_CameraDriver.transform.InverseTransformDirection(m_Velocity).x * RollIntensity);
    }

    private void ApplyLookAhead()
    {
        // Calculate smoothing of local velocities
        float verticalLookAheadValue = Mathf.SmoothStep(m_CameraDriver.transform.InverseTransformDirection(m_Velocity).y * LookAheadIntensity, 0f, Time.deltaTime * LookAheadSpeed);
        float horizontalLookAheadValue = Mathf.SmoothStep(m_CameraDriver.transform.InverseTransformDirection(m_Velocity).x * LookAheadIntensity, 0f, Time.deltaTime * LookAheadSpeed);

        // Prevent small floating point math
        if (Mathf.Approximately(verticalLookAheadValue, 0))
        {
            verticalLookAheadValue = 0;
        }

        if (Mathf.Approximately(horizontalLookAheadValue, 0))
        {
            horizontalLookAheadValue = 0;
        }

        // Apply final look-ahead values
        m_CameraDriver.transform.localRotation = Quaternion.Euler(Vector3.Lerp(m_CameraDriver.transform.localRotation.eulerAngles, m_CameraDriver.transform.localRotation.eulerAngles + new Vector3(-verticalLookAheadValue, horizontalLookAheadValue, 0), 1));
    }

    protected float HandleCollision()
    {
        // Define a ray from the player to the camera
        Vector3 rayStart = m_Target.transform.TransformPoint(PlayerLocalObstructionMovePos);
        Vector3 rayEnd = m_CameraDriver.transform.position;

        Vector3 rayDir = rayEnd - rayStart;

        float rayDist = rayDir.magnitude;

        if (rayDist <= 0f)
        {
            return 0f;
        }

        rayDir /= rayDist;

        RaycastHit[] hitInfos = Physics.SphereCastAll(rayStart, ObstacleCheckRadius, rayDir, rayDist, m_RaycastHitMask);
        if (hitInfos.Length <= 0)
        {
            return rayDist;
        }

        float minMoveUpDist = float.MaxValue;
        foreach (RaycastHit hitInfo in hitInfos)
        {
            minMoveUpDist = Mathf.Min(minMoveUpDist, hitInfo.distance);
        }

        if (minMoveUpDist < float.MaxValue)
        {
            m_CameraDriver.transform.position = rayStart + rayDir * minMoveUpDist;
        }

        return minMoveUpDist;
    }

    public override void SetFacingDirection(Vector3 lookDirection)
    {
        m_CameraDriver.transform.LookAt(lookDirection);
    }

    public override void UpdateRotation(float yaw, float pitch)
    {
        // Cache mouse input
        //m_Input.x += yaw * InputSensitivity.x * (InvertVerticalAxis ? -1 : 1);
        //m_Input.y -= pitch * InputSensitivity.y * (InvertHorizontalAxis ? -1 : 1);

        m_Input.x += yaw * GameOptions.instance.m_CameraSensitivity * (InvertVerticalAxis ? -1 : 1);
        m_Input.y -= pitch * GameOptions.instance.m_CameraSensitivity * (InvertHorizontalAxis ? -1 : 1);

        // Clamp X rotation
        m_Input.x = Mathf.Clamp(m_Input.x, ClampInDegrees.x, ClampInDegrees.y);

        // Reset rotation (prevent rotation beyond 360)
        if (Mathf.Abs(m_Input.y) > 360f)
        {
            m_Input.y = 0f;
        }

        if (Mathf.Abs(m_Input.x) > 360f)
        {
            m_Input.x = 0f;
        }
    }

    public override void UpdateZoom(float zoom)
    {
        ///DEBUG: Receive zoom input (bad change later)
        PositionOffset.z -= zoom * ZoomSensitivity * (InvertZoom ? -1 : 1);

        // Clamp zoom
        PositionOffset.z = Mathf.Clamp(PositionOffset.z, ZoomMin, ZoomMax);
    }

    public override bool UsesStandardControlRotation()
    {
        return false;
    }
}

