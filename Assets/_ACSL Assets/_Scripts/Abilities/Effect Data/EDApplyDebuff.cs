using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/ApplyDebuff")]
public class EDApplyDebuff : EffectData
{
    public List<EffectData> debuffs;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        foreach(EffectData effect in debuffs)
        {
            effect.ActivateEffect(caller, target, mousePos, abilityData);
        }
    }
}
