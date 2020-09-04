using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnableBoxCollider : Spawnable
{

    override public void OnSpawned(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData, EDSpawnable baseData)
    {
        base.OnSpawned(caller, target, mousePos, abilityData, baseData);

        EDSpawnableBoxCollider data = baseData as EDSpawnableBoxCollider;
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
        Vector3 centerPercent = new Vector3(-0.5f, -0.5f, -0.5f) + data.centerPercent;
        Vector3 bounds = data.bounds;
        bounds.z = (data.useAbilityRangeAsZBounds) ? abilityData.abilityRange : bounds.z;
        collider.size = data.bounds;
        collider.center = new Vector3(data.bounds.x * -centerPercent.x, data.bounds.y * -centerPercent.y, data.bounds.z * -centerPercent.z);

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if ((m_spawnableData as EDSpawnableBoxCollider).destroyOnFirstUpdate)
            DestroyGameObject();
    }
}
