//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//TODO: REMAKE THIS
//public class MobileController : PlayerController
//{
//    private bool m_InputEnabled = true;

//    public void SetControlsActive(bool active)
//    {
//        m_InputEnabled = active;
//    }
//    public GameObject GetAimTarget()
//    {
//        throw new System.NotImplementedException();
//    }

//    public Vector3 GetControlRotation()
//    {
//        return Vector3.zero;
//    }

//    public Vector3 GetLookInput()
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool ChangePerspective()
//    {
//        if (!m_InputEnabled)
//            return false;

//        return Input.GetButtonDown("ChangePerspective");
//    }

//    public Vector3 GetMoveInput()
//    {
//        if (!m_InputEnabled)
//            return Vector3.zero;

//#if UNITY_IOS || UNITY_ANDROID
//        DebugText.AddMessage("Checking for touch", 1.0f);
//        if (Input.touchCount > 0)
//        {
//            DebugText.AddMessage("Touch found", 10.0f);
//            Vector2 touchPosition = Input.GetTouch(0).position;
//            //m_arRaycastManager.Raycast(touchPosition, s_)
//            GameObject arcamera = GameObject.Find("AR Camera");
//            if (arcamera)
//            {
//                DebugText.AddMessage("Camera Found", 10.0f);
//                Camera camera = arcamera.GetComponent<Camera>();
//                Ray ARray = camera.ScreenPointToRay(touchPosition);
//                DebugText.AddMessage("Trying to move - raycast", 10.0f);
//                RaycastHit ARhitInfo = new RaycastHit();
//                if (Physics.Raycast(ARray, out ARhitInfo))
//                {
//                    DebugText.AddMessage("Found - raycast", 10.0f);
//                    return ARhitInfo.point;
//                }
//            }
//        }
//#endif

//        return Vector3.one * Mathf.Infinity;
//    }

//    public float GetZoomInput()
//    {
//        if (!m_InputEnabled)
//            return 0;

//        if (Input.touchCount >= 2)
//        {
//            Vector2 touch0, touch1;
//            float distance;
//            touch0 = Input.GetTouch(0).position;
//            touch1 = Input.GetTouch(1).position;
//            distance = Vector2.Distance(touch0, touch1);

//            return distance;
//        }

//        return 0;
//    }

//    public void Init(OldPlayer player)
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool IsAiming()
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool IsFiring()
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool IsJumping()
//    {
//        throw new System.NotImplementedException();
//    }

//    public void SetFacingDirection(Vector3 direction)
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool SwitchRobotTarget()
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool LeftArmAbility()
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool RightArmAbility()
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool BodyAbility()
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool LegAbility()
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool ToggleCrouch()
//    {
//        throw new System.NotImplementedException();
//    }

//    public void UpdateControls()
//    {
//        throw new System.NotImplementedException();
//    }
//}
