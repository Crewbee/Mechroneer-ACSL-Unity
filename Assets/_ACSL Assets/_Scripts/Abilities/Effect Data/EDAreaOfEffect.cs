using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Effects/AreaOfEffect")]

public class EDAreaOfEffect : EffectData
{
    public float effectRadius;
    public List<EffectData> onTargetHit;

    [Range(0.0f, 360.0f)]
    public float sweepAngle;

    public override void ActivateEffect(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        Vector3 origin;
        switch (abilityData.style)
        {
        case TargetingStyle.Self:
            origin = caller.GetCustomSpawnPoint(abilityData.customSpawnPointID).position;
            break;
        case TargetingStyle.Targeted:
            origin = target.GetMainTarget().transform.position;
            break;
        case TargetingStyle.Skillshot:
            origin = mousePos;
            break;
        default:
            origin = caller.GetCustomSpawnPoint(abilityData.customSpawnPointID).position;
            break;
        }

        RaycastHit[] raycastHits = Physics.SphereCastAll(origin, effectRadius, Vector3.forward);
        if (sweepAngle == 360 || sweepAngle == 0)
        {
            foreach (RaycastHit hit in raycastHits)
            {
                IEffectUser holder = hit.collider.GetComponent<IEffectUser>();
                if (!TargetValid(caller, holder, abilityData))
                    continue;

                ApplyEffects(caller, holder, mousePos, abilityData);
            }
        }
        else
        {
            Quaternion referenceRotation = Quaternion.LookRotation(abilityData.direction);
            float spawnDegreesInRad = sweepAngle / 180 * 3.14f;
            float currentAngleInRad = -spawnDegreesInRad / 2;

            Vector3 direction = new Vector3(Mathf.Sin(currentAngleInRad), 0, Mathf.Cos(currentAngleInRad));
            Quaternion startingRotation = referenceRotation * Quaternion.LookRotation(direction);
            float minAngle = startingRotation.eulerAngles.y;
            float maxAngle = minAngle + sweepAngle;
            
            minAngle = NormalizeAngleDegrees(minAngle);
            maxAngle = NormalizeAngleDegrees(maxAngle);

            if (minAngle > maxAngle)
                minAngle -= 360;

            foreach (RaycastHit hit in raycastHits)
            {
                IEffectUser holder = hit.collider.GetComponent<IEffectUser>();
                if (!TargetValid(caller, holder, abilityData))
                    continue;

                Vector3 hitDirection = (hit.rigidbody.gameObject.transform.position - origin);
                hitDirection.y = 0;
                hitDirection.Normalize();
                referenceRotation = Quaternion.LookRotation(hitDirection);
                float hitAngle = referenceRotation.eulerAngles.y;

                hitAngle = NormalizeAngleDegrees(hitAngle);
                float hitAngle2 = hitAngle - 360;
                

                if (hitAngle >= minAngle && hitAngle <= maxAngle)
                {
                    ApplyEffects(caller, holder, mousePos, abilityData);
                }
                else if (hitAngle2 >= minAngle && hitAngle2 <= maxAngle)
                {
                    ApplyEffects(caller, holder, mousePos, abilityData);
                }
            }
        }

    }

    private bool TargetValid(IEffectUser caller, IEffectUser target, SomethingAbility abilityData)
    {
        if (target == null)
        {
            return false;
        }

        if (AbilityData.TargetMaskEqual(abilityData.mask, TargetMask.Self) == false)
        {
            if (target == caller.GetOwner())
                return false;
        }

        if (target.GetGameObject().tag == "Spawnable")
            return false;

        return true;
    }

    private void ApplyEffects(IEffectUser caller, IEffectUser target, Vector3 mousePos, SomethingAbility abilityData)
    {
        foreach (EffectData effect in onTargetHit)
        {
            effect.ActivateEffect(caller, target, mousePos, abilityData);
        }
    }

    private float NormalizeAngleRad(float angle)
    {
        float twoPi = 2 * Mathf.PI;
        float normalizedAngle = angle % twoPi;
        return (normalizedAngle < 0) ? normalizedAngle + twoPi : normalizedAngle;
    }

    private float NormalizeAngleDegrees(float angle)
    {
        float normalizedAngle = angle % 360;
        return (normalizedAngle < 0) ? normalizedAngle + 360 : normalizedAngle;
    }
}
