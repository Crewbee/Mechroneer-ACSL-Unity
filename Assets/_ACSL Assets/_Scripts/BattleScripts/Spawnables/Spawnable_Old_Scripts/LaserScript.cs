//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

/// TODO: DO NOT DELETE
//public class LaserScript : Spawnable, ISpawnableObject
//{
//    private LineRenderer m_Laser;

//    private Vector3 m_Origin;

//    private Vector3[] points = new Vector3[2];

//    public GameObject m_Sparks;

//    private bool SpawnSparks;

//    // Start is called before the first frame update
//    void Start()
//    {
//        m_Laser = GetComponent<LineRenderer>();
//        m_Laser.enabled = false;
//        SpawnSparks = true;
//        m_Sparks = Instantiate(m_Sparks, Spawnable_Target, Quaternion.identity);
//    }
//    public void SetOwner(Transform obj)
//    {
//        Spawnable_Owner = obj;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (this.gameObject.activeSelf == true)
//        {
//            m_Laser.SetPositions(points);

//            m_Laser.startWidth = 0.2f;
//            m_Laser.endWidth = 0.2f;

//            m_Laser.enabled = true;

//            if (SpawnSparks == true)
//            {
//                m_Sparks.transform.position = Spawnable_Target;
//                m_Sparks.GetComponent<ParticleSystem>().Play();
//                SpawnSparks = false;
//            }

//            if (m_Laser.enabled == true)
//            {
//                Vector4 startCol = m_Laser.startColor;
//                Vector4 endCol = m_Laser.endColor;

//                startCol.w -= Time.deltaTime * 8.5f;
//                endCol.w -= Time.deltaTime * 2.5f;

//                m_Laser.startColor = startCol;
//                m_Laser.endColor = endCol;

//                if (startCol.w <= 0.0f && endCol.w <= 0.0f)
//                {
//                    startCol.w = 1.0f;
//                    endCol.w = 1.0f;

//                    m_Laser.startColor = startCol;
//                    m_Laser.endColor = endCol;

//                    m_Laser.enabled = false;

//                    this.gameObject.SetActive(false);

//                }

//            }
//        }
//    }

//    public override void OnObjectSpawn()
//    {
//        m_Origin = transform.position;

//        points[0] = m_Origin;
//        points[1] = Spawnable_Target;

//        SpawnSparks = true;

//    }
//}

