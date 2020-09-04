using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
public class RobotArm : RobotPart
{
    public Transform muzzle;
    public AutoAttackData autoAttackData;
    public MyTimer autoAttackTimer;


    #region Unused Variables
    //[System.Serializable]
    //private delegate void m_aaFunc(AbilityData data, Transform position, Transform Direction);
    //[System.Serializable]
    //private delegate void m_saFunc(AbilityData data, Transform position, Transform Direction);

    //private m_aaFunc m_autoAttackFunc;
    //private m_saFunc m_specialAbilityFunc;


    //public SpecialAttack m_Special { get; set; }

    //public float m_Range;

    //public float m_Accuracy;

    //public float m_AttacksPerSecond;

    //public float m_Damage;

    //public bool m_Activate = false;

    //public WeaponBehavior m_Behavior;
    #endregion

    public bool m_IsLeftArm { get; set; }


    protected override void Awake()
    {
        base.Awake();
        autoAttackTimer = new MyTimer();
    }

    protected override void Update()
    {
        base.Update();
        autoAttackTimer.Update();
    }

    public void PerformAutoAttack(IEffectUser robotParent, IEffectUser target)
    {
        if (!autoAttackData || target == null)
        {
            //Debug.Log(gameObject.name + " has no auto attack data");
            StopAutoAttack();
            return;
        }
        float distanceFromTarget = (target.GetGameObject().transform.position - transform.position).magnitude;
        if (distanceFromTarget > autoAttackData.range.y)
            return;
        if (muzzle)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Robot");
            layerMask += 1 << LayerMask.NameToLayer("Ignore Raycast");
            layerMask = int.MaxValue ^ layerMask;
            float range = (target.GetMainTarget().transform.position - muzzle.position).magnitude;
            if (Physics.Raycast(muzzle.position, (target.GetMainTarget().transform.position - muzzle.position) / range, out RaycastHit hitInfo, range, layerMask))
            {
                //Debug.Log(hitInfo.collider.name);
                return;
            }
            if (m_Animator)
            {
                m_Animator.SetBool("AutoAttack", true);
            }
        }
        else
        {
            if (m_Animator)
            {
                m_Animator.SetTrigger("AutoAttack");
            }
        }

        if (autoAttackTimer != null)
        {
            if (!autoAttackTimer.active)
            {
                autoAttackData.Attack(robotParent, target, (int)partType);
                if (LobbySettings.GetIsOnlineMatch())
                    photonView.RPC("NetworkAutoAttack", RpcTarget.Others, target.GetObjectID());
                autoAttackTimer.StartTimer(autoAttackData.cooldown);
            }
        }
    }

    [PunRPC]
    public void NetworkAutoAttack(int targetID)
    {
        autoAttackData.Attack(RobotRegistry.data[playerID], RobotRegistry.data[targetID], (int)partType);
    }

    public void StopAutoAttack()
    {
        if (m_Animator)
        {
            if (muzzle)
            {
                m_Animator.SetBool("AutoAttack", false);
            }
        }
    }


    override public Transform GetCustomSpawnPoint()
    {
        return (muzzle) ? muzzle : transform;
    }
}
