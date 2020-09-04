using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/Ricochet")]
public class EDRicochet : EffectData
{
    public int numberRicochets = 3;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        if(numberRicochets > 0)
        {
            Vector3 objectDirection = abilityData.direction;
            objectDirection.Normalize();

            Vector3 ricochetDirection = Vector3.Reflect(objectDirection, Vector3.right);


            numberRicochets--;
        }
    }
}
