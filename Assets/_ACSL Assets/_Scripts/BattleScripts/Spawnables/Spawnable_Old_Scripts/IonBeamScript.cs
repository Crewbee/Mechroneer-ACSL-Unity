//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class IonBeamScript : Spawnable, ISpawnableObject
//{
//    private LineRenderer m_IonBeam;

//    private Vector3 m_Origin;

//    private Vector3[] points = new Vector3[2];

//    public GameObject m_Sparks;
//    public GameObject m_Sparks2;

//    private bool SpawnSparks;

//    // Start is called before the first frame update
//    void Start()
//    {
//        m_IonBeam = GetComponent<LineRenderer>();
//        m_IonBeam.enabled = false;
//        SpawnSparks = true;
//        m_Sparks = Instantiate(m_Sparks, Spawnable_Target, Quaternion.identity);
//        m_Sparks2 = Instantiate(m_Sparks2, Spawnable_Target, Quaternion.identity);
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
//            m_IonBeam.SetPositions(points);

//            m_IonBeam.startWidth = 0.8f;
//            m_IonBeam.endWidth = 0.6f;


//            m_IonBeam.enabled = true;

//            if (SpawnSparks == true)
//            {
//                m_Sparks.transform.position = Spawnable_Target;
//                m_Sparks2.transform.position = Spawnable_Target;
//                m_Sparks.GetComponent<ParticleSystem>().Play();
//                m_Sparks2.GetComponent<ParticleSystem>().Play();
//                SpawnSparks = false;
//            }

//            if (m_IonBeam.enabled == true)
//            {
//                Vector4 startCol = m_IonBeam.startColor;
//                Vector4 endCol = m_IonBeam.endColor;

//                startCol.w -= Time.deltaTime * 8.5f;
//                endCol.w -= Time.deltaTime * 2.5f;

//                m_IonBeam.startColor = startCol;
//                m_IonBeam.endColor = endCol;

//                if (startCol.w <= 0.0f && endCol.w <= 0.0f)
//                {
//                    startCol.w = 1.0f;
//                    endCol.w = 1.0f;

//                    m_IonBeam.startColor = startCol;
//                    m_IonBeam.endColor = endCol;
//                    m_IonBeam.enabled = false;

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