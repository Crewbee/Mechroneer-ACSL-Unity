using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : EffectBase
{
    EDDash m_baseData;
    Rigidbody m_body;
    Robot m_robot;
    float m_defaultSpeed;
    Vector3 m_initialPos;
    Vector3 m_targetPos;
    Vector3 m_direction;
    MyTimer m_timer;

    public DashEffect(IEffectUser affectedObject, EDDash baseData, SomethingAbility abilityData) : base(affectedObject)
    {
        m_baseData = baseData;
        m_body = affectedObject.GetGameObject().GetComponent<Rigidbody>();
        m_robot = affectedObject.GetGameObject().GetComponent<Robot>();
        m_defaultSpeed = m_robot.m_robotSpeed;

        if (!m_body)
            affectedObject.RemoveEffect(this);
        m_initialPos = affectedObject.GetGameObject().transform.position;

        float amountToMove = (abilityData.inputRange > abilityData.abilityRange) ? abilityData.abilityRange : abilityData.inputRange;
        m_direction = abilityData.direction;
        m_targetPos = m_initialPos + amountToMove * abilityData.direction;

        m_timer = new MyTimer();
        m_timer.StartTimer(0.5f);
        float powerMul = (m_baseData.dashPower - m_body.velocity.magnitude);
        m_body.AddForce((m_direction * powerMul * m_body.mass), ForceMode.Impulse);
        m_robot.m_robotSpeed *= 4.0f;
    }

    public override void Update()
    {
        m_timer.Update();
        float distanceToTravel = (m_targetPos - m_initialPos).magnitude;
        float distanceTraveled = (m_targetPos - m_gameObject.transform.position).magnitude;

        if (distanceToTravel < distanceTraveled || m_timer.timeLeftSeconds <= 0.0f)
        {
            m_body.velocity = Vector3.zero;
            m_robot.m_robotSpeed = m_defaultSpeed;
            m_affectedObject.RemoveEffect(this);
        }
    }

    public override void FixedUpdate()
    {
        //m_body.AddForce(m_direction * powerMul * m_body.mass, ForceMode.Impulse);
        //float distanceToTravel = (m_targetPos - m_initialPos).magnitude;
        //float distanceTraveled = (m_targetPos - m_gameObject.transform.position).magnitude;
        //if (distanceToTravel < distanceTraveled)
        //{
        //    //float overshoot = distanceTraveled - distanceToTravel;
        //    m_body.AddForce(-m_direction * m_baseData.dashPower * m_body.mass, ForceMode.Impulse);
        //    m_affectedObject.RemoveEffect(this);
        //}

    }
    
}
