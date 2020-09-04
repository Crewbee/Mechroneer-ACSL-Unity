using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum RobotPartType
{
    Head,
    Body,
    RightArm,
    LeftArm,
    Leg
}

public abstract class RobotPart : MonoBehaviourPun
{
    #region Base Variables
    public float armor = 1.0f;
    public int energy = 75;
    public float health = 100.0f;
    public AbilityData abilityData;

    virtual public Animator m_Animator { get; protected set; }

    public Collider m_Collider;

    public delegate void RobotAbility(IEffectUser target, Vector3 mouseInput);
    public MyTimer specialAbilityTimer;
    public MyTimer abilityChargesTimer;
    public int availableAbilityCharges;

    public RobotPartType partType;

    public int playerID { get; private set; }
    #endregion

    virtual protected void Awake()
    {
        #region Initialize Timers
        specialAbilityTimer = new MyTimer();
        abilityChargesTimer = new MyTimer();
        #endregion

        #region If There Is An Ability Set The Number Of Charges
        if (abilityData)
        {
            availableAbilityCharges = abilityData.charges;
            if(GetComponent<Animator>() && m_Animator == null)
            m_Animator = GetComponent<Animator>();
            //m_Animator.SetBool("Idle", true);
        }
        #endregion
    }
    virtual protected void Update()
    {
        #region Update Timers
        specialAbilityTimer.Update();
        abilityChargesTimer.Update();
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject attack = other.gameObject;
    }

    public void FireAbility(IEffectUser robotParent, IEffectUser target, Vector3 mousePos)
    {
        Debug.Log("Fire Special Attack");
        #region Check If Ability Is Active
        if (specialAbilityTimer != null)
        {
            specialAbilityTimer.StartTimer(abilityData.cooldown);
        }
        if (abilityData.charges > 1)
        {
            availableAbilityCharges--;
            abilityChargesTimer.StartTimer(abilityData.rechargeRate, AddAbilityCharge);

        }
        #endregion

        if (m_Animator)
        {
            Debug.Log("Special Attack");
            m_Animator.SetTrigger("SpecialAttack");
        }
        else
        {
            Debug.Log("NO animator Special Attack");
        }
        #region Activate Ability
        if (LobbySettings.GetIsOnlineMatch())
            photonView.RPC("NetworkFireAbility", RpcTarget.Others, (target == null) ? -1 : target.GetObjectID(), mousePos);
        abilityData.ActivateAbility(robotParent, target, mousePos, (int)partType);
        #endregion
    }

    [PunRPC]
    public void NetworkFireAbility(int targetID, Vector3 mousePos)
    {
        abilityData.ActivateAbility(RobotRegistry.data[playerID], (targetID == -1) ? null : RobotRegistry.data[targetID], mousePos, (int)partType);
    }

    private void AddAbilityCharge()
    {
        #region If The Abilities Charge Has Cooled Down Add Charge
        availableAbilityCharges++;
        if (availableAbilityCharges < abilityData.charges)
            abilityChargesTimer.StartTimer(abilityData.rechargeRate, AddAbilityCharge);
        #endregion
    }

    public bool InRange(IEffectUser target)
    {
        #region Checks If Target Within Range
        if (target == null)
            return false;
        float distance = (target.GetGameObject().transform.position - transform.position).magnitude;
        if (distance > abilityData.range.y)
            return false;
        return true;
        #endregion
    }

    virtual public Transform GetCustomSpawnPoint()
    {
        return transform;
    }

    public bool ValidAbilityTarget(IEffectUser parentRobot, IEffectUser target, bool isTeamMode)
    {
        return AbilityData.IsValidAbilityTarget(parentRobot, target, abilityData, isTeamMode);
    }
    public bool AbilityCanFire()
    {
        #region Checks If Ability Can Be Activated
        if (abilityData.charges == 1)
        {
            return specialAbilityTimer.active == false;
        }
        else
        {
            return availableAbilityCharges > 0 && specialAbilityTimer.active == false;
        }
        #endregion
    }

    [PunRPC]
    public void RegisterPart(int playerID)
    {
        if (photonView.IsMine)
            photonView.RPC("RegisterPart", RpcTarget.Others, playerID);
        this.playerID = playerID;
    }
}
