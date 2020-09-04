//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////using Photon.Pun;

//public enum TeamColor
//{
//    RED = 0,
//    BLUE,
//    GREEN,
//    YELLOW
//}

//public class PlayerPlatformStats
//{
//    public Color playerPlatformColor;

//    public void Init()
//    {
//        playerPlatformColor = Color.red;
//    }
//}

//TODO: delete this
//[DefaultExecutionOrder(-100)]
//public class MechroneerPlayer : OldPlayer
//{
//    public RobotPartType m_abilityToFire;

//    #region Public Variables
//    [Header("Prefabs")]
//    public GameObject m_TapToMoveArrowPrefab;
//    public GameObject m_AimIndicatorPrefab;
//    public GameObject m_PlayerIndicatorPrefab;

//    #region Player
//    [Header("Player Data")]
//    public UserData userData;
//    [SerializeField]
//    public AIController temp;
//    public PlayerController playerController = null;
//    public bool isUsingGamePad = false;
//    #endregion

//    #region Robot
//    [Header("Robot Data")]
//    public RobotOld currentRobot;
//    public List<RobotOld> m_targets;
//    public RobotOld currentTarget;
//    public RobotData robotData;
//    #endregion

//    #region Networking
//    [Header("Network Data")]
//    public int playerID = -1;
//    public int playerTeam;
//    public TeamColor playerColor;
//    private bool m_hasInit;
//    #endregion
//    #endregion

//    #region Private Variables
//    private GameObject m_TapToMoveArrow;
//    private GameObject m_AimIndicator;
//    private GameObject m_PlayerIndicator;
//    private PlayerIndicator m_IndicatorScript;
//    private float m_TapToMoveArrowAlpha;
//    private RobotOld m_Robot;
//    private int m_CurrentRobotIndex;
//    bool m_playerPaused;
//    bool m_Spectator;
//    #endregion

//    public void Awake()
//    {
//        playerID = -1;
//    }
//    public void Start()
//    {
//        //if (!m_hasInit)
//        //    playerID = -1;
//        m_Spectator = false;
//        Dictionary<int, RobotOld>.ValueCollection RegisteredRobots = RobotOld.registeredRobots.Values;
//        RobotOld[] array = new RobotOld[RegisteredRobots.Count];
//        RegisteredRobots.CopyTo(array, 0);

//        for (int i = 0; i < array.Length; i++)
//        {
//            if (array[i] != currentRobot)
//            {
//                m_targets.Add(array[i]);
//            }
//        }

//        currentTarget = null;
//    }

//    public void Init()
//    {
//        // Initialize Tap arrow prefab
//        m_TapToMoveArrow = Instantiate(m_TapToMoveArrowPrefab, this.transform);
//        m_AimIndicator = Instantiate(m_AimIndicatorPrefab, this.transform);
//        m_PlayerIndicator = Instantiate(m_PlayerIndicatorPrefab, this.transform);
//        m_IndicatorScript = m_PlayerIndicator.GetComponent<PlayerIndicator>();
//        m_TapToMoveArrow.SetActive(false);
//        m_AimIndicator.SetActive(false);
//        m_TapToMoveArrowAlpha = 0;

//        // Cache current robot reference
//        m_Robot = currentRobot;

//        // Assign appropriate controller
//        if (controller != null)
//        {
//            if (controller.GetType() == typeof(MouseKeyPlayerController))
//            {
//                if (currentRobot)
//                    currentRobot.transform.name = "User_Robot";
//            }
//            else if (controller.GetType() == typeof(EnemyAIController))
//            {
//                transform.name = name + "_AI";
//                currentRobot.transform.name = "AI_Robot";
//                temp = controller as EnemyAIController;
//            }
//        }
//        if (playerID != PhotonNetwork.LocalPlayer.ActorNumber)
//            GetComponent<SpectatorPlayer>().enabled = false;


//        // Cache PlayerController
//        if (controller.GetType() == typeof(MouseKeyPlayerController))
//        {
//            if (Input.GetJoystickNames().Length > 0)
//            {
//                isUsingGamePad = true;
//                controller = null;
//                controller = new GamePadPlayerController();

//                controller.Init(this);

//                playerController = (controller as GamePadPlayerController);
//            }
//            else
//            {
//                playerController = (controller as MouseKeyPlayerController);
//            }
//        }


//        // Setup robot parenting
//        if (m_Robot != null)
//        {
//            m_Robot.SetUpParentID(currentRobot.transform.name);
//            m_Robot.TurnToFaceTarget();
//        }
//        MulticastFindRobots();
//        m_hasInit = true;
//        m_CurrentRobotIndex = 0;
//    }
//    //[PunRPC]
//    private void FindRobots()
//    {
//        RobotOld[] robotsToFind = FindObjectsOfType<RobotOld>();
//        foreach (RobotOld robot in robotsToFind)
//        {
//            if (robot.playerID == playerID)
//            {
//                currentRobot = robot;
//            }
//        }
//    }

