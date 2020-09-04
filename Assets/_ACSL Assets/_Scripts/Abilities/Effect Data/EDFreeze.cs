using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/FreezeDebuff")]
public class EDFreeze : EffectData
{
    [Header("Variables")]
    public float frozenDuration;
    public float damageOverTime;
    [Range(0.0f, 100.0f)]
    public float percentSpeedReduction;

    [Header("Effects")]
    public List<EffectData> onApplyFreeze;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Vector3 targetPos = target.GetGameObject().transform.position;

        foreach(EffectData effect in onApplyFreeze)
        {
            effect.ActivateEffect(caller, target, mousePos, abilityData);
        }

        FreezeEffect freeze = new FreezeEffect(target, this);

    }

}
