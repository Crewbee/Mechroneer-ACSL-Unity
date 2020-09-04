using UnityEngine;

public abstract class CameraBehaviour
{
    #region PROTECTED_VARIABLES
    protected CameraDriver m_CameraDriver;
    protected Transform m_Target;
    #endregion

    public CameraBehaviour()
    {

    }
    public virtual void Init(CameraDriver camera, Transform target)
    {
        m_CameraDriver = camera;
        m_Target = target;
    }
    public virtual void Activate() { }
    public virtual void Deactivate() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }
    public abstract void UpdateRotation(float yaw, float pitch);
    public virtual void UpdateZoom(float zoom) { }
    public abstract void SetFacingDirection(Vector3 lookDirection);
    public virtual Vector3 GetControlRotation()
    {
        return m_CameraDriver.transform.rotation.eulerAngles;
    }
    public virtual bool UsesStandardControlRotation()
    {
        return true;
    }
}

