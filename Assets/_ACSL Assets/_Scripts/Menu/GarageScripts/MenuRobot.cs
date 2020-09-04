using UnityEngine;

public class MenuRobot : MonoBehaviour
{

    public RobotData menuRobotData;

    #region SOCKETS
    public GameObject Head;
    public GameObject RightArm;
    public GameObject LeftArm;
    public GameObject Body;
    public GameObject Legs;

    public Vector3 headSocket = Vector3.zero;
    public Vector3 rightarmSocket = Vector3.zero;
    public Vector3 leftarmSocket = Vector3.zero;
    public Vector3 bodySocket = Vector3.zero;
    public Vector3 legSocket = Vector3.zero;
    #endregion

    private readonly bool m_SwitchRobot;
    private readonly int m_CurrentRobotIndex = 0;
    private readonly MyTimer m_SwitchRobotTime;

    public float BodyRotationIntensity;
    public float BodyBobAmount;
    public float ArmBobAmount;
    public float HeadBobAmount;

    public Transform lookAtTransform;
    private Vector3 m_lookAtPosition;

    private void Start()
    {
        if (!lookAtTransform || lookAtTransform == null)
        {
            Debug.Log(gameObject.name);
            lookAtTransform = Camera.main.transform;
        }

        m_lookAtPosition = lookAtTransform.position;

        menuRobotData.UpdateValues();
        Build(menuRobotData);


        transform.LookAt(m_lookAtPosition);

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void Update()
    {
        m_lookAtPosition = lookAtTransform.position;
        transform.LookAt(m_lookAtPosition);

        AnimateRobot(Time.time);
    }

    private void AnimateRobot(float deltaTime)
    {
        float SinwaveAmount = Mathf.Sin(deltaTime);
        Vector3 currentBodyRot = Body.transform.eulerAngles;
        Vector3 currentBodyPos = Body.transform.position;

        currentBodyRot += new Vector3(0.0f, BodyRotationIntensity * SinwaveAmount, 0.0f);
        currentBodyPos += new Vector3(0.0f, BodyBobAmount * SinwaveAmount, 0.0f);

        Vector3 currentRArmPos = RightArm.transform.position;
        Vector3 currentLArmPos = LeftArm.transform.position;
        Vector3 currentHeadPos = Head.transform.position;

        currentRArmPos += new Vector3(0.0f, ArmBobAmount * SinwaveAmount, 0.0f);
        currentLArmPos += new Vector3(0.0f, ArmBobAmount * SinwaveAmount, 0.0f);
        currentHeadPos += new Vector3(0.0f, HeadBobAmount * SinwaveAmount, 0.0f);

        Body.transform.eulerAngles = currentBodyRot;
        Body.transform.position = currentBodyPos;
        RightArm.transform.position = currentRArmPos;
        LeftArm.transform.position = currentLArmPos;
        Head.transform.position = currentHeadPos;

    }

    public void Build(RobotData robotData)
    {
        // Clear robot parts
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        transform.rotation = Quaternion.identity;

        legSocket = transform.position;

        Legs = Instantiate(robotData.legs.gameObject, transform.position, Quaternion.identity, transform);

        bodySocket = Legs.transform.GetChild(0).position;

        Body = Instantiate(robotData.body.gameObject, bodySocket, Quaternion.identity, transform);

        headSocket = Body.transform.GetChild(0).position;
        leftarmSocket = Body.transform.GetChild(1).position;
        rightarmSocket = Body.transform.GetChild(2).position;

        RightArm = Instantiate(robotData.rArm.gameObject, rightarmSocket, Quaternion.identity, Body.transform);
        LeftArm = Instantiate(robotData.lArm.gameObject, leftarmSocket, Quaternion.identity, Body.transform);
        Head = Instantiate(robotData.head.gameObject, headSocket, Quaternion.identity, Body.transform);


        // Layer all parts to default (prevent occlusion shader)
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }


    #region LEGACY
    //public void CheckForInput()
    //{
    //    if(Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        m_CurrentRobotIndex++;

    //        menuRobotData = GarageManager.Instance.GetRobot(m_CurrentRobotIndex);

    //        if(menuRobotData == null)
    //        {
    //            m_CurrentRobotIndex = 0;
    //            menuRobotData = GarageManager.Instance.GetRobot(m_CurrentRobotIndex);
    //        }

    //        ClearParts();   

    //        Build();
    //        m_SwitchRobot = true;
    //    }
    //}

    //public void ClearParts()
    //{
    //    Destroy(Head);
    //    Destroy(RightArm);
    //    Destroy(LeftArm);
    //    Destroy(Body);
    //    Destroy(Legs);

    //    headSocket = Vector3.zero;
    //    rightarmSocket = Vector3.zero;
    //    leftarmSocket = Vector3.zero;
    //    bodySocket = Vector3.zero;
    //    legSocket = Vector3.zero;
    //}
    #endregion

}
