//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

////TODO: DELETE THIS

//public enum SpawnableTypes
//{
//    ST_BULLET = 0,
//    ST_DEBRIS,
//    ST_LASER,
//    ST_CLUSTERBOMB,
//    ST_IONBEAM,
//    ST_MINIBOMB,
//    ST_MISSILE,

//    ST_PROTOTYPE_SPAWNABLE,
//    ST_PROTOTYPE_PROJECTILE,
//};

//public class ObjectPooler : MonoBehaviour
//{
//    //    public string[] ParentTags =
//    //    {
//    //        "BULLET",
//    //        "DEBRIS",
//    //        "LASER",
//    //        "CLUSTERBOMB",
//    //        "IONBEAM",
//    //        "MINIBOMB",
//    //        "MISSILE"
//    //    };

//    //    [System.Serializable]
//    //    public class Pool
//    //    {
//    //        public SpawnableTypes tag;
//    //        public List<GameObject> prefab;
//    //        public int size;
//    //    }

//    //    #region Singleton

//    //    public static ObjectPooler Instance;

//    //    private void Awake()
//    //    {
//    //        if (Instance)
//    //        {
//    //            Destroy(this.gameObject);
//    //            return;
//    //        }
//    //        Instance = this;

//    //    }
//    //    #endregion

//    //    public List<Pool> pools;
//    //    public Dictionary<SpawnableTypes, Queue<GameObject>> poolDictionary;

//    //    // Start is called before the first frame update
//    //    void Start()
//    //    {
//    //        poolDictionary = new Dictionary<SpawnableTypes, Queue<GameObject>>();


//    //        foreach (Pool pool in pools)
//    //        {
//    //            Queue<GameObject> objectPool = new Queue<GameObject>();
//    //            GameObject poolParent = Instantiate(new GameObject());
//    //            poolParent.transform.SetParent(this.transform);
//    //            poolParent.name = ParentTags[(int)pool.tag];

//    //            for (int i = 0; i < pool.size; i++)
//    //            {
//    //                int RandIndex = 0;
//    //                if (pool.prefab.Count == 1)
//    //                {
//    //                    GameObject obj = Instantiate(pool.prefab[0]);
//    //                    obj.transform.SetParent(poolParent.transform);
//    //                    obj.SetActive(false);
//    //                    obj.transform.name = ParentTags[(int)pool.tag];
//    //                    objectPool.Enqueue(obj);
//    //                }
//    //                else if (pool.prefab.Count > 1)
//    //                {
//    //                    if(i % 2 == 0)
//    //                    {
//    //                        RandIndex = 0;
//    //                    }
//    //                    else
//    //                    {
//    //                        RandIndex = i % pool.prefab.Count;
//    //                    }
//    //                    GameObject obj = Instantiate(pool.prefab[RandIndex]);
//    //                    obj.transform.SetParent(poolParent.transform);
//    //                    obj.SetActive(false);
//    //                    obj.transform.name = ParentTags[(int)pool.tag];
//    //                    objectPool.Enqueue(obj);
//    //                }
//    //            }

//    //            poolDictionary.Add(pool.tag, objectPool);
//    //        }
//    //    }

//    //    public GameObject SpawnFromPool(SpawnableTypes tag, Vector3 position, Quaternion rotation)
//    //    {
//    //        if (!poolDictionary.ContainsKey(tag))
//    //        {
//    //            Debug.LogWarning("Pool with tag" + tag + "does not exist");
//    //            return null;
//    //        }

//    //        GameObject ObjectToSpawn = poolDictionary[tag].Dequeue();

//    //        ObjectToSpawn.SetActive(true);
//    //        ObjectToSpawn.transform.position = position;
//    //        ObjectToSpawn.transform.rotation = rotation;

//    //        ISpawnableObject poolObj = ObjectToSpawn.GetComponent<ISpawnableObject>();

//    //        if (poolObj != null)
//    //        {
//    //            poolObj.OnObjectSpawn();
//    //        }


//    //        poolDictionary[tag].Enqueue(ObjectToSpawn);

//    //        return ObjectToSpawn;

//    //    }

//    //    public GameObject SpawnFromPool(SpawnableTypes tag, Vector3 position, Quaternion rotation, Vector3 velocity, float damage, Transform Owner)
//    //    {
//    //        if (!poolDictionary.ContainsKey(tag))
//    //        {
//    //            Debug.LogWarning("Pool with tag" + tag + "does not exist");
//    //            return null;
//    //        }
//    //        if (tag != SpawnableTypes.ST_BULLET && tag != SpawnableTypes.ST_CLUSTERBOMB && tag != SpawnableTypes.ST_MINIBOMB)
//    //        {
//    //            Debug.LogWarning("Cannot spawn item from projectile pool");
//    //            return null;
//    //        }

