using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SpawnableLineRenderer : Spawnable
{
    override public void OnSpawned(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData, EDSpawnable baseData)
    {
        base.OnSpawned(caller, target, mousePos, abilityData, baseData);

        EDSpawnableLineRenderer data = baseData as EDSpawnableLineRenderer;
    }
}
