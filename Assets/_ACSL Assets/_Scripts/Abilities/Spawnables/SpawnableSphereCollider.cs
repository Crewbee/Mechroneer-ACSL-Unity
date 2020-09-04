using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SpawnableSphereCollider : Spawnable
{
    override public void OnSpawned(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData, EDSpawnable baseData)
    {
        base.OnSpawned(caller, target, mousePos, abilityData, baseData);

        EDSpawnableSphereCollider data = baseData as EDSpawnableSphereCollider;
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.radius = data.hitRadius;
        collider.isTrigger = true;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, data.hitRadius, Vector3.forward, 0);
        foreach (RaycastHit hit in hits)
        {
            IEffectUser user = hit.collider.GetComponent<IEffectUser>();
            if (user == null)
                continue;
            if ((AbilityData.TargetMaskEqual(abilityData.mask, TargetMask.Self) == false) && user == caller.GetOwner())
                continue;
            //Debug.Log(hit.collider.name);
            m_stayTargets.Add(user);
            m_stayTargetsGameObject.Add(user.GetGameObject());
        }

        TriggerStay();
    }
    
}
