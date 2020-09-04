using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(EnergyComponent))]
[RequireComponent(typeof(SenseComponent))]
public class Robot : Player, IEffectUser, MechroneerController.IActions
{
    private MechroneerDriver m_driver;
    public PhotonView photonView { get; private set; }

    private RobotData m_robotData;
    private int m_teamID;
    private int m_playerID;

    public HealthComponent healthComponent { get; private set; }
    public EnergyComponent energyComponent { get; private set; }
    public SenseComponent senseComponent { get; private set; }
   
    public ParticleSystem ONDeath;

    public NavMeshAgent navMeshAgent { get; private set; }

    protected NavMeshPath m_navMeshPath;
    private Vector3 currentDirection;
    protected int pathIndex = 0;
    private Vector3 oldPosition;
    private Vector3 currentUp;
    public float m_robotSpeed;

    public Dictionary<RobotPartType, RobotPart> robotParts { get; protected set; }

    private List<GameObject> m_partsToRotateIndependantly;
    private List<EffectBase> m_activeEffects;
    private List<EffectBase> m_effectsToRemove;
    private List<DamageReductionEffect> m_damageReductionEffects;
    private List<DamageReductionEffect> m_damageReductionEffectsToRemove;

    public GameObject floatingDamageSlight;
    public GameObject floatingDamageMed;
    public GameObject floatingDamageCrit;

    public delegate void OnRobotDeath(Robot caller);
    public event OnRobotDeath onRobotDeath;

    public delegate void PauseFunction();
    public PauseFunction pauseFunction;

    public RobotPart abilityToFire { get; protected set; }
    protected IEffectUser m_autoTarget;
    protected GameObject m_autoTargetGameObject;

    public TapToMoveArrow tapToMoveArrowPrefab;
    protected TapToMoveArrow m_tapToMoveArrow;

    public LineRenderer m_aimIndicatorPrefab;
    private LineRenderer m_aimIndicator;

    public GameObject playerIndicatorPrefab;

    #region Initializing Functions
    public void Init(int playerID, int teamID, string name, RobotData robotData)
    {
        gameObject.name = name;
        m_playerID = playerID;
        m_teamID = teamID;

        photonView = PhotonView.Get(this);

        if (LobbySettings.GetIsOnlineMatch())
        {
            photonView.RPC("RegisterRobot", RpcTarget.Others, playerID, teamID, name);
        }
        RobotRegistry.data[m_playerID] = this;

        m_robotData = robotData;
        m_navMeshPath = new NavMeshPath();

        healthComponent = GetComponent<HealthComponent>();
        energyComponent = GetComponent<EnergyComponent>();
        senseComponent = GetComponent<SenseComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        m_activeEffects = new List<EffectBase>();
        m_effectsToRemove = new List<EffectBase>();
        m_damageReductionEffects = new List<DamageReductionEffect>();
        m_damageReductionEffectsToRemove = new List<DamageReductionEffect>();

        healthComponent.onResourceUsed += OnDamageTaken;
        healthComponent.onResourceReachesZero += OnHealthReachZero;

        m_robotData.UpdateValues();
        healthComponent.Init(m_robotData.health);
        energyComponent.Init(m_robotData.energy);
        m_robotSpeed = m_robotData.speed;

        //TODO: Find a better way to do this
        if (!PhotonNetwork.InRoom || !LobbySettings.GetIsOnlineMatch())
        {
            if (playerID == 1)
            {
                m_driver = Camera.main.GetComponent<MechroneerDriver>();
                m_driver.AddTarget(transform);
                m_tapToMoveArrow = Instantiate(tapToMoveArrowPrefab);
                m_aimIndicator = Instantiate(m_aimIndicatorPrefab); m_aimIndicator.gameObject.SetActive(false);
                GameObject playerIndicator = Instantiate(playerIndicatorPrefab, transform);
                playerIndicator.GetComponent<PlayerIndicator>().ChangeTarget(gameObject);
                playerIndicator.transform.localPosition = new Vector3(0, 5, 0);
            }
        }
        else if (photonView.IsMine)
        {
            m_driver = Camera.main.GetComponent<MechroneerDriver>();
            m_driver.AddTarget(transform);
            m_tapToMoveArrow = Instantiate(tapToMoveArrowPrefab);
            m_aimIndicator = Instantiate(m_aimIndicatorPrefab); m_aimIndicator.gameObject.SetActive(false);
            GameObject playerIndicator = Instantiate(playerIndicatorPrefab, transform);
            playerIndicator.GetComponent<PlayerIndicator>().ChangeTarget(gameObject);
            playerIndicator.transform.localPosition = new Vector3(0, 5, 0);
        }
        Build();
    }

