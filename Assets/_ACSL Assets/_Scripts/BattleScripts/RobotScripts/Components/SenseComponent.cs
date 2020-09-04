using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class SenseComponent : MonoBehaviour
{
    [Header("Vision Settings")]
    public LayerMask targetLayerMask;
    public LayerMask obfuscationLayerMask;
    [Space(5)]
    [Min(0)]
    public float visionDelay = 0.2f;
    public float visionRadius = 10f;
    [Range(0, 360)]
    public float visionAngle = 90f;

    //[HideInInspector]
    [Tooltip("Descending order by distance ignoring self.")]
    public List<Transform> visibleTargets = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // Start finding targets with a delay
        StartCoroutine(FindTargetsDelayed(visionDelay));
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator FindTargetsDelayed(float delay)
    {
        while (true)
        {
            // Wait before calling FindTargets
            yield return new WaitForSeconds(delay);
            FindTargets();
        }
    }

    void FindTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInSightRadius = Physics.OverlapSphere(transform.position, visionRadius, targetLayerMask); // Find all possible targets

        foreach (var target in targetsInSightRadius)
        {
            // If the direction from "this" to the target is within the angle range
            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) <= visionAngle / 2)
            {
                // If the distance from "this" to the target is withing the detection radius
                float distToTarget = Vector3.Distance(target.transform.position, transform.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obfuscationLayerMask)) // Raycast to check if target is not obscured
                {
                    if (target.transform == this.transform)
                        continue;
                    if (visibleTargets.Contains(target.transform))
                        continue;
                    visibleTargets.Add(target.transform);
                }
            }
        }

        // Sort Descending by distance
        visibleTargets = visibleTargets.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }

    private void OnDrawGizmosSelected()
    {
        // Draw sight radius
        {
            Gizmos.color = Color.black;

            Vector3[] points = new Vector3[32];
            for (int i = 0; i < 32; i++)
            {
                float angle = i * Mathf.PI * 2f / 32;
                Vector3 newPos = new Vector3(Mathf.Cos(angle) * visionRadius, 0, Mathf.Sin(angle) * visionRadius) + transform.position;
                points[i] = newPos;
            }

            for (int i = 0; i < 32; i++)
            {
                if (i + 1 < 32)
                    Gizmos.DrawLine(points[i], points[i + 1]);
                else
                    Gizmos.DrawLine(points[points.Length - 1], points[0]);
            }

            Gizmos.DrawLine(transform.position, transform.position + (transform.forward.normalized * visionRadius));
        }

        // Draw view angle
        {
            Gizmos.color = Color.green;

            for (int i = -1; i <= 1; i++)
            {
                if (i == 0)
                    continue;

                Vector3 rotatedLine = Quaternion.AngleAxis((visionAngle / 2) * i, transform.up) * transform.forward * visionRadius;
                Gizmos.DrawLine(transform.position, transform.position + rotatedLine);
                Gizmos.DrawSphere(transform.position + rotatedLine, 0.05f);
            }

            for (int i = -1; i <= 1; i++)
            {
                if (i == 0)
                    continue;

                Vector3 rotatedLine = Quaternion.AngleAxis((visionAngle / 2) * i, transform.right * i + transform.up) * transform.forward * visionRadius;
                Gizmos.DrawLine(transform.position, transform.position + rotatedLine);
                Gizmos.DrawSphere(transform.position + rotatedLine, 0.05f);
            }

            for (int i = -1; i <= 1; i++)
            {
                if (i == 0)
                    continue;

                Vector3 rotatedLine = Quaternion.AngleAxis((visionAngle / 2) * i, transform.right * -i + transform.up) * transform.forward * visionRadius;
                Gizmos.DrawLine(transform.position, transform.position + rotatedLine);
                Gizmos.DrawSphere(transform.position + rotatedLine, 0.05f);
            }

            for (int i = -1; i <= 1; i++)
            {
                if (i == 0)
                    continue;

                Vector3 rotatedLine = Quaternion.AngleAxis((visionAngle / 2) * i, transform.right) * transform.forward * visionRadius;
                Gizmos.DrawLine(transform.position, transform.position + rotatedLine);
                Gizmos.DrawSphere(transform.position + rotatedLine, 0.05f);
            }
        }

        // Draw vision indicators
        {
            foreach (var target in visibleTargets)
            {
                //Handles.Label(target.transform.position, "VISIBLE");
            }
        }
    }
}
