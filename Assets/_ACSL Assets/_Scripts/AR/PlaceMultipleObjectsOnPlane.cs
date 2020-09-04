//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;


////TODO: Remake this
//[RequireComponent(typeof(ARRaycastManager))]
//public class PlaceMultipleObjectsOnPlane : MonoBehaviour
//{
//    //[SerializeField]
//    //[Tooltip("Instantiates this prefab on a plane at the touch location.")]
//    GameObject m_PlacedPrefab;

//    public BattleSceneManager m_BattleSceneManager;
//    float ScaleValue;
//    bool m_placed;

//    /// The prefab to instantiate on touch.
//    public GameObject placedPrefab
//    {
//        get { return m_PlacedPrefab; }
//        set { m_PlacedPrefab = value; }
//    }

//    /// The object instantiated as a result of a successful raycast intersection with a plane.
//    public GameObject spawnedObject { get; private set; }

//    /// Invoked whenever an object is placed in on a plane.
//    public static event Action onPlacedObject;

//    ARRaycastManager m_RaycastManager;
//    ARSessionOrigin m_SessionOrigin;

//    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

//    void Awake()
//    {
//        spawnedObject = GameObject.Find("_Level");
        
//        if (m_BattleSceneManager == null)
//        {
//            m_BattleSceneManager = FindObjectOfType<BattleSceneManager>();
//            Debug.LogError("BattleSceneManager NULL! {0}" + m_BattleSceneManager);
//        }


//        spawnedObject.SetActive(false);

//        m_RaycastManager = GetComponent<ARRaycastManager>();
//        m_SessionOrigin = GetComponent<ARSessionOrigin>();
//        ScaleValue = 0.01f;
//        m_placed = false;
//    }

//    public void UpScale()
//    {
//        if (m_placed == true)
//        {
//            if (ScaleValue < 100.0f)
//            {
//                ScaleValue += 1.0f;
//                m_SessionOrigin.transform.localScale = new Vector3(ScaleValue, ScaleValue, ScaleValue);
//                //spawnedObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * ScaleValue;
//                //NavigationManager._instance.mainPath.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * ScaleValue;
//            }
//        }

//    }
//    public void DownScale()
//    {
//        if (m_placed == true)
//        {
//            if (ScaleValue > 1.0f)
//            {
//                ScaleValue -= 1.0f;
//            }

//            m_SessionOrigin.transform.localScale = new Vector3(ScaleValue, ScaleValue, ScaleValue);
//            //spawnedObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * ScaleValue;
//            //NavigationManager._instance.mainPath.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * ScaleValue;
//        }
//    }

//    public void PlaceObject()
//    {
//        DebugText.AddMessage("Attempt to place object", 30);

//            if(m_placed == false)
//            { 
//            DebugText.AddMessage("Spawning", 30);

//            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
//                if (m_RaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon))
//                {
//                    Pose hitPose = s_Hits[0].pose;
//                    spawnedObject.SetActive(true);
//                    spawnedObject.transform.position = hitPose.position;
//                    if (m_SessionOrigin)
//                    {
//                        ScaleValue = 50.0f;
//                        m_SessionOrigin.MakeContentAppearAt(spawnedObject.transform, hitPose.position, Quaternion.identity);
//                        m_SessionOrigin.transform.localScale = new Vector3(ScaleValue, ScaleValue, ScaleValue);
//                        m_SessionOrigin.GetComponent<ARPointCloudManager>().enabled = false;
//                        m_SessionOrigin.GetComponent<ARPlaneManager>().enabled = false;
//                    }

//                    ARPlaneManager planeManager = GetComponent<ARPlaneManager>();
//                    foreach (var plane in planeManager.trackables)
//                    {
//                        plane.gameObject.SetActive(false);
//                    }
//                    ARPointCloudManager pointCloudManager = GetComponent<ARPointCloudManager>();
//                    foreach (var point in pointCloudManager.trackables)
//                    {
//                        point.gameObject.SetActive(false);
//                    }
//                    GameObject uiManger = GameObject.Find("UIManager"); 
//                    if (uiManger)
//                    {
                    
//                        //UIManager.Instance.Pop();
//                        //uiManger.GetComponent<UIManagerOld>().Push(1);
//                        DebugText.AddMessage("Pushing scene 1", 10.0f);
//                        //UIManager.Instance.Push(2);
//                    }
//                    else
//                    {
//                        DebugText.AddMessage("Unable to find UImanager instance", 10.0f);
//                    }
//                    m_placed = true;
//                    if (m_BattleSceneManager)
//                    {
//                        DebugText.AddMessage("Arena placed", 10.0f);
//                        m_BattleSceneManager.ServerArenaPlaced();
//                    }

//                    DebugText.AddMessage("Instantiating", 30);
//                    if (onPlacedObject != null)
//                    {
//                        onPlacedObject();
//                    }
//                }
//            }
//    }
    

//    void Update()
//    {

//    }
//}
