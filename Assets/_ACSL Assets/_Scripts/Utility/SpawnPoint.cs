using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum SpawnPointType
    {
        SPT_POINT,
        SPT_AREA
    }

    public SpawnPointType spawnType;
    public float spawnAreaRadius = 5.0f;

    public Vector3 spawnPoint = Vector3.zero;

    private void Awake()
    {
        spawnPoint = GetSpawnPosition();
    }

    public Vector3 GetSpawnPosition()
    {
        switch(spawnType)
        {
            case SpawnPointType.SPT_POINT:
                return transform.position;

            case SpawnPointType.SPT_AREA:
                spawnPoint = transform.position + (Vector3.Scale(Random.insideUnitSphere, new Vector3(1, 0, 1)) * spawnAreaRadius);


                return spawnPoint;

                //if (Physics.CheckSphere(spawnPoint, 1f))
                //{
                //    GetSpawnPosition();
                //}
                //else
                //{
                //    return spawnPoint;
                //}

                //Vector3 worldPos = transform.position;
                //
                //spawnPoint = new Vector3(Random.Range(worldPos.x - spawnAreaRadius, worldPos.x + spawnAreaRadius), 
                //    worldPos.y, Random.Range(worldPos.z - spawnAreaRadius, worldPos.z + spawnAreaRadius));
                //

                //return Vector3.zero;

            default:
                Debug.LogWarning("SpawnPointType not set deafault to point spawn");
                return transform.position;
               

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnType == SpawnPointType.SPT_AREA)
        {
            // Draw spawn area
            {
                Gizmos.color = Color.white;

                Vector3[] points = new Vector3[32];
                for (int i = 0; i < 32; i++)
                {
                    float angle = i * Mathf.PI * 2f / 32;
                    Vector3 newPos = new Vector3(Mathf.Cos(angle) * spawnAreaRadius, 0, Mathf.Sin(angle) * spawnAreaRadius) + transform.position;
                    points[i] = newPos;
                }

                for (int i = 0; i < 32; i++)
                {
                    if (i + 1 < 32)
                        Gizmos.DrawLine(points[i], points[i + 1]);
                    else
                        Gizmos.DrawLine(points[points.Length - 1], points[0]);
                }
            }
        }
    }
}
