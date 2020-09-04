//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

/// TODO: DO NOT DELETE
//public class ClusterBombScript : Spawnable, ISpawnableObject
//{

//    public Collider bombCollider;
//    public Collider explosionCollider;
//    public GameObject explosionPrefab;
//    public float lifeTime = 3.0f;
//    public float explosionRadius = 15.0f;
//    public int SpawnMiniBombCount = 12;

//    private MyTimer m_BombTimer;

//Delete this script
//    //private ObjectPooler m_ObjectPooler;


//    private void Awake()
//    {
//        GetComponent<Rigidbody>().detectCollisions = true;

//        bombCollider = GetComponent<CapsuleCollider>();
//        bombCollider.isTrigger = false;

//        explosionCollider = GetComponent<SphereCollider>();
//        explosionCollider.isTrigger = true;

//        m_BombTimer = new MyTimer();

//        ExplosionScriptr explosionBehavior = explosionPrefab.GetComponent<ExplosionScriptr>();
//    }

//    private void Start()
//    {
//        //m_ObjectPooler = ObjectPooler.Instance;
//    }

//    public void SetOwner(Transform obj)
//    {
//        Spawnable_Owner = obj;
//    }

//    private void LateUpdate()
//    {
//        m_BombTimer.Update();
//        if (!m_BombTimer.active)
//        {
//            Explode();
//            ReturnToPool();
//        }
//    }

//    private void Explode()
//    {
//        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

//        ExplosionScriptr explosionBehavior = explosion.GetComponent<ExplosionScriptr>();
//        explosionBehavior.explosionRadiusMax = explosionRadius;
//        explosionBehavior.explosionSpeed = 5.0f;
//        explosionBehavior.OnObjectSpawn();

//        Collider[] explosiontargets = Physics.OverlapSphere(transform.position, explosionRadius);

//        List<HealthComponent> targetHealthComps = new List<HealthComponent>();

//        foreach (Collider collider in explosiontargets)
//        {
//            GameObject obj = collider.gameObject;
//            if (!obj)
//                return;
//            HealthComponent targethealth = obj.GetComponent<HealthComponent>();
//            Rigidbody objBody = obj.GetComponent<Rigidbody>();
//            if (obj.transform != null)
//            {
//                if (targethealth && obj.transform != Spawnable_Owner.parent)
//                {
//                    if (targetHealthComps.Count == 0)
//                    {
//                        targetHealthComps.Add(targethealth);
//                    }
//                    else if (!targetHealthComps.Contains(targethealth))
//                    {
//                        targetHealthComps.Add(targethealth);
//                    }
//                }
//            }
//            if (objBody)
//            {
//                Vector3 dir = transform.position - obj.transform.position;
//                dir.y = 0;
//                dir = dir.normalized;

//                objBody.AddForce(-dir * 50.0f, ForceMode.Impulse);
//            }
//        }

//        foreach (HealthComponent target in targetHealthComps)
//        {
//            target.ApplyDamage(Spawnable_Damage);
//        }



//        for (int i = 0; i < SpawnMiniBombCount; i++)
//        {
//            Vector3 direction = Quaternion.Euler(0, (360.0f / SpawnMiniBombCount) * i, 0) * Vector3.forward + (Vector3.up * (Random.Range(4.0f, 10.0f)));
//            Vector3 position = transform.position + direction * 1.0f;

//            int Damage = (int)(Spawnable_Damage / SpawnMiniBombCount);

//            direction.Normalize();

//            //m_ObjectPooler.SpawnFromPool(SpawnableTypes.ST_MINIBOMB, position, Quaternion.identity, direction * 8.0f, Damage, Spawnable_Owner);

//        }
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        GameObject obj = collision.gameObject;
//        if (obj.layer == LayerMask.GetMask("Robot"))
//        {
//            Explode();
//            ReturnToPool();
//        }

//    }

//    private void ReturnToPool()
//    {
//        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
//        gameObject.SetActive(false);
//    }

//    // Is called on first frame after spawn
//    public override void OnObjectSpawn()
//    {
//        GetComponent<Rigidbody>().AddForce(Spawnable_Direction, ForceMode.Impulse);
//        m_BombTimer.StartTimer(lifeTime);

//    }

//    public void OnTriggerEnter(Collider col)
//    {

//    }
//    public void OnTriggerExit(Collider col)
//    {

//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawWireSphere(transform.position, 4.0f);
//    }
//}
