using UnityEngine;

[System.Serializable]
public class FPSCameraBehaviour : CameraBehaviour
{
    #region PUBLIC
    [Header("General Camera Settings")]
    public float CameraMaxY = 90f;
    public float CameraMinY = -90f;

    [Header("Mouse Settings")]
    public bool InvertVerticalAxis = false;
    public bool InvertHorizontalAxis = false;
    public float MouseSensitivityX = 2F;
    public float MouseSensitivityY = 2F;
    [Range(0, 1)]
    public float MouseSmoothing = 0.0f;

    [Header("Position Settings")]
    public Vector3 m_PositionOffset = Vector3.zero;

    [Header("Roll Settings")]
    public bool EnableRoll = true;
    [Range(0, 1)]
    public float RollDamping = 1.0f;
    [Range(-1, 1)]
    public float RollIntensity = 0.5f;

    [Header("Look Ahead Settings")]
    public bool EnableLookAhead = true;
    [Range(0, 1)]
    public float LookAheadDamping = 1.0f;
    public Vector2 LookAheadIntensity = new Vector2(0.25f, 1f);

    [Header("FOV Settings")]
    public bool EnableFOVScaling = true;
    [Range(0, 5)]
    public float FOVDamping = 2.5f;
    [Range(0, 5)]
    public float FOVIntensity = 2.0f;
    [Tooltip("This must be higher than the Camera's FOV")]
    public float MaxFOV = 100f;
    #endregion

    #region PRIVATE
    // Damping
    private Vector2 m_MouseVelocity = Vector2.zero;
    private float m_RollVelocity = 0f;
    private Vector2 m_LookAheadVelocity = Vector2.zero;

    // Camera
    private Vector2 m_MouseInput = Vector2.zero;
    private Quaternion m_Rotation = Quaternion.identity;
    private float m_DesiredFOV = 0f;

    // Physics
    private Vector3 m_Velocity = Vector3.zero;
    private Vector3 m_LastVelocity = Vector3.zero;
    #endregion

    #region ACCESSORS
    public Quaternion Rotation { get => m_Rotation; set => m_Rotation = value; }
    public Vector2 MouseInput { get => m_MouseInput; set => m_MouseInput = value; }
    #endregion

    public FPSCameraBehaviour()
    {
    }

    public override void Activate()
    {
        base.Activate();

        // If no target object set
        if (!m_Target)
        {
            Deactivate();
        }

        // Get Camera FOV
        m_DesiredFOV = m_CameraDriver.GetComponent<Camera>().fieldOfView;

        // Smoothly move camera towards head location
        while (m_CameraDriver.transform.position != m_Target.transform.position)
        {
            m_CameraDriver.transform.position = Vector3.Slerp(m_CameraDriver.transform.position, m_Target.transform.position, Time.deltaTime);


            if (Vector3.Distance(m_CameraDriver.transform.position, m_Target.transform.position) <= 0.1f)
            {
                m_CameraDriver.transform.position = m_Target.transform.position;
            }
        }
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

        // Store current velocity
        m_Velocity = (m_CameraDriver.transform.position - m_LastVelocity) / Time.deltaTime;

        // Apply velocity based tilt
        if (EnableRoll)
        {
            ApplyRoll();
        }

        // Apply velocity based look-ahead
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
        // Translate camera to target object position
        m_CameraDriver.transform.position = m_Target.transform.TransformPoint(m_PositionOffset);
    }

    private void ApplyRotation()
    {
        // Apply final rotation
        m_CameraDriver.transform.rotation = m_Rotation;
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
        float horizontalLookAheadValue = EnableLookAhead ? Mathf.SmoothDamp(m_CameraDriver.transform.InverseTransformDirection(m_MouseVelocity).x * LookAheadIntensity.x, 0, ref m_LookAheadVelocity.x, LookAheadDamping) : 0;
        float verticalLookAheadValue = EnableLookAhead ? Mathf.SmoothDamp(m_CameraDriver.transform.InverseTransformDirection(m_MouseVelocity).y * LookAheadIntensity.y, 0, ref m_LookAheadVelocity.y, LookAheadDamping) : 0;

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
        //float fovValue = Mathf.Clamp((m_DesiredFOV + Mathf.Abs(new Vector3(m_Velocity.x, 0, m_Velocity.z).magnitude) * FOVIntensity), m_DesiredFOV, MaxFOV);
        float fovValue = Mathf.Clamp((m_DesiredFOV + Mathf.Abs(m_CameraDriver.transform.InverseTransformDirection(m_Velocity).z) * FOVIntensity), m_DesiredFOV, MaxFOV);

        // Prevent small floating point math
        if (Mathf.Approximately(fovValue, m_DesiredFOV))
        {
            fovValue = m_DesiredFOV;
        }

        // Apply final fov value
        //m_CameraDriver.GetComponent<Camera>().fieldOfView = Mathf.SmoothStep(m_CameraDriver.GetComponent<Camera>().fieldOfView, fovValue, Time.deltaTime * FOVDamping);
        m_CameraDriver.GetComponent<Camera>().fieldOfView = Mathf.Lerp(m_CameraDriver.GetComponent<Camera>().fieldOfView, fovValue, Time.deltaTime);
        //m_CameraDriver.GetComponent<Camera>().fieldOfView = MathUtils.LerpTo(FOVDamping, m_CameraDriver.GetComponent<Camera>().fieldOfView, fovValue, Time.deltaTime);

    }

    public override void SetFacingDirection(Vector3 lookDirection)
    {
        m_CameraDriver.transform.LookAt(lookDirection);
    }

    public override void UpdateRotation(float yaw, float pitch)
    {
        // Store mouse input
        m_MouseInput.x += yaw * MouseSensitivityX * (InvertVerticalAxis ? -1 : 1);
        m_MouseInput.y += pitch * MouseSensitivityY * (InvertHorizontalAxis ? -1 : 1);

        // Clamp Y rotation
        m_MouseInput.x = Mathf.Clamp(m_MouseInput.x, CameraMinY, CameraMaxY);

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
    }

    public override bool UsesStandardControlRotation()
    {
        return false;
    }
}
