using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/Spawnable Circle Collider")]
public class EDSpawnableSphereCollider : EDSpawnable
{
    [Header("Sphere Spawnable")]
    public SpawnableSphereCollider objectToSpawn;
    public float hitRadius;
    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Spawnable objectSpawned = SpawnObject(objectToSpawn, caller, target, mousePos, abilityData);
        objectSpawned.transform.position = DetermineSpawnPos(caller, target, mousePos, abilityData);
    }
}
