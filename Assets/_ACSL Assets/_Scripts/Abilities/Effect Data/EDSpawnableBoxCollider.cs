using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/Spawnable Box Collider")]
public class EDSpawnableBoxCollider : EDSpawnable
{
    [Header("Box Spawnable")]
    public SpawnableBoxCollider objectToSpawn;
    public Vector3 bounds;
    public Vector3 centerPercent = new Vector3(0.5f, 0.5f, 0.5f);

    [Header("Overrides")]
    public bool useAbilityRangeAsZBounds;
    public bool destroyOnFirstUpdate;
    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Spawnable objectSpawned = SpawnObject(objectToSpawn, caller, target, mousePos, abilityData);
    }
}
