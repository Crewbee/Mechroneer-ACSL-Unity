using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : EffectBase
{
    private EDProjectile m_data;
    private Vector3 m_initialPos;
    private Vector3 m_targetPos;
    private IEffectUser m_target;
    private int m_ricochetCount = 0;
    delegate void UpdateFunction();
    UpdateFunction m_updateFunction;

    private float m_distanceToTravel;
    private Vector3 m_directionToTravel;

    private GameObject m_targetGameObject;

    bool m_usesLifeTime;
    bool m_lockProjectileHeight;

    public ProjectileEffect(IEffectUser affectedObject, IEffectUser target, Vector3 targetPos, SomethingAbility abilityData, EDProjectile data, Vector3 initialPos) : base(affectedObject)
    {
        m_data = data;
        m_initialPos = initialPos;
        m_targetPos = targetPos;
        m_target = target;
        m_lockProjectileHeight = data.lockProjectileHeight;

        float maxRange = (data.overrideAbilityRange) ? data.range : abilityData.abilityRange;
        if (m_lockProjectileHeight)
        {
            m_directionToTravel = (new Vector3(m_targetPos.x, m_initialPos.y, m_targetPos.z) - m_initialPos).normalized;
        }
        else
        {
            m_directionToTravel = (new Vector3(m_targetPos.x, m_targetPos.y, m_targetPos.z) - m_initialPos).normalized;
        }
        m_distanceToTravel = (data.travelMaxRange) ? maxRange : (m_targetPos - m_initialPos).magnitude;
        if (m_distanceToTravel > maxRange)
            m_distanceToTravel = maxRange;

        m_usesLifeTime = data.lifeTime > 0;

        TargetingStyle style = (data.overrideTargetingStyle) ? data.overridenStyle : abilityData.style;

        switch (style)
        {
            case TargetingStyle.Targeted:
                if (m_target == null)
                    m_updateFunction = DefaultUpdate;
                else
                {
                    m_updateFunction = TargetedUpdate;
                    m_targetGameObject = m_target.GetGameObject();
                    //m_target.SubscribeOnEffectHolderDestroyed(OnTargetObjectDestroyed);
                }
                break;
            case TargetingStyle.Skillshot:
                m_updateFunction = SkillshotUpdate;
                break;
            default:
                m_updateFunction = DefaultUpdate;
                break;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        m_updateFunction();
    }

    void DefaultUpdate()
    {
    }

    void TargetedUpdate()
    {
        if (m_targetGameObject == null)
        {
            m_affectedObject.RemoveEffect(this);
            m_affectedObject.DestroyGameObject();
            return;
        }
        GameObject part = m_target.GetMainTarget();
        Vector3 targetPosition = part.transform.position;
        Vector3 currentPosition = m_gameObject.transform.position;
        Vector3 direction = (targetPosition - currentPosition).normalized;

        Vector3 nextPosition = currentPosition + direction * m_data.speed * Time.deltaTime;

        m_gameObject.transform.position = nextPosition;
    }

    void SkillshotUpdate()
    {
        Vector3 currentPosition = m_gameObject.transform.position;
        Vector3 nextPosition = currentPosition + m_directionToTravel * m_data.speed * Time.deltaTime;
        float distanceTraveled = (nextPosition - m_initialPos).magnitude;
        if (distanceTraveled > m_distanceToTravel)
        {
            if (!m_usesLifeTime)
            {
                m_affectedObject.RemoveEffect(this);
                m_affectedObject.DestroyGameObject();
            }
        }
        else
        {
            m_gameObject.transform.position = nextPosition;
        }
            
    }

    void OnTargetObjectDestroyed()
    {
        if (m_gameObject != null)
        {
            m_affectedObject.RemoveEffect(this);
            m_affectedObject.DestroyGameObject();
        }
    }

    public override void OnAffectedObjectDestroyed()
    {
        if (m_updateFunction == TargetedUpdate)
        {
            if (m_target != null)
            {
                //m_target.UnsubscribeOnEffectHolderDestroyed(OnTargetObjectDestroyed);
                m_target = null;
            }
        }
    }

    public override void FixedUpdate()
    {
    }
    public override void OnObjectHit(Collider other)
    {
        base.OnObjectHit(other);

        if (m_data.ricochetAmount > 0)
        {
            if (m_ricochetCount == m_data.ricochetAmount)
            {
                //Destroy affected objecct
                m_affectedObject.RemoveEffect(this);
                m_affectedObject.DestroyGameObject();
            }
            //else
            else
            {
                //Set Update function to skillshot
                if (m_updateFunction != SkillshotUpdate)
                {
                    m_updateFunction = SkillshotUpdate;
                }

                m_initialPos = m_gameObject.transform.position;

                LayerMask collisionMask = m_data.ricochetOffLayer;
                Ray ray = new Ray(m_gameObject.transform.position - m_directionToTravel, m_directionToTravel);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 reflectedVector = Vector3.Reflect(ray.direction, hit.normal);

                    float rot = 90.0f - Mathf.Atan2(reflectedVector.z, reflectedVector.x) * Mathf.Rad2Deg;

                    m_gameObject.transform.eulerAngles = new Vector3(m_gameObject.transform.eulerAngles.x, rot, m_gameObject.transform.eulerAngles.z);

                    m_directionToTravel = reflectedVector;

                    m_ricochetCount++;

                }
            }
        }


    }
}
