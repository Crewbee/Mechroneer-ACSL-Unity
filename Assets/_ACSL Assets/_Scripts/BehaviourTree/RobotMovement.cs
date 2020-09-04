//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//TODO: REMAKE THIS

//[RequireComponent(typeof(NavMeshAgent))]
//public class RobotMovement : MonoBehaviour
//{
//    //private RobotOld robot;
//    private Rigidbody rigidBody;
//    public NavMeshPath navMeshPath;
//    public Vector3 finalPosition;
//    public bool movementEnabled = true;

//    private NavMeshAgent navMeshAgent;
//    private Vector3 currentDirection;
//    private int pathIndex = 0;

//    // Start is called before the first frame update
//    void Start()
//    {
//        //robot = GetComponent<RobotOld>();

//        rigidBody = GetComponent<Rigidbody>();
//        rigidBody.detectCollisions = true;

//        currentDirection = Vector3.zero;

//        navMeshAgent = GetComponent<NavMeshAgent>();
//        navMeshAgent.updatePosition = false;
//        navMeshAgent.updateRotation = false;

//        if (movementEnabled != true)
//            movementEnabled = true;

//        navMeshPath = new NavMeshPath();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (movementEnabled)
//        {
//            //NavMeshAgent is only used to caluculate paths so it just teleports to the object's location
//            navMeshAgent.Warp(transform.position);

//            //Slap this in Player's movement but not AI's, all AI has to do is pass in a new location using MoveToPoint

//            if (navMeshPath.corners.Length > pathIndex)
//            {
//                //m_NavMeshAgent.enabled = false;
//                if (transform.position != navMeshPath.corners[pathIndex])
//                {
//                    currentDirection = navMeshPath.corners[pathIndex] - transform.position;
//                    //m_Direction = m_Direction.normalized;

//                    Vector3 position = new Vector3(transform.position.x, -navMeshPath.corners[pathIndex].y, transform.position.z);

//                    // Rotate to match velocity
//                    if (robot.target == null || robot.isAiming == false)
//                    {
//                        Vector3 direction = navMeshPath.corners[pathIndex] - position;
//                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(this.currentDirection), 3.0f * Time.deltaTime);
//                    }

//                    if (Vector3.Distance(transform.position, navMeshPath.corners[pathIndex]) < 1.0f * Time.deltaTime)
//                    {
//                        transform.position = navMeshPath.corners[pathIndex];
//                    }
//                    else
//                    {
//                        currentDirection = currentDirection.normalized;
//                        transform.position = transform.position + currentDirection * 4.0f * Time.deltaTime;
//                        //go.transform.Translate(0, 0, Speed * Time.deltaTime);
//                    }
//                    //m_Rigidbody.AddForce(m_Direction * 1000.0f, ForceMode.Force);

//                    float distance = Vector3.Distance(transform.position, navMeshPath.corners[pathIndex]);
//                    if (distance < 1.0f)
//                    {
//                        pathIndex++;
//                    }
//                }
//            }
//            else
//            {
//                //m_NavMeshAgent.enabled = true;
//            }
//        }
//    }

//    public void MoveToPoint(Vector3 point)
//    {
//        if (navMeshAgent)
//        {
//            if (navMeshAgent.isActiveAndEnabled == true)
//            {
//                NavMeshPath testPath = new NavMeshPath();

//                if (navMeshAgent.CalculatePath(point, testPath) == true)
//                {
//                    navMeshAgent.CalculatePath(point, navMeshPath);
//                    pathIndex = 1;
//                    finalPosition = point;
//                }
//            }
//        }
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject)
//        {
//            if (collision.relativeVelocity.magnitude >= 0.25)
//            {

//            }
//        }
//    }

//    private void OnDrawGizmosSelected()
//    {
//        if (navMeshPath != null)
//        {
//            foreach (Vector3 corn in navMeshPath.corners)
//            {
//                Gizmos.color = new Color(1, 1, 0, 0.75F);
//                Gizmos.DrawSphere(corn, 0.25f);
//            }
//        }
//    }

//    public void ResetPath()
//    {
//        navMeshPath.ClearCorners();
//    }
//}