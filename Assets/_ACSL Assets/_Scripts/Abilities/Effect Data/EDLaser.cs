using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/Laser")]
public class EDLaser : EDSpawnableLineRenderer
{
    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Spawnable spawnedObject = SpawnObject(objectToSpawn, caller, target, mousePos, abilityData);

        Vector3 initialPos = (useCustomSpawnPoint) ? caller.GetCustomSpawnPoint(abilityData.customSpawnPointID).position : caller.GetGameObject().transform.position;
        spawnedObject.transform.position = initialPos;

        spawnedObject.ApplyEffect(new LaserEffect(spawnedObject, mousePos, target, abilityData, this));
    }


}
