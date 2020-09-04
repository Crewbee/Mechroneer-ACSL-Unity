//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TempBehaviour : CameraBehaviour
//{
//    public float HorizontalEaseSpeed = 5.0f;
//    public float VerticalEaseSpeed = 5.0f;
//    public float LookPosEaseSpeed = 5.0f;

//    public Vector3 PlayerMaxDistLocalLookPos = Vector3.zero;
//    public Vector3 PlayerMinDistLocalLookPos = Vector3.zero;

//    public Vector3 PlayerLocalPivotPos = Vector3.zero;

//    public float YawRotateSpeed = 1.0f;
//    public float PitchRotateSpeed = 1.0f;

//    public float MaxVerticalAngle = 70.0f;

//    public float MaxDistFromPlayer = 6.0f;
//    public float MinDistanceFromPlayer = 5.0f;
//    public float AutoRotateDelayTime = 1.0f;

//    public float ObstacleCheckRadius = 0.5f;
//    public Vector3 PlayerLocalObstructionMovePos = Vector3.zero;
//    private int m_RaycastHitMask;

//    public override void Activate()
//    {
//        base.Activate();

//        m_GoalPos = m_CameraDriver.transform.position;

//        m_AllowAutoRotate = false;
//        m_TimeTillAutoRotate = AutoRotateDelayTime;
//        m_RaycastHitMask = ~LayerMask.GetMask("Player", "IgnoreRaycast");

//    }

//    public override void Deactivate()
//    {
//        base.Deactivate();
//    }

//    public override void LateUpdate()
//    {
//        Vector3 worldPivotPos = m_Player.transform.TransformPoint(PlayerLocalPivotPos);
//        Vector3 offsetFromPlayer = m_GoalPos - worldPivotPos;
//        float distFromPlayer = offsetFromPlayer.magnitude;
//        Vector3 rotateAmount = new Vector3(m_PitchInput * PitchRotateSpeed, m_YawInput * YawRotateSpeed);

//        m_TimeTillAutoRotate -= Time.deltaTime;

//        if(!Mathf.Approximately(rotateAmount.y, MathUtils.CompareEpsilon))
//        {
//            m_AllowAutoRotate = false;
//            m_TimeTillAutoRotate = AutoRotateDelayTime;
//        }
//        else if(m_TimeTillAutoRotate <= 0.0f)
//        {
//            m_AllowAutoRotate = true;
//        }

//        Vector3 pivotRotation = m_CameraDriver.PivotRotation;

//        if(m_AllowAutoRotate)
//        {
//            Vector3 anglesFromPlayer = Quaternion.LookRotation(offsetFromPlayer).eulerAngles;
//            pivotRotation.y += anglesFromPlayer.y;
//        }
//        else
//        {
//            pivotRotation.y = rotateAmount.y;
//        }

//        pivotRotation.y += m_Player.GroundAngularVelocity.y * Time.deltaTime;
//        pivotRotation.x -= rotateAmount.x;
//        pivotRotation.x = Mathf.Clamp(pivotRotation.x, -MaxVerticalAngle, MaxVerticalAngle);

//        m_CameraDriver.PivotRotation = pivotRotation;

//        distFromPlayer = Mathf.Clamp(distFromPlayer, MinDistanceFromPlayer, MaxDistFromPlayer);
//        offsetFromPlayer = Quaternion.Euler(pivotRotation.x, pivotRotation.y, 0.0f) * Vector3.forward;
//        offsetFromPlayer *= distFromPlayer;

//        m_GoalPos = offsetFromPlayer + worldPivotPos;

//        Vector3 newCameraPos = m_CameraDriver.transform.position;
//        newCameraPos = MathUtils.SlerpToHoriz(HorizontalEaseSpeed, newCameraPos, m_GoalPos, worldPivotPos, Time.deltaTime);
//        newCameraPos.y = MathUtils.LerpTo(VerticalEaseSpeed, newCameraPos.y, m_GoalPos.y, Time.deltaTime);

//        m_CameraDriver.transform.position = newCameraPos;
//        HandleObstacles();

//        Vector3 goalLookPos = m_Player.transform.TransformPoint(Vector3.zero);
//        m_CameraDriver.LookPosition = MathUtils.LerpTo(LookPosEaseSpeed, m_CameraDriver.LookPosition, goalLookPos, Time.deltaTime);
//        Vector3 lookDir = m_CameraDriver.LookPosition - m_CameraDriver.transform.position;
//        m_CameraDriver.transform.rotation = Quaternion.LookRotation(lookDir);
//    }

//    protected float HandleObstacles()
//    {
//        // Define a ray from the player to the camera
//        Vector3 rayStart = m_Player.transform.TransformPoint(PlayerLocalObstructionMovePos);
//        Vector3 rayEnd = m_CameraDriver.transform.position;

//        Vector3 rayDir = rayEnd - rayStart;

//        float rayDist = rayDir.magnitude;

//        if (rayDist <= 0f)
//        {
//            return 0f;
//        }

//        rayDir /= rayDist;

//        RaycastHit[] hitInfos = Physics.SphereCastAll(rayStart, ObstacleCheckRadius, rayDir, rayDist, m_RaycastHitMask);
//        if (hitInfos.Length <= 0)
//        {
//            return rayDist;
//        }

//        float minMoveUpDist = float.MaxValue;
//        foreach (RaycastHit hitInfo in hitInfos)
//        {
//            minMoveUpDist = Mathf.Min(minMoveUpDist, hitInfo.distance);
//        }

//        if (minMoveUpDist < float.MaxValue)
//        {
//            m_CameraDriver.transform.position = rayStart + rayDir * minMoveUpDist;
//        }

//        return minMoveUpDist;
//    }

//    public override void SetFacingDirection(Vector3 lookDirection)
//    {
        
//    }

//    public override Vector3 GetControlRotation()
//    {
//        return base.GetControlRotation();
//    }

//    public override void UpdateRotation(float yaw, float pitch)
//    {
//        m_YawInput = yaw;
//        m_PitchInput = pitch;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public override void FixedUpdate()
//    {
//        throw new System.NotImplementedException();
//    }

//    Vector3 m_GoalPos = Vector3.zero;
//    float m_YawInput = 0f;
//    float m_PitchInput = 0f;
//    float m_TimeTillAutoRotate = 0f;
//    bool m_AllowAutoRotate;
//}
