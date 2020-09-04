using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBombSpawner : MonoBehaviour
{
    //TODO: Reimplement when ability is replaced
    //ObjectPooler objectPooler;

    Transform m_Owner;

    private void Start()
    {
        //objectPooler = ObjectPooler.Instance;

        m_Owner = transform.parent;

    }

    public void SpawnClusterBomb(Vector3 position, Vector3 direction, float damage, float speed)
    {
        
        //objectPooler.SpawnFromPool(SpawnableTypes.ST_CLUSTERBOMB, position, Quaternion.identity, direction * speed, damage, m_Owner);

    }
}
