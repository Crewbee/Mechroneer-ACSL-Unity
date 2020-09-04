using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReductionEffect : EffectBase
{
    EDDamageReduction m_baseData;
    float m_timeAlive;
    public DamageReductionEffect(IEffectUser affectedObject, EDDamageReduction baseData) : base(affectedObject)
    {
        this.m_baseData = baseData;

        m_timeAlive = 0.0f;
    }

    public override void Update()
    {
        if (m_timeAlive > m_baseData.lifeTime)
        {
            m_affectedObject.RemoveDamageReductionEffect(this);
        }
        m_timeAlive += Time.deltaTime;
    }

    public void ApplyReduction(ref float damage)
    {
        damage *= m_baseData.damageMultiplier;
    }

    public override void FixedUpdate()
    {
    }
}
