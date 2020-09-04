//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

/// TODO: DO DELETE
/// 
//public class MiniBombScript : Spawnable, ISpawnableObject
//{

//    public Collider bombCollider;
//    public GameObject explosionPrefab;
//    public float lifeTime = 2.0f;
//    public float explosionRadius = 20.0f;
//    private MyTimer m_BombTimer;
//    //private ObjectPooler m_ObjectPooler;

//    private void Awake()
//    {
//        GetComponent<Rigidbody>().detectCollisions = true;

//        bombCollider = GetComponent<SphereCollider>();
//        bombCollider.isTrigger = false;

//        m_BombTimer = new MyTimer();
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

//    private void OnCollisionEnter(Collision collision)
//    {
//        GameObject obj = collision.gameObject;
//        if (obj.layer == LayerMask.GetMask("Robot"))
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
//            HealthComponent targethealth = obj.GetComponent<HealthComponent>();
//            Rigidbody objBody = obj.GetComponent<Rigidbody>();
//            if (obj == null)
//                return;
//            if (Spawnable_Owner == null)
//                return;
//            if (targethealth != null && obj.transform != Spawnable_Owner.parent)
//            {
//                if (targetHealthComps.Count == 0)
//                {
//                    targetHealthComps.Add(targethealth);
//                }
//                else if (!targetHealthComps.Contains(targethealth))
//                {
//                    targetHealthComps.Add(targethealth);
//                }
//            }

//            if (objBody)
//            {
//                Vector3 dir = transform.position - obj.transform.position;
//                dir.y = 0;
//                dir = dir.normalized;
//                objBody.AddForce(-dir * 8.0f, ForceMode.Impulse);
//            }
//        }
//        foreach (HealthComponent target in targetHealthComps)
//        {
//            target.ApplyDamage(Spawnable_Damage);
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
