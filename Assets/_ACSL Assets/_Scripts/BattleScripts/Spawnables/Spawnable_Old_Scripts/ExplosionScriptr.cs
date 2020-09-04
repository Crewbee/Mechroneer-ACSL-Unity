//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ExplosionScriptr : Spawnable, ISpawnableObject
//{
//    public float explosionRadiusMax;

//    public float explosionSpeed;

//    private Vector3 m_ExplosionMaxVector;

//    private Material m_ExplosionMat;

//    // Start is called before the first frame update
//    void Start()
//    {
//        m_ExplosionMat = gameObject.GetComponent<MeshRenderer>().material;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        transform.localScale = Vector3.Lerp(transform.localScale, m_ExplosionMaxVector, Time.deltaTime * explosionSpeed);

//        if (m_ExplosionMaxVector.x - transform.localScale.x <= 0.01f)
//        {
//            Destroy(gameObject);
//        }
//    }

//    public override void OnObjectSpawn()
//    {
//        transform.localScale = Vector3.zero;

//        m_ExplosionMaxVector = new Vector3(explosionRadiusMax, explosionRadiusMax, explosionRadiusMax);
//    }

//}