    private void Start()
    {
        if (!photonView)
        {
            photonView = PhotonView.Get(this);
        }
    }

    [PunRPC]
    private void RegisterRobot(int playerID, int teamID, string name)
    {
        gameObject.name = name;
        m_playerID = playerID;
        m_teamID = teamID;

        if (RobotRegistry.data == null)
        {
            RobotRegistry.data = new Dictionary<int, Robot>();
        }

        RobotRegistry.data[m_playerID] = this;

        healthComponent = GetComponent<HealthComponent>();
        energyComponent = GetComponent<EnergyComponent>();
        senseComponent = GetComponent<SenseComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Build()
    {
        if (robotParts == null)
        {
            robotParts = new Dictionary<RobotPartType, RobotPart>();
        }

        RobotLeg legs;
        RobotBody body;
        RobotArm rightArm;
        RobotArm leftArm;
        RobotHead head;

        if (!PhotonNetwork.InRoom)
        {
            legs = Instantiate(m_robotData.legs, transform);
            body = Instantiate(m_robotData.body, legs.transform.GetChild(0).position, Quaternion.identity, legs.transform.GetChild(0));
            rightArm = Instantiate(m_robotData.rArm, body.transform.GetChild(2).position, Quaternion.identity, body.transform.GetChild(2));
            leftArm = Instantiate(m_robotData.lArm, body.transform.GetChild(1).position, Quaternion.identity, body.transform.GetChild(1));
            head = Instantiate(m_robotData.head, body.transform.GetChild(0).position, Quaternion.identity, body.transform.GetChild(0));
        }
        else
        {
            legs = PhotonNetwork.Instantiate(m_robotData.legs.name, transform.position, Quaternion.identity).GetComponent<RobotLeg>();
            body = PhotonNetwork.Instantiate(m_robotData.body.name, legs.transform.GetChild(0).position, Quaternion.identity).GetComponent<RobotBody>();
            rightArm = PhotonNetwork.Instantiate(m_robotData.rArm.name, body.transform.GetChild(2).position, Quaternion.identity).GetComponent<RobotArm>();
            leftArm = PhotonNetwork.Instantiate(m_robotData.lArm.name, body.transform.GetChild(1).position, Quaternion.identity).GetComponent<RobotArm>();
            head = PhotonNetwork.Instantiate(m_robotData.head.name, body.transform.GetChild(0).position, Quaternion.identity).GetComponent<RobotHead>();

            legs.transform.SetParent(transform);
            body.transform.SetParent(legs.transform.GetChild(0));
            rightArm.transform.SetParent(body.transform.GetChild(2));
            leftArm.transform.SetParent(body.transform.GetChild(1));
            head.transform.SetParent(body.transform.GetChild(0));
        }

        robotParts[RobotPartType.Leg] = legs;
        robotParts[RobotPartType.Body] = body;
        robotParts[RobotPartType.LeftArm] = leftArm;
        robotParts[RobotPartType.RightArm] = rightArm;
        robotParts[RobotPartType.Head] = head;

        if (PhotonNetwork.InRoom)
        {
            foreach (RobotPart part in robotParts.Values)
            {
                part.RegisterPart(m_playerID);
            }
            photonView.RPC("ClientParentParts", RpcTarget.Others);
        }

        m_partsToRotateIndependantly = new List<GameObject>
        {
            head.gameObject,
            rightArm.gameObject,
            leftArm.gameObject,
            body.gameObject
        };
    }

    [PunRPC]
    private void ClientParentParts()
    {
        if (robotParts == null)
        {
            robotParts = new Dictionary<RobotPartType, RobotPart>();
        }

        List<RobotPart> findParts = new List<RobotPart>();
        findParts.AddRange(FindObjectsOfType<RobotPart>());

        FindAndParentPart(RobotPartType.Leg, transform, ref findParts);
        FindAndParentPart(RobotPartType.Body, transform, ref findParts);
        FindAndParentPart(RobotPartType.LeftArm, robotParts[RobotPartType.Body].transform, ref findParts);
        FindAndParentPart(RobotPartType.RightArm, robotParts[RobotPartType.Body].transform, ref findParts);
        FindAndParentPart(RobotPartType.Head, robotParts[RobotPartType.Body].transform, ref findParts);
    }

    private void FindAndParentPart(RobotPartType type, Transform parent, ref List<RobotPart> findParts)
    {
        while (findParts.Count > 0)
        {
            int lastIndex = findParts.Count - 1;
            RobotPart part = findParts[lastIndex];
            findParts.RemoveAt(lastIndex);

            if (part.partType != type)
            {
                continue;
            }

            if (part.playerID != m_playerID)
            {
                continue;
            }

            part.transform.SetParent(parent);
            robotParts[type] = part;

            break;
        }
    }
    #endregion

    #region Update Functions
    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            if (!photonView.IsMine)
            {
                return;
            }
        }