//    //[PunRPC]
//    private void SetPlayerID(int ID)
//    {
//        playerID = ID;
//        if (playerID != PhotonNetwork.LocalPlayer.ActorNumber)
//            GetComponent<SpectatorPlayer>().enabled = false;
//    }

//    public void MulticastSetPlayerID(int ID)
//    {
//        photonView.RPC("SetPlayerID", RpcTarget.AllBuffered, ID);
//    }
//    private void MulticastFindRobots()
//    {
//        photonView.RPC("FindRobots", RpcTarget.AllBuffered);
//    }

//    private void UpdateControls()
//    {
//        if (controller == null)
//        {
//            Debug.LogError("NULL Controller {0}" + controller);
//            return;
//        }
//        if (!isUsingGamePad)
//        {
//            if (controller != null)
//            {
//                if (playerID >= 0) // User
//                {
//                    if (Time.inFixedTimeStep)
//                    {
//                        // Fixed Update Loop
//                        (controller as MouseKeyPlayerController).FixedUpdate();
//                    }
//                    else
//                    {
//                        // Update / Late Update Loop
//                        (controller as MouseKeyPlayerController).Update();
//                    }
//                }
//            }
//        }
//        else
//        {
//            if (controller != null)
//            {
//                if (playerID >= 0)
//                {
//                    if (Time.inFixedTimeStep)
//                    {
//                        //Fixed Update Loop
//                        (controller as GamePadPlayerController).FixedUpdate();
//                    }
//                    else
//                    {
//                        //Update / Late Update Loop
//                        (controller as GamePadPlayerController).Update();
//                    }
//                }
//            }
//        }

//        if (Time.inFixedTimeStep == false && playerID <= -1) // AI
//        {
//            (controller as EnemyAIController).Update();
//        }
//    }

//    private void FixedUpdate()
//    {
//        if (!photonView.IsMine)
//            return;
//        // Update Linear Controls
//        UpdateControls();
//    }

//    private void Update()
//    {
//        if (!photonView.IsMine)
//            return;
//        // Update Binary Controls
//        UpdateControls();

//        if (playerID == PhotonNetwork.LocalPlayer.ActorNumber)
//        {
//            if (Input.GetKeyDown(KeyCode.Escape))
//            {
//                BattleSceneManager.instance.TogglePauseMenu();
//            }
//            if (currentRobot == null && m_Spectator == false)
//            {
//                m_Spectator = true;
//                //UIManagerOld.Instance.Push(4);
//                return;
//            }
//        }
//        // Update currentRobot Input
//        if (currentRobot != null && playerController != null)
//        {
//            UpdateCurrentRobotControls();
//        }
//        else
//        {
//            m_IndicatorScript.DisableVisuals();
//        }
//    }
//    private void UpdateCurrentRobotControls()
//    {
//        m_IndicatorScript.ChangeTarget(currentRobot.gameObject);


//        if (!isUsingGamePad)
//        {
//            if (m_abilityToFire != RobotPartType.Head)
//            {
//                Vector3 input = AbilityTarget.GetMousePosition();
//                if (input.z >= Mathf.Infinity )
//                {
//                    m_AimIndicator.SetActive(false);
//                }
//                else
//                {
//                    m_AimIndicator.GetComponent<LineRenderer>().SetPosition(0, m_Robot.transform.position);
//                    m_AimIndicator.GetComponent<LineRenderer>().startWidth = 0.1f;
//                    m_AimIndicator.GetComponent<LineRenderer>().endWidth = 1.0f;
//                    m_AimIndicator.GetComponent<LineRenderer>().SetPosition(1, m_AimIndicator.transform.position);
//                    input.y += 0.1f;
//                    m_AimIndicator.transform.position = input;
//                    m_AimIndicator.SetActive(true);
//                    m_AimIndicator.transform.Rotate(new Vector3(0, 150.0f * Time.deltaTime, 0), Space.World);
//                }
//            }
//            else
//            {
//                m_AimIndicator.SetActive(false);
//            }

//            if (playerController.IsFiring())
//            {
//                IEffectUser target = AbilityTarget.GetTargetRaycast();
//                if (m_abilityToFire != RobotPartType.Head)
//                {
//                    currentRobot.FireAbility(m_abilityToFire, target, AbilityTarget.GetMousePosition());
//                    m_abilityToFire = RobotPartType.Head;
//                }
//                else if (target != null)
//                {
//                    currentRobot.SetRobotTarget(target);
//                }
//                else
//                {
//                    //currentRobot.SetRobotTarget(null);
//                    Vector3 Destination = playerController.GetMoveInput();

