using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/RoombaAI")]
public class EDRoombaAI : EffectData
{ 
    public float RoombaDetectionRange;
    public float RoombaSpeed;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        if (abilityData.style == TargetingStyle.Self)
        {
            caller.ApplyEffect(new RoombaAIEffect(caller, target, mousePos, this, abilityData));
        }
    }


}
