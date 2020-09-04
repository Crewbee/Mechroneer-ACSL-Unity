using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/Dash")]
public class EDDash : EffectData
{
    public float dashPower;
    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Rigidbody body = caller.GetGameObject().GetComponent<Rigidbody>();
        if (!body)
        {
            Debug.LogError(caller.GetGameObject().name + " is missing a RigidBody, dash cannot work without one");
            return;
        }

        caller.ApplyEffect(new DashEffect(caller, this, abilityData));
        //Vector3 direction = Vector3.forward;//(mousePos - caller.GetGameObject().transform.position).normalized;
        //body.AddForce(direction * dashPower, ForceMode.Impulse);

    }
    
}
