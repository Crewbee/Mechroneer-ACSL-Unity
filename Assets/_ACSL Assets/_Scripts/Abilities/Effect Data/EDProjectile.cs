using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/Projectile")]
public class EDProjectile : EDSpawnableSphereCollider
{
    [Header("Projectile Data")]
    public float damage;
    public float speed;
    public bool travelMaxRange;
    public bool lockProjectileHeight;

    [Header("Multi-Spawn Options")]
    public int amountToSpawn;
    [Range(0, 360)]
    public float spawnDegrees;

    [Header("Overrides")]
    public bool overrideAbilityRange;
    public float range;

    [Header("Ricochet")]
    public int ricochetAmount;
    public LayerMask ricochetOffLayer;


    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Transform spawner = DetermineSpawnTransform(caller, abilityData.customSpawnPointID);

        Vector3 spawnPos =  spawner.position;
        int amountSpawned = 0;
        
        Vector3 referenceAngle = abilityData.direction;

        if (amountToSpawn == 1)
        {
            SpawnProjectile(caller, target, mousePos, abilityData, spawnPos);
        }
        else
        {
            Quaternion referenceRotation = Quaternion.LookRotation(referenceAngle);
            float spawnDegreesInRad = spawnDegrees / 180 * 3.14f;
            float currentAngleInRad = -spawnDegreesInRad / 2;
            float maxRange = (overrideAbilityRange) ? range : abilityData.abilityRange;
            float distance = (abilityData.inputRange > maxRange || travelMaxRange) ? maxRange : abilityData.inputRange;

            Vector3 direction = new Vector3(Mathf.Sin(currentAngleInRad), 0, Mathf.Cos(currentAngleInRad));
            Quaternion startingRotation = referenceRotation * Quaternion.LookRotation(direction);
            float yRotation = startingRotation.eulerAngles.y / 180 * 3.14f;
            float angleInRadIncrease = spawnDegreesInRad / amountToSpawn;

            while (amountSpawned < amountToSpawn)
            {
                direction = new Vector3(Mathf.Sin(yRotation), 0, Mathf.Cos(yRotation));
                Vector3 rotatedMousePos = spawnPos + (direction) * distance;
                abilityData.direction = direction;
                SpawnProjectile(caller, target, rotatedMousePos, abilityData, spawnPos);

                yRotation += angleInRadIncrease;

                amountSpawned++;
            }
        }
    }

    Transform DetermineSpawnTransform(IEffectUser caller, int customSpawnPointID)
    {
        if (useCustomSpawnPoint)
            return (spawnOnOwner) ? caller.GetOwner().GetCustomSpawnPoint(customSpawnPointID) : caller.GetCustomSpawnPoint(customSpawnPointID);
        else
            return (spawnOnOwner) ? caller.GetOwner().GetGameObject().transform : caller.GetGameObject().transform;
    }

    Spawnable SpawnProjectile(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData, Vector3 initialPos)
    {
        Spawnable objectSpawned = SpawnObject(objectToSpawn, caller, target, mousePos, abilityData);

        objectSpawned.transform.position = initialPos;
        Spawnable component = objectSpawned.GetComponent<Spawnable>();

        component.ApplyEffect(new ProjectileEffect(component, target, mousePos, abilityData, this, objectSpawned.transform.position));
        return objectSpawned;
    }
}