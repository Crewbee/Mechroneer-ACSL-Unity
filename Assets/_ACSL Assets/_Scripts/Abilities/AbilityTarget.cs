using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTarget
{
    public static Vector3 GetMousePosition()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = LayerMask.NameToLayer("Robot");
        layerMask = int.MaxValue ^ layerMask;
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 100))
        {
            Vector3 targetHit = hitInfo.point;
            targetHit.y = 0;
            return targetHit;
        }


        return Vector3.zero;
    }

    public static IEffectUser GetTargetRaycast()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("Robot");
        //layerMask = layerMask ^ int.MaxValue;
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 100, layerMask))
        {
            //Debug.Log("Target hit");
            GameObject objectHit = hitInfo.collider.gameObject;
            IEffectUser holder = objectHit.GetComponent<IEffectUser>();
            if (holder == null)
                holder = objectHit.GetComponentInParent<IEffectUser>();
            return holder;
        }


        return null;
    }
}
