//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;

//TODO: DO NOT DELETE
//[DefaultExecutionOrder(-100)]
//public class PlayerInput : TestController
//{
//    //public bool testTouchControlsInEditor = false;  // Should touch controls be tested?
//    //public float verticalDPadThreshold = .5f;       // Threshold touch pad inputs
//    //public Thumbstick thumbstick;					// Reference to Thumbstick
//    //public TouchButton jumpButton;			    // Reference to jump TouchButton

//    public bool inputEnabled = true;
//    [HideInInspector] public Vector2 mousePos;      // Vector2 that stores the mouse location on screen
//    [HideInInspector] public Vector3 mouseWorldPos; // Vector2 that stores the mouse location in world
//    [HideInInspector] public float horizontal;      // Float that stores horizontal input
//    [HideInInspector] public float vertical;        // Float that stores horizontal input

//    [HideInInspector] public bool ability0Held;
//    [HideInInspector] public bool ability0Pressed;
//    [HideInInspector] public bool ability1Held;
//    [HideInInspector] public bool ability1Pressed;
//    [HideInInspector] public bool ability2Held;
//    [HideInInspector] public bool ability2Pressed;
//    [HideInInspector] public bool ability3Held;
//    [HideInInspector] public bool ability3Pressed;

//    [HideInInspector] public bool changePerspectiveHeld;
//    [HideInInspector] public bool changePerspectivePressed;

//    //bool dPadCrouchPrev;                            // Previous values of touch Thumbstick
//    private bool m_readyToClear;                                // Bool used to keep input in sync

//    private void Update()
//    {
//        //Clear out existing input values
//        ClearInput();

//        //If the Game Manager says the game is over, exit
//        if (!inputEnabled)
//        {
//            return;
//        }

//        //Process keyboard, mouse, gamepad (etc) inputs
//        ProcessInputs();

//        //Process mobile (touch) inputs
//        //ProcessTouchInputs();

//        //Clamp the horizontal input to be between -1 and 1
//        horizontal = Mathf.Clamp(horizontal, -1f, 1f);

//        //Clamp the vertical input to be between -1 and 1
//        vertical = Mathf.Clamp(vertical, -1f, 1f);
//    }

//    private void FixedUpdate()
//    {
//        //In FixedUpdate() we set a flag that lets inputs to be cleared out during the 
//        //next Update(). This ensures that all code gets to use the current inputs
//        m_readyToClear = true;
//    }

//    private void ClearInput()
//    {
//        //If we're not ready to clear input, exit
//        if (!m_readyToClear)
//        {
//            return;
//        }

//        //Reset all inputs
//        horizontal = 0f;
//        ability0Pressed = false;
//        ability0Held = false;
//        ability1Pressed = false;
//        ability1Held = false;

//        m_readyToClear = false;
//    }

//    private void ProcessInputs()
//    {
//        //Assign mouse screen position
//        mousePos = Input.mousePosition;

//        //Assign mouse world position
//        if (EventSystem.current.IsPointerOverGameObject()) // If mouse is occluded by UI element
//        {
//            return;
//        }

//        #region DESKTOP
//#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hitInfo = new RaycastHit();

//        int layerMask = 1 << 10;
//        layerMask = ~layerMask;

//        RaycastHit[] allHits = Physics.RaycastAll(ray);
//        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
//        {
//            mouseWorldPos = hitInfo.point;
//        }
//#endif
//        #endregion
//        #region MOBILE
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
//                    mouseWorldPos = ARhitInfo.point;
//                }
//            }
//        }
        
//#endif
//        #endregion

//        //Accumulate horizontal axis input
//        horizontal += Input.GetAxis("Horizontal");

//        //Accumulate vertical axis input
//        vertical += Input.GetAxis("Vertical");

//        //Accumulate button inputs
//        ability0Pressed = ability0Pressed || Input.GetButtonDown("Ability0");
//        ability0Held = ability0Held || Input.GetButton("Ability0");

//        ability1Pressed = ability1Pressed || Input.GetButtonDown("Ability1");
//        ability1Held = ability1Held || Input.GetButton("Ability1");

//        ability2Pressed = ability2Pressed || Input.GetButtonDown("Ability2");
//        ability2Held = ability2Held || Input.GetButton("Ability2");

//        ability3Pressed = ability3Pressed || Input.GetButtonDown("Ability3");
//        ability3Held = ability3Held || Input.GetButton("Ability3");

//        changePerspectivePressed = changePerspectivePressed || Input.GetButtonDown("Change Perspective");
//        changePerspectiveHeld = changePerspectiveHeld || Input.GetButton("Change Perspective");
//    }

//    //void ProcessTouchInputs()
//    //{
//    //	//If this isn't a mobile platform AND we aren't testing in editor, exit
//    //	if (!Application.isMobilePlatform && !testTouchControlsInEditor)
//    //		return;

//    //	//Record inputs from screen thumbstick
//    //	Vector2 thumbstickInput = thumbstick.GetDirection();

//    //	//Accumulate horizontal input
//    //	horizontal		+= thumbstickInput.x;

//    //	//Accumulate jump button input
//    //	jumpPressed		= jumpPressed || jumpButton.GetButtonDown();
//    //	jumpHeld		= jumpHeld || jumpButton.GetButton();

//    //	//Using thumbstick, accumulate crouch input
//    //	bool dPadCrouch = thumbstickInput.y <= -verticalDPadThreshold;
//    //	crouchPressed	= crouchPressed || (dPadCrouch && !dPadCrouchPrev);
//    //	crouchHeld		= crouchHeld || dPadCrouch;

//    //	//Record whether or not playing is crouching this frame (used for determining
//    //	//if button is pressed for first time or held
//    //	dPadCrouchPrev	= dPadCrouch;
//    //}
//}
