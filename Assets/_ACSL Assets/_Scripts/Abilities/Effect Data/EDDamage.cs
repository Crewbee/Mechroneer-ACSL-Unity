using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/Damage")]
public class EDDamage : EffectData
{
    public float damage;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        if (target == null)
            return;
        target.ApplyDamage(damage);
    }
}
