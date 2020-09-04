using UnityEngine;

public class FPSCameraDriver : CameraDriver
{
    public FPSCameraBehaviour FPSCamera;

    public void Awake()
    {
        m_CameraBehaviour = FPSCamera;


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

    // Update is called once per frame
    private void Update()
    {

    }
}
