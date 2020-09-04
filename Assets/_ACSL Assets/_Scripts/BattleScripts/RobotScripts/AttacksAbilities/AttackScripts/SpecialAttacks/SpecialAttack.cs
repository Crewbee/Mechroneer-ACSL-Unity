using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAttack : ISpecialAttack
{
    public enum DamageType
    {
        Piercing = 0,
        Expolsive,
        Blunt,
        Electric,
        Slashing
    }

    public float m_CooldownTime = 8.0f;
    public MyTimer m_Cooldown;

    public bool m_IsActive = false;
    public GameObject Owner;
    public DamageType m_DamageType { get; protected set; }

    public float m_SpecialDamage { get; protected set; }

   public virtual void SetOwner(GameObject owner)
    {

        Owner = owner;
    }    

   public virtual void Activated(bool isActivated) {

        m_IsActive = isActivated;
   }

    public virtual void CalculateDamage() {}

    public virtual void AttackSpeedCalc() {}

    public virtual void Update()
    { }
}
