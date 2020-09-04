using UnityEngine;

[System.Serializable]
public class FollowCameraBehaviour : CameraBehaviour
{
    #region PUBLIC
    [Header("General Camera Settings")]
    public float CameraMaxY = 89f;
    public float CameraMinY = -89f;

    [Header("Mouse Settings")]
    public bool InvertVerticalAxis = false;
    public bool InvertHorizontalAxis = false;
    public float MouseSensitivityX = 2F;
    public float MouseSensitivityY = 2F;
    [Range(0, 1)]
    public float MouseSmoothing = 0.0f;

    [Header("Zoom Settings")]
    public float ZoomSensitivity = 2f;
    public bool InvertZoom = true;
    public float ZoomMin = 2f;
    public float ZoomMax = 10f;

    [Header("Look Settings")]
    public Vector3 m_LookOffset = Vector3.zero;

    [Header("Position Settings")]
    public Vector3 m_PositionOffset = Vector3.zero;
    [Range(0, 1)]
    public float PositionalDamping = 0.25f;

    [Header("Roll Settings")]
    public bool EnableRoll = true;
    [Range(0, 1)]
    public float RollDamping = 1.0f;
    [Range(-1, 1)]
    public float RollIntensity = -0.5f;

    [Header("Look Ahead Settings")]
    public bool EnableLookAhead = true;
    [Range(0, 1)]
    public float LookAheadDamping = 1.0f;
    [Range(-5, 5)]
    public float LookAheadIntensity = 1.5f;

    [Header("FOV Settings")]
    public bool EnableFOVScaling = true;
    [Range(0, 5)]
    public float FOVDamping = 2.5f;
    [Range(-1, 1)]
    public float FOVIntensity = 0.25f;
    [Tooltip("This must be higher than the Camera's FOV")]
    public float MaxFOV = 100f;

    [Header("Camera Collision Settings")]
    public bool EnableCameraCollision = true;
    public float ObstacleCheckRadius = 0.5f;
    public Vector3 PlayerLocalObstructionMovePos = Vector3.zero;
    #endregion

    #region PRIVATE
    // Damping
    private Vector2 m_MouseVelocity = Vector2.zero;
    private Vector3 m_PositionVelocity = Vector3.zero;
    private float m_RollVelocity = 0f;

    // Camera
    private Vector2 m_MouseInput = Vector2.zero;
    private Quaternion m_Rotation = Quaternion.identity;
    private float m_DesiredFOV = 0f;

    // Physics
    private Vector3 m_Velocity = Vector3.zero;
    private Vector3 m_LastVelocity = Vector3.zero;
    private int m_RaycastHitMask;
    #endregion

    #region ACCESSORS
    public Quaternion Rotation { get => m_Rotation; set => m_Rotation = value; }
    public Vector2 MouseInput { get => m_MouseInput; set => m_MouseInput = value; }
    #endregion

    public FollowCameraBehaviour()
    {
    }

    public override void Init(CameraDriver camera, Transform target)
    {
        base.Init(camera, target);

        m_RaycastHitMask = ~LayerMask.GetMask("Player", "IgnoreRaycast");
    }