//    //        GameObject ObjectToSpawn = poolDictionary[tag].Dequeue();

//    //        ObjectToSpawn.SetActive(true);
//    //        ObjectToSpawn.transform.position = position;
//    //        ObjectToSpawn.transform.rotation = rotation;

//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_Direction = velocity;
//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_Damage = damage;
//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_Owner = Owner;

//    //        ISpawnableObject poolObj = ObjectToSpawn.GetComponent<ISpawnableObject>();

//    //        if (poolObj != null)
//    //        {
//    //            poolObj.OnObjectSpawn();
//    //        }


//    //        poolDictionary[tag].Enqueue(ObjectToSpawn);

//    //        return ObjectToSpawn;

//    //    }

//    //    public GameObject SpawnFromPool(SpawnableTypes tag, Vector3 position, Quaternion rotation, Vector3 velocity, float damage, Transform Owner, Transform Homing_Target)
//    //    {
//    //        if (!poolDictionary.ContainsKey(tag))
//    //        {
//    //            Debug.LogWarning("Pool with tag" + tag + "does not exist");
//    //            return null;
//    //        }
//    //        if (tag != SpawnableTypes.ST_BULLET && tag != SpawnableTypes.ST_CLUSTERBOMB)
//    //        {
//    //            Debug.LogWarning("Cannot spawn item from projectile pool");
//    //            return null;
//    //        }

//    //        GameObject ObjectToSpawn = poolDictionary[tag].Dequeue();

//    //        ObjectToSpawn.SetActive(true);
//    //        ObjectToSpawn.transform.position = position;
//    //        ObjectToSpawn.transform.rotation = rotation;

//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_Direction = velocity;
//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_Damage = damage;
//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_Owner = Owner;
//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_HomingTarget = Homing_Target;


//    //        poolDictionary[tag].Enqueue(ObjectToSpawn);

//    //        ISpawnableObject poolObj = ObjectToSpawn.GetComponent<ISpawnableObject>();
//    //        if (poolObj != null)
//    //        {
//    //            poolObj.OnObjectSpawn();
//    //        }

//    //        return ObjectToSpawn;

//    //    }
//    //    public GameObject SpawnLaserFromPool(SpawnableTypes tag, Vector3 target, Vector3 position, float damage, Transform Owner)
//    //    {
//    //        if (!poolDictionary.ContainsKey(tag))
//    //        {
//    //            Debug.LogWarning("Pool with tag" + tag + "does not exist");
//    //            return null;
//    //        }

//    //        if (tag != SpawnableTypes.ST_LASER && tag != SpawnableTypes.ST_IONBEAM)
//    //        {
//    //            Debug.LogWarning("Cannot spawn item from Laser Pool");
//    //            return null;
//    //        }

//    //        GameObject ObjectToSpawn = poolDictionary[tag].Dequeue();

//    //        ObjectToSpawn.SetActive(true);
//    //        ObjectToSpawn.transform.position = position;
//    //        ObjectToSpawn.transform.rotation = Quaternion.identity;

//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_Target = target;
//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_Damage = damage;
//    //        ObjectToSpawn.GetComponent<Spawnable>().Spawnable_Owner = Owner;

//    //        ISpawnableObject poolObj = ObjectToSpawn.GetComponent<ISpawnableObject>();

//    //        if (poolObj != null)
//    //        {
//    //            poolObj.OnObjectSpawn();
//    //        }

//    //        poolDictionary[tag].Enqueue(ObjectToSpawn);

//    //        return ObjectToSpawn;
//    //    }

//    //    public GameObject SpawnPrototypeEffect(SpawnableTypes tag, GameObject owner, GameObject caller, GameObject target, Vector3 spawnPos, Quaternion spawnRot, Vector3 velocity, ACSL.Prototype.TargetingStyle targetingStyle)
//    //    {
//    //        if (!poolDictionary.ContainsKey(tag))
//    //        {
//    //            Debug.LogWarning("Pool with tag" + tag + "does not exist");
//    //            return null;
//    //        }
//    //        GameObject ObjectToSpawn = poolDictionary[tag].Dequeue();

//    //        poolDictionary[tag].Enqueue(ObjectToSpawn);

//    //        return ObjectToSpawn;
//    //    }
//}
