//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BulletScript : Spawnable, ISpawnableObject
//{
//    public Collider bulletCollider;

//    private float lifeTime = 2.0f;

//    public GameObject m_Sparks;

//    private bool SpawnSparks;

//    private void Awake()
//    {
//        GetComponent<Rigidbody>().detectCollisions = true;

//        bulletCollider = GetComponent<BoxCollider>();
//        bulletCollider.isTrigger = true;
//        SpawnSparks = true;
//    }

//    public void SetOwner(Transform obj)
//    {
//        Spawnable_Owner = obj;
//    }

//    void Update()
//    {
//        lifeTime -= Time.deltaTime;

//        if(lifeTime <= 0.0f)
//        {
//            gameObject.SetActive(false);
//        }
//    }

//    private void LateUpdate()
//    {
//        transform.Translate(Spawnable_Direction * Time.deltaTime);
//        GetComponent<Rigidbody>().MovePosition(transform.position);
//    }


//    #region Unused RigidBody Collision Code
//    private void OnCollisionEnter(Collision collision)
//    {
//        gameObject.SetActive(false);
//    }

//    private void Impact()
//    {
//        gameObject.SetActive(false);
//    }
//    #endregion


//    private void OnTriggerEnter(Collider other)
//    {
//        GameObject target = other.gameObject;
//        this.gameObject.SetActive(false);

//        if(SpawnSparks)
//        {
//            m_Sparks.transform.position = other.gameObject.transform.position;
//            m_Sparks.GetComponent<ParticleSystem>().Play();
//            SpawnSparks = false;
//            Debug.Log("HitsBulletSparks");
//        }

//        if (target.tag == "Robot" && target != Spawnable_Owner)
//        {
//            GameObject robot = target;

//            if (robot.GetComponent<HealthComponent>() != null)
//            {
//                robot.GetComponent<HealthComponent>().ApplyDamage(Spawnable_Damage);
//            }

//        }

//    }

//    #region OnObjectSpawn(Unused)
//    // Start is called before the first frame update
//    public override void OnObjectSpawn()
//    {
//        lifeTime = 2.0f;
//        GetComponent<Rigidbody>().detectCollisions = true;
//        bulletCollider.isTrigger = true;
//        SpawnSparks = true;
//    }
//    #endregion
//}