//                    if (Destination.x < Mathf.Infinity)
//                    {
//                        currentRobot.MoveToPoint(Destination);
//                        if (m_TapToMoveArrow)
//                        {
//                            m_TapToMoveArrow.SetActive(true);
//                            m_TapToMoveArrow.transform.position = Destination;
//                            m_TapToMoveArrowAlpha = 0.5f;
//                            m_TapToMoveArrow.GetComponent<TapToMoveArrow>().m_dropletAlpha = 0.5f;
//                            m_TapToMoveArrow.GetComponent<TapToMoveArrow>().b_isMoving = true;
//                            m_TapToMoveArrow.GetComponent<TapToMoveArrow>().m_dropletDestination = Destination;
//                            m_TapToMoveArrow.GetComponent<TapToMoveArrow>().m_dropletDestination.y += 0.1f;
//                        }
//                    }
//                }
//            }


//            if (m_TapToMoveArrow.activeSelf == true)
//            {
//                if (m_TapToMoveArrowAlpha > 0.0f)
//                {
//                    Color tempMatColor = m_TapToMoveArrow.GetComponent<Renderer>().material.color;
//                    m_TapToMoveArrow.GetComponent<Renderer>().material.color = new Color(tempMatColor.r, tempMatColor.g, tempMatColor.b, m_TapToMoveArrowAlpha);
//                    m_TapToMoveArrowAlpha -= 0.1f;
//                }
//                else
//                {
//                    m_TapToMoveArrow.SetActive(false);
//                    m_TapToMoveArrow.GetComponent<TapToMoveArrow>().b_isMoving = false;
//                }
//            }

//            if (currentRobot)
//            //foreach (Robot robot in activeRobots)
//            {
//                currentRobot.TurnToFaceTarget();
//                currentRobot.ArmAbility();
//            }

//            SelectRobotAbility();

//            if (playerController.SwitchRobotTarget())
//            {
//                m_abilityToFire = RobotPartType.Head;
//                //m_CurrentRobotIndex++;

//                //if (m_CurrentRobotIndex < activeRobots.Count)
//                //{
//                //    currentRobot = activeRobots[m_CurrentRobotIndex];
//                //}

//                //else
//                //{
//                //    m_CurrentRobotIndex = 0;
//                //    currentRobot = activeRobots[m_CurrentRobotIndex];
//                //}

//                MechroneerDriver cameraDriver = ((controller as MouseKeyPlayerController).ControlCamera) as MechroneerDriver;
//                cameraDriver.AddTarget(currentRobot.transform);
//                cameraDriver.SetTarget(currentRobot.transform);
//            }

//            if (playerController.ChangePerspective())
//            {
//                MechroneerDriver cameraDriver = ((controller as MouseKeyPlayerController).ControlCamera) as MechroneerDriver;
//                cameraDriver.SwitchPerspective();
//            }
//        }
//        else
//        {
//            if (playerController.IsFiring())
//            {
//                RobotPart firingPart = currentRobot.GetRobotPart(m_abilityToFire);

//                if (currentTarget != null)
//                {
//                    if (!firingPart.InRange(currentTarget))
//                    {
//                        foreach (RobotOld enemy in m_targets)
//                        {
//                            if (firingPart.InRange(enemy))
//                            {
//                                currentTarget = enemy;
//                            }
//                        }

//                        currentTarget = null;
//                    }
//                }
//                if (firingPart.partType != RobotPartType.Head)
//                {
//                    if (firingPart.partType == RobotPartType.LeftArm || firingPart.partType == RobotPartType.RightArm)
//                    {
//                        currentRobot.FireAbility(m_abilityToFire, currentTarget, currentTarget.transform.position);
//                        m_abilityToFire = RobotPartType.Head;
//                    }
//                    else if (firingPart.partType == RobotPartType.Body)
//                    {
//                        if (firingPart.abilityData.targetingStyle == TargetingStyle.Self)
//                        {
//                            currentRobot.FireAbility(m_abilityToFire, currentRobot, currentRobot.transform.position);
//                            m_abilityToFire = RobotPartType.Head;
//                        }
//                        else
//                        {
//                            currentRobot.FireAbility(m_abilityToFire, currentTarget, currentTarget.transform.position);
//                            m_abilityToFire = RobotPartType.Head;
//                        }
//                    }
//                    else if (firingPart.partType == RobotPartType.Leg)
//                    {
//                        if (firingPart.abilityData.targetingStyle == TargetingStyle.Self)
//                        {
//                            currentRobot.FireAbility(m_abilityToFire, currentRobot, playerController.GetMoveInput() * 10.0f);
//                            m_abilityToFire = RobotPartType.Head;
//                        }
//                    }
//                }
//            }

//            Vector3 Destination = playerController.GetMoveInput() * 4.0f;

//            if (Destination.x < Mathf.Infinity)
//            {
//                currentRobot.MoveToPoint(Destination);

