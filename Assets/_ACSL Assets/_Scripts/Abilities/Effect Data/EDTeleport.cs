using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Ability/Effects/Teleport")]
public class EDTeleport : EffectData
{
    public Transform hitObject;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        RaycastHit hit;

        Physics.Raycast(mousePos + new Vector3(0, 10, 0), Vector3.down, out hit);

        if(hit.transform != null)
        {
            hitObject = hit.transform;

            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(hit.point, out navMeshHit, 10.0f, NavMesh.AllAreas);

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                if (abilityData.inputRange > abilityData.abilityRange)
                {
                    //caller.GetGameObject().transform.position += abilityData.direction * abilityData.abilityRange;
                    caller.GetGameObject().GetComponent<NavMeshAgent>().Warp(navMeshHit.position);
                }
                else
                {
                    //caller.GetGameObject().transform.position += abilityData.direction * abilityData.inputRange;
                    caller.GetGameObject().GetComponent<NavMeshAgent>().Warp(navMeshHit.position);
                }
            }
        }

    }
}