    public override void Activate()
    {
        base.Activate();

        // If no player
        if (!m_Target)
        {
            Deactivate();
        }

        // Get Camera offset
        m_PositionOffset = m_Target.transform.position - m_CameraDriver.transform.position;

        // Get Camera FOV
        m_DesiredFOV = m_CameraDriver.GetComponent<Camera>().fieldOfView;
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public override void FixedUpdate()
    {
        // Calculate camera transform velocity
        m_LastVelocity = m_CameraDriver.transform.position;

        // Done in late update for render-time
        ApplyTranslation();
        ApplyRotation();

        // Prevent camera from clipping through objects
        if (EnableCameraCollision)
        {
            HandleCollision();
        }

        // Store current velocity
        m_Velocity = (m_CameraDriver.transform.position - m_LastVelocity) / Time.deltaTime;

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

        // Apply velocity based FOV scaling
        if (EnableFOVScaling)
        {
            ApplyFOV();
        }
    }

    private void ApplyTranslation()
    {
        // Orbit Camera & apply damping
        Quaternion rotation = Quaternion.Euler(m_MouseInput.x, m_MouseInput.y, 0);
        if (PositionalDamping > 0)
        {
            m_CameraDriver.transform.position = Vector3.SmoothDamp(m_CameraDriver.transform.position, m_Target.transform.position - (rotation * new Vector3(0, 0, m_PositionOffset.z)), ref m_PositionVelocity, PositionalDamping);
        }
        else
        {
            m_CameraDriver.transform.position = m_Target.transform.position - (rotation * m_PositionOffset);
        }
    }

    private void ApplyRotation()
    {
        // Ensure camera is always facing player
        m_CameraDriver.transform.LookAt(m_Target.transform.TransformPoint(m_LookOffset));
        //m_CameraDriver.transform.rotation = Quaternion.Euler(Mathf.Clamp(m_CameraDriver.transform.rotation.x, -89, 89), m_CameraDriver.transform.rotation.y, m_CameraDriver.transform.rotation.z);
    }

    private void ApplyRoll()
    {
        // Calculate smoothing of local X velocity
        float rollValue = EnableRoll ? Mathf.SmoothDamp(m_CameraDriver.transform.InverseTransformDirection(m_Velocity).x * RollIntensity, 0, ref m_RollVelocity, RollDamping) : 0;

        // Prevent small floating point math
        if (Mathf.Approximately(rollValue, 0))
        {
            rollValue = 0;
        }

        // Apply final roll value
        m_CameraDriver.transform.rotation = m_CameraDriver.transform.rotation * Quaternion.Euler(0, 0, rollValue);
    }

    private void ApplyLookAhead()
    {
        // Calculate smoothing of local velocities
        float verticalLookAheadValue = EnableLookAhead ? Mathf.SmoothDamp(m_CameraDriver.transform.InverseTransformDirection(m_Velocity).y * LookAheadIntensity, 0, ref m_RollVelocity, LookAheadDamping) : 0;
        float horizontalLookAheadValue = EnableLookAhead ? Mathf.SmoothDamp(m_CameraDriver.transform.InverseTransformDirection(m_Velocity).x * LookAheadIntensity, 0, ref m_RollVelocity, LookAheadDamping) : 0;

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
        m_CameraDriver.transform.rotation = Quaternion.Euler(m_CameraDriver.transform.rotation.eulerAngles + new Vector3(-verticalLookAheadValue, -horizontalLookAheadValue, 0));
    }

    private void ApplyFOV()
    {
        // Modify FOV based on forward velocity
        float fovValue = Mathf.Clamp((m_DesiredFOV + Mathf.Abs(m_CameraDriver.transform.InverseTransformDirection(m_Velocity).z) * FOVIntensity), m_DesiredFOV, MaxFOV);

        // Prevent small floating point math
        if (Mathf.Approximately(fovValue, 0))
        {
            fovValue = 0;
        }

        // Apply final fov value
        m_CameraDriver.GetComponent<Camera>().fieldOfView = Mathf.Lerp(m_CameraDriver.GetComponent<Camera>().fieldOfView, fovValue, Time.deltaTime);
        //m_CameraDriver.GetComponent<Camera>().fieldOfView = MathUtils.LerpTo(FOVDamping, m_CameraDriver.GetComponent<Camera>().fieldOfView, fovValue, Time.deltaTime);
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
        m_MouseInput.x += yaw * MouseSensitivityX * (InvertVerticalAxis ? -1 : 1);
        m_MouseInput.y += pitch * MouseSensitivityY * (InvertHorizontalAxis ? -1 : 1);
        m_PositionOffset.z += Input.GetAxisRaw("Mouse ScrollWheel") * ZoomSensitivity * (InvertZoom ? -1 : 1);

        // Clamp Y rotation
        m_MouseInput.x = Mathf.Clamp(m_MouseInput.x, CameraMinY, CameraMaxY);

        // Clamp zoom
        m_PositionOffset.z = Mathf.Clamp(m_PositionOffset.z, ZoomMin, ZoomMax);

        // Reset rotation (prevent rotation beyond 360)
        if (Mathf.Abs(m_MouseInput.y) > 360f)
        {
            m_MouseInput.y = 0f;
        }

        if (Mathf.Abs(m_MouseInput.x) > 360f)
        {
            m_MouseInput.x = 0f;
        }

        // Calculate mouse smoothing
        if (MouseSmoothing > 0)
        {
            float rotX = Mathf.SmoothDampAngle(m_CameraDriver.transform.rotation.eulerAngles.x, -m_MouseInput.x, ref m_MouseVelocity.x, MouseSmoothing);
            float rotY = Mathf.SmoothDampAngle(m_CameraDriver.transform.rotation.eulerAngles.y, m_MouseInput.y, ref m_MouseVelocity.y, MouseSmoothing);

            m_Rotation = Quaternion.Euler(rotX, rotY, 0);
        }
        else
        {
            m_Rotation = Quaternion.Euler(-m_MouseInput.x, m_MouseInput.y, 0);
        }

        //m_Rotation = Quaternion.Euler(m_Player.transform.TransformPoint(m_LookOffset) - m_CameraDriver.transform.position);
        //m_CameraDriver.transform.rotation.SetLookRotation(m_Rotation.eulerAngles);
        //m_CameraDriver.transform.LookAt(m_Player.transform.position + m_LookOffset);
    }

    public override bool UsesStandardControlRotation()
    {
        return false;
    }
}