//                #region Tap To Move Arrow Not Implemented For GamePad
//                //if (m_TapToMoveArrow)
//                //{
//                //    m_TapToMoveArrow.SetActive(true);
//                //    m_TapToMoveArrow.transform.position = Destination;
//                //    m_TapToMoveArrowAlpha = 0.5f;
//                //    m_TapToMoveArrow.GetComponent<TapToMoveArrow>().m_dropletAlpha = 0.5f;
//                //    m_TapToMoveArrow.GetComponent<TapToMoveArrow>().b_isMoving = true;
//                //    m_TapToMoveArrow.GetComponent<TapToMoveArrow>().m_dropletDestination = Destination;
//                //    m_TapToMoveArrow.GetComponent<TapToMoveArrow>().m_dropletDestination.y += 0.1f;
//                //}
//                #endregion
//            }

//            #region Tap To Move Arrow Not Implemented For GamePad
//            //if (m_TapToMoveArrow.activeSelf == true)
//            //{
//            //    if (m_TapToMoveArrowAlpha > 0.0f)
//            //    {
//            //        Color tempMatColor = m_TapToMoveArrow.GetComponent<Renderer>().material.color;
//            //        m_TapToMoveArrow.GetComponent<Renderer>().material.color = new Color(tempMatColor.r, tempMatColor.g, tempMatColor.b, m_TapToMoveArrowAlpha);
//            //        m_TapToMoveArrowAlpha -= 0.1f;
//            //    }
//            //    else
//            //    {
//            //        m_TapToMoveArrow.SetActive(false);
//            //        m_TapToMoveArrow.GetComponent<TapToMoveArrow>().b_isMoving = false;
//            //    }
//            //}
//            #endregion

//            if (currentRobot)
//                //foreach (Robot robot in activeRobots)
//            {
//                currentRobot.TurnToFaceTarget();
//                currentRobot.ArmAbility();
//            }

//            MechroneerDriver cameraDriver = ((controller as GamePadPlayerController).ControlCamera) as MechroneerDriver;

//            if (playerController.SwitchRobotTarget())
//            {
//                if (currentTarget != null)
//                {
//                    int index = 0;

//                    foreach (RobotOld enemy in m_targets)
//                    {
//                        if (currentTarget == enemy)
//                        {
//                            break;
//                        }
//                        index++;
//                    }

//                    cameraDriver.RemoveTarget(currentTarget.transform);

//                    currentTarget = m_targets[index];

//                    cameraDriver.AddTarget(currentTarget.transform);
//                    cameraDriver.SetTarget(currentTarget.transform);
//                }
//                else
//                {
//                    currentTarget = m_targets[0];
//                    cameraDriver.AddTarget(currentTarget.transform);
//                    cameraDriver.SetTarget(currentTarget.transform);
//                }
//            }

//            if ((playerController as GamePadPlayerController).ResetTargetToSelf())
//            {
//                if (currentTarget != null)
//                {
//                    cameraDriver.RemoveTarget(currentTarget.transform);
//                }

//                cameraDriver.AddTarget(currentRobot.transform);
//                cameraDriver.SetTarget(currentRobot.transform);
//            }

//            if (playerController.ChangePerspective())
//            {
//                cameraDriver.SwitchPerspective();
//            }
//        }
//    }

//    private void SelectRobotAbility()
//    {
//        if (playerController.LeftArmAbility())
//        {
//            SetAbilityToFire(RobotPartType.LeftArm);
//        }
//        if (playerController.RightArmAbility())
//        {
//            SetAbilityToFire(RobotPartType.RightArm);
//        }

//        if (playerController.BodyAbility())
//        {
//            SetAbilityToFire(RobotPartType.Body);
//        }
//        if (playerController.LegAbility())
//        {
//            SetAbilityToFire(RobotPartType.Leg);
//        }
//    }

//    public void SetAbilityToFire(RobotPartType part)
//    {
//        if (m_abilityToFire == part)
//            m_abilityToFire = RobotPartType.Head;
//        else
//        {
//            m_abilityToFire = currentRobot.AimAbility(part);
//        }
//    }

//    public void MoveTo()
//    {
//        Vector3 Destination = playerController.GetMoveInput();
//        currentRobot.GetComponent<RobotMovement>().MoveToPoint(Destination);
//    }

//    //[PunRPC]
//    private void SetTeam(int team)
//    {
//        this.playerTeam = team;
//    }

//    public void MulticastSetTeam(int team)
//    {
//        photonView.RPC("SetTeam", RpcTarget.All, team);
//    }

//    //[PunRPC]
//    private void SetName(string name)
//    {
//        gameObject.name = name;
//    }

//    public void MulticastSetName(string name)
//    {
//        photonView.RPC("SetName", RpcTarget.All, name);
//    }
//}