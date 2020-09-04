using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Ability/Effects/KnockBack")]
public class EDKnockback : EffectData
{
    public float forcePower;
    public bool reverse;
    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Rigidbody body = target.GetGameObject().GetComponent<Rigidbody>();
        if (body == null)
            return;
        Vector3 forceDirection = (target.GetGameObject().transform.position - caller.GetGameObject().transform.position);
        forceDirection.y = 0;
        forceDirection.Normalize();
        if (reverse)
            forceDirection *= -1;

        target.GetGameObject().GetComponent<Robot>().MoveToPoint(caller.GetGameObject().transform.position);
        body.AddForce(forceDirection * forcePower, ForceMode.Impulse);
    }
}
