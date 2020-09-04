using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/Damage Reduction")]
public class EDDamageReduction : EffectData
{

    public float damageMultiplier;
    public float lifeTime;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        if (target == null)
            return;

        target.ApplyDamageReudctionEffect(new DamageReductionEffect(target, this));

    }
}
