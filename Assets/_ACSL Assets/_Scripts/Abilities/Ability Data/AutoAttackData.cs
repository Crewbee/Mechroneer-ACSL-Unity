using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AutoAttack")]
public class AutoAttackData : ScriptableObject
{
    public float cooldown;
    public Vector2 range;
    public List<EffectData> effect;

    public void Attack(IEffectUser owner, IEffectUser target, int customSpawnPointID)
    {
        foreach (EffectData effectToActive in effect)
        {
            effectToActive.ActivateEffect(owner, target, target.GetGameObject().transform.position, new SomethingAbility(range.y, TargetingStyle.Targeted, TargetMask.Enemies, customSpawnPointID, (target.GetGameObject().transform.position - owner.GetGameObject().transform.position).normalized, range.y));
        }
    }
}
