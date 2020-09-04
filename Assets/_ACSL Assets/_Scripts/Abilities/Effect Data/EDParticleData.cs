using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Ability/Effects/Particle")]
public class EDParticleData : EDSpawnable
{
    public bool ReverseDirection;
    public bool isLooped;
    public bool followOwner;
    public List<SpawnableParticleSystem> particles;

    public bool useOwnerRotation;


    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        foreach (SpawnableParticleSystem particle in particles)
        {
            Spawnable objectSpawned = SpawnObject(particle, caller, target, mousePos, abilityData);
            if(followOwner)
            {
                objectSpawned.transform.parent = caller.GetGameObject().transform;
            }
        }

    }

}