        UpdateEffects();
        UpdateMovement();
        AutoAttackTarget();
        UpdateAimIndicator();
    }

    private void UpdateEffects()
    {
        if (m_effectsToRemove.Count > 0)
        {
            foreach (EffectBase effect in m_effectsToRemove)
            {
                m_activeEffects.Remove(effect);
            }

            m_effectsToRemove.Clear();
        }

        if (m_damageReductionEffectsToRemove.Count > 0)
        {
            foreach (DamageReductionEffect effect in m_damageReductionEffectsToRemove)
            {
                m_damageReductionEffects.Remove(effect);
            }

            m_damageReductionEffectsToRemove.Clear();
        }

        foreach (EffectBase effect in m_activeEffects)
        {
            effect.Update();
        }

        foreach (DamageReductionEffect effect in m_damageReductionEffects)
        {
            effect.Update();
        }
    }

    private void UpdateMovement()
    {

        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        Vector3 direction = oldPosition - transform.position;
        oldPosition = transform.position + (direction.normalized * 1.0f);
        direction.y = 0;

        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 20.0f, layerMask))
        {
            currentUp = Vector3.Lerp(currentUp, hit.normal, 3.0f * Time.deltaTime);

            if (-direction.normalized != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(-direction.normalized, currentUp);
            }

            transform.rotation = Quaternion.FromToRotation(Vector3.ProjectOnPlane(transform.up, -direction.normalized), currentUp) * transform.rotation;
        }

        //NavMeshAgent is only used to caluculate paths so it just teleports to the object's location
        navMeshAgent.Warp(transform.position);

        //Slap this in Player's movement but not AI's, all AI has to do is pass in a new location using MoveToPoint
        if (m_navMeshPath.corners.Length > pathIndex)
        {
            //m_NavMeshAgent.enabled = false;
            if (transform.position != m_navMeshPath.corners[pathIndex])
            {
                currentDirection = m_navMeshPath.corners[pathIndex] - transform.position;

                transform.position = transform.position + currentDirection.normalized * m_robotSpeed * Time.deltaTime;

                float distance = Vector3.Distance(transform.position, m_navMeshPath.corners[pathIndex]);
                if (distance < 1.0f)
                {
                    pathIndex++;
                }
            }
        }
        else
        {
            //m_NavMeshAgent.enabled = true;
        }

    }

    private void AutoAttackTarget()
    {
        TurnToFaceTarget();

            (robotParts[RobotPartType.LeftArm] as RobotArm).PerformAutoAttack(this, m_autoTarget);
            (robotParts[RobotPartType.RightArm] as RobotArm).PerformAutoAttack(this, m_autoTarget);


            if (!((robotParts[RobotPartType.LeftArm] as RobotArm).InRange(m_autoTarget) || (robotParts[RobotPartType.RightArm] as RobotArm).InRange(m_autoTarget)))
            {
                m_autoTarget = null;
                m_autoTargetGameObject = null;
            }
    }

    public void TurnToFaceTarget()
    {
        //if the target is not null
        #region Rotate Parts To Face The Target Object
        if (m_autoTargetGameObject != null)
        {
            if (robotParts[RobotPartType.LeftArm] != null && robotParts[RobotPartType.RightArm] != null)
            {
                // Calculate looking direction for robot transform
                Vector3 lookDir = m_autoTargetGameObject.transform.position - transform.position;
                Quaternion lookDirection;
                if (lookDir != Vector3.zero)
                {
                    lookDirection = Quaternion.LookRotation(lookDir);
                }

                // Calculate aiming direciton for robot parts
                Vector3 aimDir = m_autoTargetGameObject.transform.position - transform.position;

                if (aimDir != Vector3.zero)
                {
                    Quaternion aimDirection = Quaternion.LookRotation(aimDir);

                    // Rotate robot head and arms
                    foreach (GameObject part in m_partsToRotateIndependantly)
                    {
                        Quaternion partRot = part.transform.rotation;
                        partRot = Quaternion.Slerp(partRot, aimDirection, Time.deltaTime * 20);
                        part.transform.rotation = partRot;
                    }
                }
            }
        }
        #endregion

        //otherwise
        #region Rotate Parts To The Robots Forward 
        else
        {
            foreach (GameObject part in m_partsToRotateIndependantly)
            {
                if (part != null)
                {
                    if (part == m_partsToRotateIndependantly.Last())
                    {
                        Quaternion partRot = part.transform.rotation;
                        partRot = Quaternion.Slerp(partRot, transform.rotation, Time.deltaTime * 5);
                        part.transform.rotation = partRot;
                    }
                    else
                    {
                        Quaternion partRot = part.transform.rotation;
                        partRot = Quaternion.Slerp(partRot, transform.rotation, Time.deltaTime * 10);
                        part.transform.rotation = partRot;
                    }
                }
            }
        }
        #endregion
    }
    private void UpdateAimIndicator()
    {
        if (m_aimIndicator == null)
            return;

        if (abilityToFire)
        {
            Vector3 input = AbilityTarget.GetMousePosition();
            if (input.z >= Mathf.Infinity)
            {
                m_aimIndicator.gameObject.SetActive(false);
            }
            else
            {
                m_aimIndicator.SetPosition(0, transform.position);
                m_aimIndicator.startWidth = 0.1f;
                m_aimIndicator.endWidth = 1.0f;
                m_aimIndicator.SetPosition(1, m_aimIndicator.transform.position);
                input.y += 0.1f;
                m_aimIndicator.transform.position = input;
                m_aimIndicator.gameObject.SetActive(true);
                m_aimIndicator.transform.Rotate(new Vector3(0, 150.0f * Time.deltaTime, 0), Space.World);
            }
        }
        else
        {
            m_aimIndicator.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Input Functions
    public void SelectAbilityToFire(RobotPartType type)
    {
        if (type == RobotPartType.Head)
        {
            abilityToFire = null;
        }
        else if (abilityToFire)
        {
            RobotPart partToCheck = robotParts[type];
            if (partToCheck == abilityToFire)
            {
                abilityToFire = null;
            }
            else
            {
                abilityToFire = partToCheck;
            }
        }
        else
        {
            abilityToFire = robotParts[type];
        }

        if (abilityToFire)
        {
            if (!CanSelectAbility(abilityToFire))
            {
                abilityToFire = null;
            }
            else if (abilityToFire.abilityData.targetingStyle == TargetingStyle.Self)
            {
                FireAbility(this, transform.position);
            }
        }
    }

    private bool CanSelectAbility(RobotPart robotPart)
    {
        if (!robotPart.AbilityCanFire())
        {
            return false;
        }

        if (!energyComponent.HasEnoughEnergy(robotPart.abilityData.energyCost))
        {
            return false;
        }

        return true;
    }

    public void FireAbility(IEffectUser target, Vector3 mousePos)
    {
        if (!abilityToFire)
        {
            return;
        }

        if (abilityToFire.abilityData.targetingStyle == TargetingStyle.Targeted)
        {
            if (!IsValidAbilityTarget(target))
            {
                abilityToFire = null;
                return;
            }
            if (!(abilityToFire.InRange(target)))
            {
                abilityToFire = null;
                return;
            }
        }
        energyComponent.UseResource(abilityToFire.abilityData.energyCost);
        abilityToFire.FireAbility(this, target, mousePos);
        abilityToFire = null;
    }

    public void SetAutoTarget(IEffectUser target)
    {

        if (IsValidAutoTarget(target))
        {
            m_autoTarget = target;
            m_autoTargetGameObject = target.GetGameObject();
        }
        else
        {
            //m_autoTargetGameObject = null;
        }
    }

    private IEffectUser FindTarget(Vector3 mousePosition)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("Robot");
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 100, layerMask))
        {
            GameObject objectHit = hitInfo.collider.gameObject;
            IEffectUser holder = objectHit.GetComponent<IEffectUser>();
            if (holder == null)
            {
                holder = objectHit.GetComponentInParent<IEffectUser>();
            }

            return holder;
        }

        return null;
    }

    public void MoveToPoint(Vector3 point) //using nav mesh agent
    {
        if (navMeshAgent)
        {
            if (navMeshAgent.isActiveAndEnabled == true)
            {
                NavMeshPath testPath = new NavMeshPath();

                if (navMeshAgent.CalculatePath(point, testPath) == true)
                {
                    navMeshAgent.CalculatePath(point, m_navMeshPath);
                    pathIndex = 1;
                }
            }
        }
    }

    public Vector3 GetMoveInput(Vector3 mousePosition)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("EventSystem.current.IsPointerOverGameObject");
            return Vector3.one * Mathf.Infinity;
        }
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hitInfo = new RaycastHit();

        int layerMask = 1 << 10;
        layerMask = ~layerMask;

        RaycastHit[] allHits = Physics.RaycastAll(ray);
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            return hitInfo.point;
        }
