using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Ability/Effects/Spawnable")]
public abstract class EDSpawnable : EffectData
{
    [Header("Spawn Data")]
    //public Spawnable objectToSpawn;
    public List<EffectData> onObjectSpawned;
    public List<EffectData> onObjectDespawned;
    public List<EffectData> onTriggerStay;
    public List<EffectData> onObjectHit;
    
    public float lifeTime;
    public bool followTarget;

    public float triggerStayTickRate;

    [Header("Overrides")]
    public bool spawnOnOwner;
    public bool useCustomSpawnPoint;
    public bool destroyOnSkillshotHit;
    public bool destroyOnCollision;
    public List<string> collisionTags;
    public bool spawnFromSelf;

    protected Spawnable SpawnObject(Spawnable aObject, IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Spawnable objectSpawned = Instantiate(aObject);
        objectSpawned.transform.position = DetermineSpawnPos(caller, target, mousePos, abilityData);

        if (abilityData.direction != Vector3.zero)
        objectSpawned.transform.rotation = Quaternion.LookRotation(abilityData.direction);

        objectSpawned.OnSpawned(caller, target, mousePos, abilityData, this);
        return objectSpawned;
    }

    protected Vector3 DetermineSpawnPos(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        TargetingStyle style = overrideTargetingStyle ? overridenStyle : abilityData.style;
        switch (style)
        {
            case TargetingStyle.Self:
                if (spawnOnOwner)
                    return (useCustomSpawnPoint) ? caller.GetOwner().GetCustomSpawnPoint(abilityData.customSpawnPointID).position : caller.GetOwner().GetGameObject().transform.position;
                else
                    return (useCustomSpawnPoint) ? caller.GetCustomSpawnPoint(abilityData.customSpawnPointID).position : caller.GetGameObject().transform.position;

            case TargetingStyle.Skillshot:

                Vector3 callerPos = (useCustomSpawnPoint) ? caller.GetCustomSpawnPoint(abilityData.customSpawnPointID).position : caller.GetGameObject().transform.position;
                Vector3 direction = abilityData.direction;
                if (spawnFromSelf)
                    return callerPos + direction;

                if (abilityData.inputRange > abilityData.abilityRange)
                    return callerPos + direction * abilityData.abilityRange;
                return mousePos;

            case TargetingStyle.Targeted:
                return target.GetGameObject().transform.position;

            default:
                return Vector3.zero;
        }
    }
}