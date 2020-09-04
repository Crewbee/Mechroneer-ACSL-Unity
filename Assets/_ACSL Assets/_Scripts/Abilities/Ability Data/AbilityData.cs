using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TargetingStyle
{
    Targeted,
    Skillshot,
    Self
}
public enum TargetMask
{
    Self = 1,
    Allies = 2,
    Enemies = 4,
    All = Self + Allies + Enemies,
}

[CreateAssetMenu(menuName = "Ability/Ability")]
public class AbilityData : ScriptableObject
{
    [Header("Ability Visuals")]
    public string abilityName = "Ability";
    public string abilityDescription = "It does something!";
    public Sprite abilityIcon;
    public Color abilityColor = Color.white;

    [Header("Ability Attributes")]
    public int energyCost;
    public float cooldown;
    public Vector2 range;

    public int charges = 1;
    public float rechargeRate;

    public List<EffectData> effects;

    [Header("Targeting")]
    public TargetingStyle targetingStyle;
    public TargetMask targetMask = TargetMask.All;


    public void ActivateAbility(IEffectUser caller, IEffectUser target, Vector3 mousePos, int customSpawnPointID = 0)
    {
        Vector3 direction = mousePos - caller.GetGameObject().transform.position;
        foreach (EffectData effect in effects)
        {
            effect.ActivateEffect(caller, target, mousePos, new SomethingAbility(range.y, targetingStyle, targetMask, customSpawnPointID, direction.normalized, direction.magnitude));
        }
    }

    static public bool TargetMaskEqual(TargetMask left, TargetMask right)
    {
        return (left & right) == right;
    }

    static public bool IsValidAbilityTarget(IEffectUser caller, IEffectUser target, AbilityData abilityData, bool isTeamMode)
    {
        if (caller == null)
            return false;
        if (target == null)
            return false;

        bool isValidTarget = false;

        if (TargetMaskEqual(abilityData.targetMask, TargetMask.Self))
        {
            if (caller.GetGameObject() == target.GetGameObject())
                isValidTarget = true;
        }
        if (TargetMaskEqual(abilityData.targetMask, TargetMask.Enemies))
        {
            if (!isTeamMode)
            {
                if (caller.GetGameObject() != target.GetGameObject())
                    isValidTarget = true;
            }
            else
            {
                if (caller.GetTeamID() != target.GetTeamID())
                    isValidTarget = true;
            }
        }
        if (TargetMaskEqual(abilityData.targetMask, TargetMask.Allies))
        {
            if (!isTeamMode)
            {
                if (caller.GetGameObject() == target.GetGameObject())
                    isValidTarget = true;
            }
            else
            {
                if (caller.GetTeamID() == target.GetTeamID())
                    isValidTarget = true;
            }
        }

        return isValidTarget;
    }
}
