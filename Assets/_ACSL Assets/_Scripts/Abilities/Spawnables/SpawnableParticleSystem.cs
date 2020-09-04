using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableParticleSystem : Spawnable
{
    public List<ParticleSystem> particleSystems;

    public override void OnSpawned(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData, EDSpawnable baseData)
    {
        base.OnSpawned(caller, target, mousePos, abilityData, baseData);

        EDParticleData data = baseData as EDParticleData;

        foreach(ParticleSystem system in particleSystems)
        {
            if(data.useOwnerRotation)
            {
                Vector3 rot = transform.rotation.eulerAngles + caller.GetGameObject().transform.forward;
                system.transform.rotation = Quaternion.Euler(rot);

                var ps = system.main;
                ps.startRotationY = rot.y;

            }
            system.transform.position = caller.GetGameObject().transform.position;
            system.Play();
        }

    }
}
