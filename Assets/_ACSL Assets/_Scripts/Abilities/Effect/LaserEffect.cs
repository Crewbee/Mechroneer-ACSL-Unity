using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : EffectBase
{
    MyTimer m_lifeTime;

    LineRenderer m_lineRenderer;
    Color m_initialColor;
    Color m_finalColor;

    EDLaser m_baseData;

    public LaserEffect(IEffectUser affectedObject, Vector3 targetPos, IEffectUser target, SomethingAbility abilityData, EDLaser baseData) :
        base(affectedObject)
    {
        m_lineRenderer = affectedObject.GetGameObject().GetComponent<LineRenderer>();
        if (m_lineRenderer == null)
            m_lineRenderer = affectedObject.GetGameObject().AddComponent<LineRenderer>();

        Vector3 initialPos = m_lineRenderer.transform.position;
        this.m_baseData = baseData;

        m_lineRenderer.SetPosition(0, initialPos);
        TargetingStyle style = (baseData.overrideTargetingStyle) ? baseData.overridenStyle : abilityData.style;
        switch (style)
        {
            case TargetingStyle.Targeted:
                m_lineRenderer.SetPosition(1, target.GetMainTarget().transform.position);
                foreach (EffectData data in baseData.onObjectHit)
                {
                    data.ActivateEffect(affectedObject, target, targetPos, abilityData);
                }
                break;

            case TargetingStyle.Skillshot:
                float inputRange = (targetPos - initialPos).magnitude;
                Vector3 direction = (targetPos - initialPos).normalized;
                Vector3 finalPos;
                if (inputRange > abilityData.abilityRange)
                {
                    inputRange = abilityData.abilityRange;
                    finalPos = direction * inputRange;
                    m_lineRenderer.SetPosition(1, finalPos);
                }
                else
                {
                    finalPos = targetPos;
                    m_lineRenderer.SetPosition(1, finalPos);
                }

                RaycastHit[] targetsHit = Physics.RaycastAll(initialPos, direction, inputRange);
                foreach (RaycastHit hit in targetsHit)
                {
                    IEffectUser user = hit.collider.gameObject.GetComponent<IEffectUser>();
                    if (user != null)
                    {
                        foreach (EffectData data in baseData.onObjectHit)
                        {
                            data.ActivateEffect(affectedObject, target, targetPos, abilityData);
                        }
                    }
                }

                break;

            default:
                break;
        }

        m_lifeTime = new MyTimer();
        m_lifeTime.StartTimer(baseData.lifeTime, DestroyGameObject);
    }

    void DestroyGameObject()
    {
        m_affectedObject.DestroyGameObject();
    }

    public override void Update()
    {
        m_lineRenderer.startWidth -= Time.deltaTime / m_baseData.lifeTime / 0.7f;
        if (m_lineRenderer.startWidth < 0)
            m_lineRenderer.startWidth = 0;
        m_lineRenderer.endWidth -= Time.deltaTime / m_baseData.lifeTime / 1.0f;
        
        m_lifeTime.Update();
    }

    public override void FixedUpdate()
    {
    }
}
