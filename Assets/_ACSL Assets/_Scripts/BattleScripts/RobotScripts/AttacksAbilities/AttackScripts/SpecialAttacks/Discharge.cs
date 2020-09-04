using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discharge : SpecialAttack
{
    public void Activate()
    {

    }
    public override void CalculateDamage()
    {
        m_DamageType = DamageType.Electric;

        m_SpecialDamage = 20.0f;
    }

    public override void AttackSpeedCalc()
    {

    }
}
