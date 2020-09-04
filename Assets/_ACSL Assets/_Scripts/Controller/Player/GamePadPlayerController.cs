//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;
//using UnityEngine.InputSystem;

//[System.Serializable]
//public class GamePadPlayerController : PlayerController
//{
//    #region Base Variables
//    public CameraDriver ControlCamera { get; set; }
//    public GamePadController gamePadController;
//    ARRaycastManager m_arRaycastManager;
//    #endregion

//    #region Variables to Interface With Input Action Object
//    bool ZoomingIn = false;
//    Vector2 gamePadMoveAxis = Vector2.zero;
//    Vector2 gamePadCamPivotAxis = Vector2.zero;
//    bool LeftArmActive = false;
//    bool RightArmActive = false;
//    bool BodyActive = false;
//    bool LegActive = false;
//    bool ToggleNextTarget = false;
//    bool ResetCamTargetToSelf = false;
//    bool ToggleTopDownView = false;
//    bool isFiring = false;
//    #endregion


//    public float ZoomSpeed = 0.4f;
//    float ZoomedAmount = 0.0f;

//    MechroneerPlayer m_player;
//    List<Robot> m_EnemyRobots;
//    Robot gamePadControllerTarget;
//    public GamePadPlayerController() { }

//    private bool m_InputEnabled = true;

//    public void SetControlsActive(bool active)
//    {
//        m_InputEnabled = active;
//    }

//    public void Init(Player player)
//    {
//        #region Setup Control Camera and Owning Player Refs
//        Cursor.visible = false;
//        ControlCamera = Camera.main.GetComponent<CameraDriver>();
//        if (ControlCamera)
//        {
//            if ((player as MechroneerPlayer).currentRobot != null)
//            {
//                (ControlCamera as MechroneerDriver).AddTarget((player as MechroneerPlayer).currentRobot.transform);
//                //foreach (var Robot in (player as MechroneerPlayer).activeRobots)
//                //    (ControlCamera as MechroneerDriver).AddTarget(Robot.transform);
//            }
//            else
//            {
//                ControlCamera.SetTarget(new GameObject().transform);
//            }
//        }
//        else
//        {
//            GameObject arSessionOrigin = GameObject.Find("AR Session Origin");
//            if (arSessionOrigin)
//            {
//                m_arRaycastManager = arSessionOrigin.GetComponent<ARRaycastManager>();
//            }
//        }
//        #endregion

//        m_player = player as MechroneerPlayer; //cache player ref
//    }

//    void Awake()
//    {
//        gamePadController = new GamePadController();//cache gamepad ref

//        #region Assign Variables To Input Actions
//        gamePadController.AnalogController.CameraZoomIn.performed += (InputAction.CallbackContext context) => { ZoomingIn = context.ReadValue<bool>(); };
//        gamePadController.AnalogController.CameraZoomOut.performed += context => ZoomingIn = !context.ReadValue<bool>();

//        gamePadController.AnalogController.Move.performed += context => gamePadMoveAxis = context.ReadValue<Vector2>();
//        gamePadController.AnalogController.PivotCamera.performed += context => gamePadCamPivotAxis = context.ReadValue<Vector2>();

//        gamePadController.AnalogController.LeftArmAbility.performed += context => LeftArmActive = context.ReadValue<bool>();
//        gamePadController.AnalogController.RightArmAbility.performed += context => RightArmActive = context.ReadValue<bool>();
//        gamePadController.AnalogController.BodyAbility.performed += context => BodyActive = context.ReadValue<bool>();
//        gamePadController.AnalogController.LegAbility.performed += context => LegActive = context.ReadValue<bool>();

//        gamePadController.AnalogController.SwitchEnemyTarget.performed += context => ToggleNextTarget = context.ReadValue<bool>();
//        gamePadController.AnalogController.ResetTargetToSelf.performed += context => ResetCamTargetToSelf = context.ReadValue<bool>();

//        gamePadController.AnalogController.ResetCameraState.performed += context => ToggleTopDownView = context.ReadValue<bool>();
//        gamePadController.AnalogController.Fire.performed += context => isFiring = context.ReadValue<bool>();
//        #endregion

//        gamePadController.Enable();
//    }

//    public void UpdateControls()
//    {

//    }

//    public bool ChangePerspective()
//    {
//        if (!m_InputEnabled)
//            return false;

//        return Input.GetButtonDown("JoystickSwtichView");

//    }

//    public void FixedUpdate()
//    {
//        if (!ControlCamera)
//        {
//            return;
//        }

//        Vector2 lookInput = GetLookInput();
//        ControlCamera.UpdateRotation(lookInput.x, lookInput.y);
//    }

//    public void Update()
//    {
//        if (!ControlCamera)
//        {
//            return;
//        }

//        float zoomInput = GetZoomInput();
//        ControlCamera.UpdateZoom(zoomInput);
//    }

//    public void SetFacingDirection(Vector3 direction)
//    {

//    }

//    public void AddLedgeDir(Vector3 ledgeDir)
//    {

//    }

//    public Vector3 GetControlRotation()
//    {
//        return Vector3.zero;
//    }

//    public float GetZoomInput()
//    {
//        if (!m_InputEnabled)
//            return 0;

//        if (ZoomingIn)
//        {
//            ZoomedAmount += ZoomSpeed;
//            return ZoomedAmount;
//        }
//        else if (!ZoomingIn)
//        {
//            ZoomedAmount -= ZoomSpeed;
//            return ZoomedAmount;
//        }

//        return ZoomedAmount;
//    }

//    public Vector3 GetMoveInput()
//    {
//        if (!m_InputEnabled)
//            return Vector3.zero;

//#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX

//        Vector3 direction = Vector3.zero;

//        Vector2 Axis = new Vector2(Input.GetAxis("LeftStickHorizontal"), -Input.GetAxis("LeftStickVertical"));

//        direction.x += Axis.x;
//        direction.z += Axis.y;

//        direction.Normalize();
//        return direction;
//#endif
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
//        return Vector3.zero;
//#endif
//    }

//    public Vector3 GetLookInput()
//    {
//        if (!m_InputEnabled)
//            return Vector3.zero;

//        return new Vector3(Input.GetAxis("RightStickVertical"), Input.GetAxis("RightStickHorizontal"), 0.0f);
//    }

//    public GameObject GetAimTarget()
//    {
//        if (!m_InputEnabled)
//            return null;

//        return m_player.currentRobot.GetMainTarget();
//    }

//    public bool IsFiring()
//    {
//        if (Input.GetButton("LeftArmButton") || Input.GetButton("RightArmButton") || Input.GetButton("BodyButton") || Input.GetButton("LegButton"))
//            return true;
//        else
//            return false;
//    }

//    public bool SwitchRobotTarget()
//    {
//        return false;
//    }

//    public bool SwitchRobotTargetForward()
//    {
//        return Input.GetButtonDown("SwitchTargetForwardButton");
//    }

//    public bool SwitchRobotTargetBackward()
//    {
//        return Input.GetButtonDown("SwitchTargetBackwardButton");
//    }
//    public bool ResetTargetToSelf()
//    {
//        return ResetCamTargetToSelf;
//    }

//    public bool LeftArmAbility()
//    {
//        return Input.GetButton("LeftArmButton");
//    }

//    public bool RightArmAbility()
//    {
//        return Input.GetButton("RightArmButton");
//    }

//    public bool BodyAbility()
//    {
//        return Input.GetButton("BodyButton");
//    }

//    public bool LegAbility()
//    {
//        return Input.GetButton("LegButton");
//    }

//}
