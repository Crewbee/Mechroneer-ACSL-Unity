using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARCore;
//using UnityEngine.XR.ARKit;

/// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
/// and overlays some information as well as the source Texture2D on top of the
/// detected image.
/// </summary>
//[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoManager : MonoBehaviour
{
    //TODO: What does battle scene manager do
    [SerializeField]
    [Tooltip("The camera to set on the world space UI canvas for each instantiated image info.")]
    Camera m_WorldSpaceCanvasCamera;
    public GameObject objectToSpawnPrefab;
    public GameObject objectSpawned;
    public bool b_objectSpawned = false;
    //public Text hdn;
    //public BattleSceneManager m_BattleSceneManager;
    ARSessionOrigin m_SessionOrigin;
    float ScaleValue;
    /// <summary>
    /// The prefab has a world space UI canvas,
    /// which requires a camera to function properly.
    /// </summary>
    public Camera worldSpaceCanvasCamera
    {
        get { return m_WorldSpaceCanvasCamera; }
        set { m_WorldSpaceCanvasCamera = value; }
    }

    [SerializeField]
    [Tooltip("If an image is detected but no source texture can be found, this texture is used instead.")]
    Texture2D m_DefaultTexture;

    /// <summary>
    /// If an image is detected but no source texture can be found,
    /// this texture is used instead.
    /// </summary>
    public Texture2D defaultTexture
    {
        get { return m_DefaultTexture; }
        set { m_DefaultTexture = value; }
    }

    ARTrackedImageManager m_TrackedImageManager;

    private void Start()
    {
        b_objectSpawned = false;
        //objectSpawned.SetActive(false);
        //b_objectSpawned = true;
        //Input.compass.enabled = true;
        //Input.location.Start();
        ScaleValue = 50.0f;


  //      let configuration = ARKitSessionSubsystem.ARWorldTrackingSessionConfiguration();
  //configuration.worldAlignment = .gravityAndHeading;
    }

    void Awake()
    {
        if(objectSpawned == null)
        objectSpawned = GameObject.Find("_Level");

        //if (m_BattleSceneManager == null)
        //{
        //    m_BattleSceneManager = FindObjectOfType<BattleSceneManager>();
        //    Debug.LogError("BattleSceneManager NULL! {0}" + m_BattleSceneManager);
        //}


        //objectSpawned.SetActive(false);
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        //b_objectSpawned = false;
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
    }

    private void Update()
    {
        //hdn.text = Input.compass.trueHeading.ToString();
        //Application.Quit();
        //GetComponent<ARSessionOrigin>().transform.rotation = Quaternion.Slerp(objectSpawned.transform.rotation, Quaternion.Euler(0, Input.compass.trueHeading, 0), Time.deltaTime);

        //GetComponent<ARSessionOrigin>().MakeContentAppearAt(objectSpawned.transform, Quaternion.Euler(0, Input.compass.trueHeading, Time.deltaTime));
        if (b_objectSpawned == true)
        {
            //objectSpawned.transform.rotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);
            
        }
        //objectSpawned.transform.rotation = Quaternion.Slerp(objectSpawned.transform.rotation, Quaternion.Euler(0, Input.compass.trueHeading, 0), Time.deltaTime);
        //objectSpawned.transform.RotateAround(Camera.current.transform.position, Vector3.up, objectSpawned.transform.rotation.eulerAngles - Input.compass.rawVector);
        //objectSpawned.transform.RotateAround(GetComponent<ARSessionOrigin>().camera.transform.position, Vector3.up, Input.compass.trueHeading);//objectSpawned.transform.rotation.eulerAngles.y - 
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void UpdateInfo(ARTrackedImage trackedImage)
    {
        DebugText.AddMessage("Update info", 10.0f);
        // Set canvas camera
        /*var canvas = trackedImage.GetComponentInChildren<Canvas>();
        canvas.worldCamera = worldSpaceCanvasCamera;

        // Update information about the tracked image
        var text = canvas.GetComponentInChildren<Text>();
        text.text = string.Format(
            "{0}\ntrackingState: {1}\nGUID: {2}\nReference size: {3} cm\nDetected size: {4} cm",
            trackedImage.referenceImage.name,
            trackedImage.trackingState,
            trackedImage.referenceImage.guid,
            trackedImage.referenceImage.size * 1.0f,
            trackedImage.size * 1.0f);
*/
        //var planeParentGo = trackedImage.gameObject;//.GetChild(0).gameObject;
        var planeGo = trackedImage.gameObject;//;.transform//.GetChild(0).gameObject;

        // Disable the visual plane if it is not being tracked
        //if (trackedImage.trackingState != TrackingState.None)
        //{
        DebugText.AddMessage("trackedImage.trackingState" + trackedImage.gameObject.name, 10.0f);
            

            // The image extents is only valid when the image is being tracked
            trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);

            // Set the texture
            var material = planeGo.GetComponentInChildren<MeshRenderer>().material;
            material.mainTexture = (trackedImage.referenceImage.texture == null) ? defaultTexture : trackedImage.referenceImage.texture;
            //
            if (b_objectSpawned == false)
            {
                planeGo.SetActive(true);
                DebugText.AddMessage("b_objectSpawned false", 10.0f);
                

                objectSpawned.SetActive(true);
                    objectSpawned.transform.position = planeGo.transform.position;
                    if (m_SessionOrigin)
                    {
                    DebugText.AddMessage("Attempting to spawn", 10.0f);
                    //ScaleValue = 50.0f;
                    m_SessionOrigin.MakeContentAppearAt(objectSpawned.transform, planeGo.transform.position, Quaternion.identity);
                        m_SessionOrigin.transform.localScale = new Vector3(ScaleValue, ScaleValue, ScaleValue);
                        m_SessionOrigin.GetComponent<ARPointCloudManager>().enabled = false;
                        m_SessionOrigin.GetComponent<ARPlaneManager>().enabled = false;
                        planeGo.SetActive(false);
                    }

                    b_objectSpawned = true;
                    //GameObject uiManger = GameObject.Find("UIManager");
                    //if (uiManger)
                    //{

                    //    //UIManager.Instance.Pop();
                    //    uiManger.GetComponent<UIManagerOld>().Push(1);
                    //    DebugText.AddMessage("Pushing scene 1", 10.0f);
                    //    //UIManager.Instance.Push(2);
                    //}
                    //else
                    //{
                    //    DebugText.AddMessage("Unable to find UImanager instance", 10.0f);
                    //}
                    //if (m_BattleSceneManager)
                    //{
                    //    DebugText.AddMessage("Arena placed", 10.0f);
                    //    m_BattleSceneManager.ServerArenaPlaced();
                    //}
                //}
            }
            
        //}
        //else
        //{
        //    planeGo.SetActive(false);
        //}
    }

    public void SpawnCampus()
    {
        objectSpawned = Instantiate(objectToSpawnPrefab, Vector3.zero, Quaternion.identity);
        //LocationInfo._instance.AddAllRoomsToList();
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Give the initial image a reasonable default scale
            trackedImage.transform.localScale = new Vector3(0.01f, 1f, 0.01f);

            UpdateInfo(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
            UpdateInfo(trackedImage);
    }

    Vector3 OffsetCalculation(Vector3 planeGoPos, string roomName)
    {
        GameObject tempRoomObject = GameObject.Find(roomName);
        Vector3 offset = Vector3.zero;
        if (tempRoomObject)
        {
            offset = planeGoPos - tempRoomObject.transform.position;
        }
        return offset;
    }
    //Quaternion RotationCalculation(string roomName)
    //{
    //    GameObject tempRoomObject = GameObject.Find(roomName);
    //    Quaternion rot = tempRoomObject.transform.rotation;
    //    //if (tempRoomObject)
    //    //{
    //    //    offset = planeGoPos - tempRoomObject.transform.position;
    //    //}
    //    return rot;
    //}

    Vector3 RotationCalculation(Vector3 planeUp, string roomName)
    {
        GameObject tempRoomObject = GameObject.Find(roomName);
        Vector3 rot = planeUp - tempRoomObject.transform.up;
        //if (tempRoomObject)
        //{
        //    offset = planeGoPos - tempRoomObject.transform.position;
        //}
        return rot;
    }
}