#endif
#if UNITY_IOS || UNITY_ANDROID
        DebugText.AddMessage("Checking for touch", 1.0f);
        if (Input.touchCount > 0)
        {
            DebugText.AddMessage("Touch found", 10.0f);
            Vector2 touchPosition = Input.GetTouch(0).position;
            //m_arRaycastManager.Raycast(touchPosition, s_)
            GameObject arcamera = GameObject.Find("AR Camera");
            if (arcamera)
            {
                DebugText.AddMessage("Camera Found", 10.0f);
                Camera camera = arcamera.GetComponent<Camera>();
                Ray ARray = camera.ScreenPointToRay(touchPosition);
                DebugText.AddMessage("Trying to move - raycast", 10.0f);
                RaycastHit ARhitInfo = new RaycastHit();
                if (Physics.Raycast(ARray, out ARhitInfo))
                {
                    DebugText.AddMessage("Found - raycast", 10.0f);
                    return ARhitInfo.point;
                }
            }
        }
#endif
        return Vector3.one * Mathf.Infinity;
    }

    private void SetTapArrowTo(Vector3 position)
    {
        m_tapToMoveArrow.SetPosition(position + new Vector3(0, 0.1f, 0));
    }

    private bool IsValidAutoTarget(IEffectUser target)
    {
        if (target == null)
        {
            return false;
        }

        if ((target as Object) == this)
        {
            return false;
        }

        if (target.GetGameObject().tag == "Spawnable")
        {
            return false;
        }

        return true;
    }

    protected virtual bool IsValidAbilityTarget(IEffectUser target)
    {
        if (!abilityToFire)
        {
            return false;
        }

        if (target == null)
            return false;

        if (target.GetGameObject().tag == "Spawnable")
        {
            return false;
        }

        return abilityToFire.ValidAbilityTarget(this, target, false);
    }
    #endregion

    #region Health Callbacks
    [PunRPC]
    protected void OnHealthReachZero()
    {
        if (LobbySettings.GetIsOnlineMatch())
        {
            if (photonView.IsMine)
            {
                photonView.RPC("OnHealthReachZero", RpcTarget.Others);
            }
        }
        onRobotDeath?.Invoke(this);
        DestroyGameObject();
    }

    private void OnDamageTaken(float damageTaken)
    {
        SpawnFloatingDamageNumber(damageTaken);
    }

    [PunRPC]
    public void SpawnFloatingDamageNumber(float damageTaken)
    {
        if (!GameOptions.instance.m_DamageNumbersEnabled)
        {
            return;
        }

        if (photonView.IsMine)
        {
            photonView.RPC("SpawnFloatingDamageNumber", RpcTarget.Others, damageTaken);
        }
        if (damageTaken <= 0)
        {
            return;
        }

        if (damageTaken > 160)
        {
            GameObject go = Instantiate(floatingDamageCrit, transform.position, Quaternion.identity);
            go.GetComponent<DamageNumberScript>().UpdateFont(damageTaken);
            go.GetComponent<DamageNumberScript>().owner = transform.gameObject;
        }
        else if (damageTaken > 60)
        {
            GameObject go = Instantiate(floatingDamageMed, transform.position, Quaternion.identity);
            go.GetComponent<DamageNumberScript>().UpdateFont(damageTaken);
            go.GetComponent<DamageNumberScript>().owner = transform.gameObject;
        }
        else if (damageTaken <= 160)
        {
            GameObject go = Instantiate(floatingDamageSlight, transform.position, Quaternion.identity);
            go.GetComponent<DamageNumberScript>().UpdateFont(damageTaken);
            go.GetComponent<DamageNumberScript>().owner = transform.gameObject;
        }
    }

    #endregion

    #region IEffectUser Functions
    public void ApplyDamage(float damage)
    {
        if (photonView.IsMine || !LobbySettings.GetIsOnlineMatch())
        {
            float dmg = damage;
            if (m_damageReductionEffects.Count != 0)
            m_damageReductionEffects[0].ApplyReduction(ref dmg);
            healthComponent.UseResource(dmg);
        }
    }

    public void ApplyDamageReudctionEffect(DamageReductionEffect effect)
    {
        if (photonView.IsMine || !LobbySettings.GetIsOnlineMatch())
        {
            m_damageReductionEffects.Add(effect);
        }
    }

    public void ApplyEffect(EffectBase effect)
    {
        if (photonView.IsMine || !LobbySettings.GetIsOnlineMatch())
        {
            m_activeEffects.Add(effect);
        }
    }

    public void DestroyGameObject()
    {
        if (PhotonNetwork.InRoom)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
                Destroy(gameObject);
                ParticleSystem ONDeathPS = Instantiate(ONDeath,transform.position,Quaternion.identity);
                float totalDuration = ONDeathPS.main.duration;
                Destroy(ONDeathPS, totalDuration);
                Debug.Log("Photon Boom");
            }
        }
        else
        {
            Debug.Log("Offline  Boom");
            Destroy(gameObject);
            ParticleSystem ONDeathPS = Instantiate(ONDeath, transform.position, Quaternion.identity);
            float totalDuration = ONDeathPS.main.duration;
            Destroy(ONDeathPS, totalDuration);
        }
    }

    public List<EffectBase> GetActiveEffects()
    {
        return m_activeEffects;
    }

    public IEffectUser GetCreator()
    {
        return this;
    }

    public AudioSource[] GetCustomAudioSources(int customID)
    {
        return robotParts[(RobotPartType)customID].GetComponents<AudioSource>();
    }

    public Transform GetCustomSpawnPoint(int spawnPointID)
    {
        return robotParts[(RobotPartType)spawnPointID].GetCustomSpawnPoint();
    }

    public GameObject GetMainTarget()
    {
        return robotParts[RobotPartType.Body].gameObject;
    }

    public int GetObjectID()
    {
        return m_playerID;
    }

    public IEffectUser GetOwner()
    {
        return this;
    }

    public int GetTeamID()
    {
        return m_teamID;
    }

    public void RemoveDamageReductionEffect(DamageReductionEffect effect)
    {
        if (m_damageReductionEffects.Contains(effect))
        {
            m_damageReductionEffectsToRemove.Add(effect);
        }
    }

    public void RemoveEffect(EffectBase effect)
    {
        if (m_activeEffects.Contains(effect))
        {
            m_effectsToRemove.Add(effect);
        }
    }

    public virtual bool IsTeamMode()
    {
        return false;
    }

    #endregion

    #region Controller Actions
    public virtual void ZoomCamera(float input)
    {
        m_driver.UpdateZoom(input);
    }

    public virtual void RotateCamera(Vector2 input)
    {
        m_driver.UpdateRotation(input.x, input.y);
    }

    public virtual void ChangePerspective()
    {
        m_driver.SwitchPerspective();
    }

    public virtual void MovePlayer(Vector3 mousePos)
    {
        IEffectUser target = FindTarget(mousePos);
        if (abilityToFire)
        {
            FireAbility(target, GetMoveInput(mousePos));
        }
        else
        {
            SetAutoTarget(target);
            Vector3 destination = GetMoveInput(mousePos);

            if (destination.x < Mathf.Infinity)
            {
                MoveToPoint(destination);
                SetTapArrowTo(destination);
            }
        }
    }

    public virtual void SelectAbility1()
    {
        SelectAbilityToFire(RobotPartType.LeftArm);
    }

    public virtual void SelectAbility2()
    {
        SelectAbilityToFire(RobotPartType.RightArm);
    }

    public virtual void SelectAbility3()
    {
        SelectAbilityToFire(RobotPartType.Body);
    }

    public virtual void SelectAbility4()
    {
        SelectAbilityToFire(RobotPartType.Leg);
    }

    public virtual void PauseGame()
    {
        pauseFunction?.Invoke();
    }

    public override void OnControllerEnabled()
    {
        enabled = true;
    }

    public override void OnControllerDisabled()
    {
        enabled = false;
    }
    #endregion


    void OnDestroy()
    {
        if (m_tapToMoveArrow)
        {
            Destroy(m_tapToMoveArrow.gameObject);
        }
        if (m_aimIndicator)
        {
            Destroy(m_aimIndicator.gameObject);
        }
    }
}
