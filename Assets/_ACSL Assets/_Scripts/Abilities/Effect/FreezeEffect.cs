using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Update this
public class FreezeEffect : EffectBase
{
    MyTimer m_Lifetime;
    MyTimer m_DamageTimer;
    float m_Damage;
    float m_SpeedReduction;
    float m_robotCurrentSpeed;
    Robot m_AffectedRobot;

    public FreezeEffect(IEffectUser affectedObject, EDFreeze data) : base(affectedObject)
    {
        m_Lifetime = new MyTimer();
        m_DamageTimer = new MyTimer();
        m_Lifetime.StartTimer(data.frozenDuration);
        m_DamageTimer.StartTimer(1.0f);
        m_Damage = data.damageOverTime / data.frozenDuration;
        m_Damage = Mathf.RoundToInt(m_Damage);
        m_SpeedReduction = data.percentSpeedReduction;
        m_AffectedRobot = affectedObject.GetGameObject().GetComponent<Robot>();

        RobotPart legRef;
        if (m_AffectedRobot != null)
        {
            m_AffectedRobot.robotParts.TryGetValue(RobotPartType.Leg, out legRef);
            RobotLeg leg = (RobotLeg)legRef;
            m_robotCurrentSpeed = leg.m_MovementSpeed;

            m_AffectedRobot.m_robotSpeed = m_robotCurrentSpeed * (m_SpeedReduction / 100.0f);
            m_AffectedRobot.ApplyEffect(this);
        }

    }
    // Update is called once per frame
    public override void Update()
    {
        m_Lifetime.Update();
        m_DamageTimer.Update();

        if (!m_DamageTimer.active)
        {
            m_AffectedRobot.ApplyDamage(m_Damage);
            m_DamageTimer.StartTimer(1.0f);
        }

        if (!m_Lifetime.active)
        {
            m_AffectedRobot.m_robotSpeed = m_robotCurrentSpeed;
            m_AffectedRobot.RemoveEffect(this);
        }

    }

    public override void FixedUpdate()
    {

    }
}
