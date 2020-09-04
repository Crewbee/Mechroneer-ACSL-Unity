using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomethingAbility
{
    public float abilityRange;
    public TargetingStyle style;
    public TargetMask mask;
    public int customSpawnPointID;
    public Vector3 direction;
    public float inputRange;

    public SomethingAbility()
    {
        abilityRange = 0;
        style = TargetingStyle.Self;
        mask = TargetMask.Self;
        customSpawnPointID = 0;
        direction = Vector3.forward;
        inputRange = 0;
    }
    public SomethingAbility(float range, TargetingStyle style, TargetMask mask, int customSpawnPointID, Vector3 direction, float inputRange)
    {
        this.abilityRange = range;
        this.style = style;
        this.mask = mask;
        this.customSpawnPointID = customSpawnPointID;
        direction.y = 0;
        this.direction = direction.normalized;
        this.inputRange = inputRange;// (inputRange > range) ? range : inputRange;
    }
}

public abstract class EffectData : ScriptableObject
{
    public bool overrideTargetingStyle;
    public TargetingStyle overridenStyle;

    public abstract void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData);
}