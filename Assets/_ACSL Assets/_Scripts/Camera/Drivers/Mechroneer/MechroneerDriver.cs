using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CameraCentroid))]
public class MechroneerDriver : CameraDriver
{
    public enum CameraMode
    {
        Robot,
        TopDown
    }

    [Header("Global Camera Settings")]
    public CameraMode StartingCamera = CameraMode.TopDown;

    [Header("Camera Behaviour Settings")]
    public RobotCameraBehaviour RobotCamera;
    public TopDownCameraBehaviour TopDownCamera;

    public CameraCentroid cameraCentroid;
    private Transform currentTarget;

    public void Awake()
    {
        switch (StartingCamera)
        {
            case CameraMode.Robot:
                m_CameraBehaviour = RobotCamera;
                if (m_CurrentCameraBehaviour == null)
                {
                    m_CurrentCameraBehaviour = new RobotCameraBehaviour();
                }
                break;
            case CameraMode.TopDown:
                m_CameraBehaviour = TopDownCamera;
                if (m_CurrentCameraBehaviour == null)
                {
                    m_CurrentCameraBehaviour = new TopDownCameraBehaviour();
                }
                break;
        }

        cameraCentroid = GetComponent<CameraCentroid>();
    }


    //public override void SetTarget(Transform target)
    //{
    //    for (int i = 0; i < cameraCentroid.m_Targets.Count; i++)
    //    {
    //        if (cameraCentroid.m_Targets[i] != target)
    //        {
    //            cameraCentroid.m_Targets.RemoveAt(i);
    //        }
    //    }
    //}

    public void RemoveTarget(Transform target)
    {
        if (cameraCentroid.m_Targets.Contains(target))
        {
            cameraCentroid.m_Targets.Remove(target);
        }
    }

    public void AddTarget(Transform target)
    {
        cameraCentroid.m_Targets.Add(target);
    }

    ///DEBUG REMOVE ME
    public void Start()
    {
        //m_CurrentCameraBehaviour.Init(this, cameraCentroid.m_Centroid);
        base.SetTarget(cameraCentroid.m_Centroid);
    }
    ///END REMOVE

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
            Debug.Log("m_Target == null");
            return;
        }

        if (m_CurrentCameraBehaviour != null)
        {
            if (m_CurrentCameraBehaviour.GetType() == typeof(RobotCameraBehaviour))
            {
                //Debug.Log("RobotCameraBehaviour");
                m_CurrentCameraBehaviour.Deactivate();
                TopDownCamera.Init(this, m_Target);
                TopDownCamera.m_Input = (m_CurrentCameraBehaviour as RobotCameraBehaviour).m_Input;
                SetCameraBehaviour(TopDownCamera);
                return;
            }
            else if (m_CurrentCameraBehaviour.GetType() == typeof(TopDownCameraBehaviour))
            {
                //Debug.Log("TopDownCameraBehaviour");
                m_CurrentCameraBehaviour.Deactivate();
                RobotCamera.Init(this, m_Target);
                RobotCamera.m_Input = (m_CurrentCameraBehaviour as TopDownCameraBehaviour).m_Input;
                SetCameraBehaviour(RobotCamera);
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
