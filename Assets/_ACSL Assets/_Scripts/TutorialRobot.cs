using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(EnergyComponent))]
[RequireComponent(typeof(SenseComponent))]
public class TutorialRobot : Robot, IEffectUser, MechroneerController.IActions
{
    // Events
    public delegate void ZoomDelegate(float input);
    public event ZoomDelegate ZoomCameraEvent;

    public delegate void RotateDelegate(Vector2 input);
    public event RotateDelegate RotateCameraEvent;

    public delegate void ChangePerspectiveDelegate();
    public event ChangePerspectiveDelegate ChangePerspectiveEvent;

    public delegate void MovePlayerDelegate();
    public event MovePlayerDelegate MovePlayerEvent;

    public delegate void AbilitySelected();
    public event AbilitySelected AbilitySelectedEvent;

    public delegate void AbilityFired();
    public event AbilityFired AbilityFiredEvent;

    public delegate void TargetSelectedDelegate();
    public event TargetSelectedDelegate TargetSelectedEvent;


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
                AbilitySelectedEvent?.Invoke();
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
            Debug.Log("abilityToFire returns");
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
        Debug.Log("abilityToFire fire");
        abilityToFire.FireAbility(this, target, mousePos);
        abilityToFire = null;
        AbilityFiredEvent?.Invoke();
    }

    public void SetAutoTarget(IEffectUser target)
    {
        //m_autoTarget = target;
        if (IsValidAutoTarget(target))
        {
            m_autoTarget = target;
            m_autoTargetGameObject = target.GetGameObject();
        }
        else
        {
            m_autoTargetGameObject = null;
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
            Debug.Log("Not vaild - target");
            return false;
        }

        if ((target as Object) == this)
        {
            Debug.Log("Not vaild - Object");
            return false;
        }

        if (target.GetGameObject().tag == "Spawnable")
        {
            Debug.Log("Not vaild - spawnable");
            return false;
        }
        Debug.Log("Valid");
        return true;
    }

    protected virtual bool IsValidAbilityTarget(IEffectUser target)
    {
        if (!abilityToFire)
        {
            return false;
        }

        if (target.GetGameObject().tag == "Spawnable")
        {
            return false;
        }

        return abilityToFire.ValidAbilityTarget(this, target, false);
    }
    #endregion


    #region Controller Actions
    public override void ZoomCamera(float input)
    {
        base.ZoomCamera(input);
        ZoomCameraEvent?.Invoke(input);
    }

    public override void RotateCamera(Vector2 input)
    {
        base.RotateCamera(input);
        RotateCameraEvent?.Invoke(input);
    }

    public override void ChangePerspective()
    {
        base.ChangePerspective();
        ChangePerspectiveEvent?.Invoke();
    }

    public override void MovePlayer(Vector3 mousePos)
    {
        IEffectUser target = FindTarget(mousePos);
        if (abilityToFire)
        {
            FireAbility(target, mousePos);
            AbilityFiredEvent?.Invoke();
        }
        else
        {
            SetAutoTarget(target);
            if (target != null)
                TargetSelectedEvent?.Invoke();

            Vector3 destination = GetMoveInput(mousePos);

            if (destination.x < Mathf.Infinity)
            {
                MoveToPoint(destination);
                SetTapArrowTo(destination);
                MovePlayerEvent?.Invoke();
            }
        }
    }

    public override void SelectAbility1()
    {
        SelectAbilityToFire(RobotPartType.LeftArm);
    }

    public override void SelectAbility2()
    {
        SelectAbilityToFire(RobotPartType.RightArm);
    }

    public override void SelectAbility3()
    {
        SelectAbilityToFire(RobotPartType.Body);
    }

    public override void SelectAbility4()
    {
        SelectAbilityToFire(RobotPartType.Leg);
    }

    public override void PauseGame()
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
}
