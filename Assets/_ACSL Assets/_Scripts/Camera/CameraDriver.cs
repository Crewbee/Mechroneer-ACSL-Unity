using UnityEngine;

[RequireComponent(typeof(Camera))]
public abstract class CameraDriver : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public CameraBehaviour m_CameraBehaviour;
    protected CameraBehaviour m_CurrentCameraBehaviour;
    public Vector3 ControlRotation { get; protected set; }
    public Vector3 LookPosition { get; set; }
    public Vector3 PivotRotation { get; set; }
    #endregion

    #region PROTECTED_VARIABLES
    protected Transform m_Target;
    #endregion

    private void FixedUpdate()
    {
        if (m_Target == null)
        {
            return;
        }

        if (m_CurrentCameraBehaviour != null)
        {
            m_CurrentCameraBehaviour.FixedUpdate();
        }
    }

    private void Update()
    {
        if (m_Target == null)
        {
            return;
        }

        if (m_CurrentCameraBehaviour != null)
        {
            m_CurrentCameraBehaviour.Update();
        }
    }

    private void LateUpdate()
    {
        if (m_Target == null)
        {
            return;
        }

        if (m_CurrentCameraBehaviour != null)
        {
            m_CurrentCameraBehaviour.LateUpdate();
            ControlRotation = m_CurrentCameraBehaviour.GetControlRotation();
        }
    }

    public virtual void SetTarget(Transform target)
    {
        m_Target = target;

        if (m_Target != null)
        {
            LookPosition = m_Target.transform.position;
            m_CameraBehaviour.Init(this, m_Target);
        }

        SetCameraBehaviour(m_CameraBehaviour);
    }
    
    public void UpdateRotation(float yaw, float pitch)
    {
        if (m_CurrentCameraBehaviour != null)
        {
            m_CurrentCameraBehaviour.UpdateRotation(yaw, pitch);
        }
    }

    public void UpdateZoom(float zoom)
    {
        if (m_CurrentCameraBehaviour != null)
        {
            m_CurrentCameraBehaviour.UpdateZoom(zoom);
        }
    }

    public void SetFacingDirection(Vector3 facingDirection)
    {
        if (m_CurrentCameraBehaviour != null)
        {
            m_CurrentCameraBehaviour.SetFacingDirection(facingDirection);
        }
    }

    public void SetCameraBehaviour(CameraBehaviour behaviour)
    {
        if (m_CurrentCameraBehaviour == behaviour)
        {
            return;
        }

        if (m_CurrentCameraBehaviour != null)
        {
            m_CurrentCameraBehaviour.Deactivate();
        }

        m_CurrentCameraBehaviour = behaviour;

        if (m_CurrentCameraBehaviour != null)
        {
            m_CurrentCameraBehaviour.Activate();
        }
    }
}
