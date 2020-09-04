using UnityEngine;

public class DualCameraDriver : CameraDriver
{
    public enum CameraMode
    {
        FPS,
        ThirdPerson
    }

    [Header("Global Camera Settings")]
    public CameraMode StartingCamera = CameraMode.FPS;

    [Header("Camera Behaviour Settings")]
    public FollowCameraBehaviour FollowCamera;
    public FPSCameraBehaviour FPSCamera;

    public void Awake()
    {
        switch (StartingCamera)
        {
            case CameraMode.FPS:
                m_CameraBehaviour = FPSCamera;
                break;
            case CameraMode.ThirdPerson:
                m_CameraBehaviour = FollowCamera;
                break;
        }

        if (m_CurrentCameraBehaviour == null)
        {
            m_CurrentCameraBehaviour = new FPSCameraBehaviour();
        }
    }

    private void FixedUpdate()
    {
        if (m_Target == null)
        {
            return;
        }

        if (m_CurrentCameraBehaviour != null)
        {
            m_CurrentCameraBehaviour.FixedUpdate();
            ControlRotation = m_CurrentCameraBehaviour.GetControlRotation();
        }
    }

    public void SwitchPerspective()
    {
        if (m_Target == null)
        {
            return;
        }

        if (m_CurrentCameraBehaviour != null)
        {
            if (m_CurrentCameraBehaviour.GetType() == typeof(FollowCameraBehaviour))
            {
                FPSCamera.Init(this, m_Target);
                SetCameraBehaviour(FPSCamera);
                //FPSCamera.Rotation = Quaternion.Euler(FollowCamera.GetControlRotation());
                FPSCamera.MouseInput = new Vector2(-FollowCamera.MouseInput.x, FollowCamera.MouseInput.y);
                return;
            }
            else if (m_CurrentCameraBehaviour.GetType() == typeof(FPSCameraBehaviour))
            {
                FollowCamera.Init(this, m_Target);
                SetCameraBehaviour(FollowCamera);
                //FollowCamera.Rotation = Quaternion.Euler(FPSCamera.GetControlRotation());
                FollowCamera.MouseInput = new Vector2(-FPSCamera.MouseInput.x, FPSCamera.MouseInput.y);
                return;
            }
        }
    }

    public void SetBehaviour(CameraBehaviour newBehaviour)
    {
        if (m_CurrentCameraBehaviour != null)
        {
            // If a specific behaviour is provided
            if (newBehaviour != null)
            {
                newBehaviour.Init(this, m_Target);
                SetCameraBehaviour(newBehaviour);
            }
        }
    }
}
