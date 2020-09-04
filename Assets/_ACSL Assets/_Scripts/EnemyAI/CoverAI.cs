//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.AI;

//TODO: DO NOT DELETE
//public class CoverAI : AI
//{
//    //AI behavior that attempts to take cover and use more defensive skills.
//    //It will also attempt to conserve energy, only using dashes sparingly
//    //if energy is low and is far enough away from enemies.

//    #region PUBLIC_VARIABLES
//    public float coverUpdateRate = 1f;
//    public GameObject Target; // Define this at runtime
//    public int coverSampleCount = 40;
//    public int coverSampleLayers = 3;
//    public float coverSampleRadius = 20f;
//    [Range(-0.5f, 0.5f)]
//    public float coverQuality = 0.5f;
//    public List<Transform> visibleTargets = new List<Transform>();

//    #endregion

//    #region PRIVATE_VARIABLES
//    private NavMeshAgent navMeshAgent;
//    private Vector3[] coverSamples;
//    [SerializeField]
//    private MyTimer UpdateSampleTimer;
//    private MyTimer InViewTimer;
//    private MyTimer DashTimer;
//    private SenseComponentOld senseComp;
//    RobotPart leg;
//    RobotPart body;
//    #endregion

//    public override void Init(RobotOld robot)
//    {
//        if (robot)
//        {
//            navMeshAgent = robot.GetComponent<NavMeshAgent>();
//            senseComp = robot.senseComponent;
//            this.robot = robot;

//            if (robot.target == null)
//            {
//                FindTargets();
//                if (visibleTargets.Count != 0)
//                {
//                    if (visibleTargets[0] != robot)
//                    {
//                        SetTarget(visibleTargets[0].GetComponent<RobotOld>());
//                        Debug.Log("Target set");
//                    }
//                }
//                else if (Target == null)
//                {
//                    TargetRandomRobot();
//                }
//            }
//        }

        
//        UpdateSampleTimer = new MyTimer();
//        InViewTimer = new MyTimer();
//        DashTimer = new MyTimer();
//        leg = robot.GetComponent<RobotOld>().GetRobotPart(RobotPartType.Leg);
//        body = robot.GetComponent<RobotOld>().GetRobotPart(RobotPartType.Body);
//        coverSamples = new Vector3[coverSampleCount * coverSampleLayers];
//        UpdateSamples(coverUpdateRate);

//        //StartCoroutine(UpdateSamples(coverUpdateRate));
//    }

//    public override void Update()
//    {
//        UpdateSampleTimer.Update();
//        InViewTimer.Update();
//        DashTimer.Update();

//        if (UpdateSampleTimer.timeLeft <= 0)
//        {
//            UpdateSamples(coverUpdateRate);
//        }

//        if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
//        {
//            Vector3 points = (robot.finalPosition - robot.finalPosition);
//            float dist = points.magnitude;

//            if (Target != null)
//            {
//                if (robot.IsTargetVisible(10.0f))
//                {
//                    InViewTimer.StartTimer(1.0f);
//                }
//            }
//            else
//            {
//                TargetRandomRobot();
//                InViewTimer.StartTimer(1.0f);
//            }

//            if (InViewTimer.timeLeft > 0)
//            {
//                FindCover();
//                if (robot.IsTargetVisible(10.0f) && robot.energyComponent.energy > 150 && !DashTimer.active)
//                {
//                    if (leg.specialAbilityTimer.timeLeftSeconds <= 0.0f)
//                    {
//                        robot.FireAbility(RobotPartType.Leg, robot, Vector3.zero);
//                        DashTimer.StartTimer(1.75f);
//                    }
//                }

//                if (robot.energyComponent.energy > 120)
//                {
//                    if (body.specialAbilityTimer.timeLeftSeconds <= 0.0f)
//                    {
//                        robot.FireAbility(RobotPartType.Body, robot, robot.transform.position);
//                    }
//                }
//            }
//            else
//            {
//                //get back into the fight faster
//                robot.Aggression = 1.0f;
//            }

//        }
//    }

//    #region COVER_FUNCTIONS
//    private void UpdateSamples(float updateRate)
//    {
//        if (robot != null)
//        {
//            // Update layer
//            for (int i = 1; i <= coverSampleLayers; i++)
//            {
//                // Update radius
//                for (int j = 0; j < coverSamples.Length / i; j++)
//                {
//                    float angle = (Mathf.Deg2Rad * -robot.transform.rotation.eulerAngles.y) + (j * Mathf.PI * 2f / coverSamples.Length * i);
//                    Vector3 newPos = robot.transform.position + new Vector3(Mathf.Cos(angle) * coverSampleRadius / i, 0, Mathf.Sin(angle) * coverSampleRadius / i);
//                    coverSamples[j * i] = newPos;
//                }
//            }
//            UpdateSampleTimer.StartTimer(coverUpdateRate);
//        }
//    }

//    private void FindCover()
//    {
//        NavMeshHit hit = new NavMeshHit();
//        List<Vector3> candidates = new List<Vector3>();

//        // Test all samples and populate distances
//        for (int i = 0; i < coverSamples.Length; i++)
//        {
//            // Find nearest wall to sample position 
//            if (NavMesh.FindClosestEdge(coverSamples[i], out hit, NavMesh.GetAreaFromName("Not Walkable")))
//            {
//                // test normal direction against enemy location (avg enemy location)
//                if (Vector3.Dot(hit.normal, (Target.transform.position - robot.transform.position)) < -coverQuality)
//                {
//                    Debug.DrawLine(robot.transform.position, hit.position, Color.yellow);
//                    candidates.Add(hit.position);
//                }
//            }
//        }

//        if (candidates.Count() == 0)
//        {
//            Debug.Log("NO CANDIDATES!");
//            //Debug.Break();

//            return;
//        }

//        // Determine shortest path to cover
//        for (int i = 0; i < candidates.Count(); i++)
//        {
//            NavMeshPath path = new NavMeshPath();

//            // If path invalid retry
//            if (!navMeshAgent.CalculatePath(candidates[i], path))
//            {
//                continue;
//            }

//            // Remove candidates that would move us closer to the target (with variance of 5%)
//            if (MinimumTargetDistance(path) < Vector3.Distance(robot.transform.position, Target.transform.position))// && candidates.Count - 1 > 0)
//            {
//                candidates.RemoveAt(i);
//            }
//        }

//        // Sort by path length
//        candidates = candidates.OrderBy(x => Vector3.Distance(x, robot.transform.position)).ToList();

//        // Remove candidates if they are in view of the target
//        for (int i = 0; i < candidates.Count(); i++)
//        {
//            RaycastHit hitInfo = new RaycastHit();
//            if (Physics.Raycast(candidates[i], Target.transform.position - candidates[i], out hitInfo, Vector3.Distance(Target.transform.position, candidates[i])) && candidates.Count - 1 > 0)
//            {
//                candidates.RemoveAt(i);
//            }
//        }

//        if (candidates.Count() == 0)
//        {
//            Debug.Log("NO CANDIDATES!");
//            Debug.Break();
//            return;
//        }

//        // Move to cover!
//        //m_FindingCover = true;
//        robot.MoveToPoint(candidates[0]);
//    }

//    public float MinimumTargetDistance(NavMeshPath path)
//    {
//        float minimumDistance = 0.0f;

//        for (int i = 0; i < path.corners.Length; i++)
//        {
//            if (i + 1 > path.corners.Length - 1)
//            {
//                return minimumDistance;
//            }

//            if (Vector3.Distance(path.corners[i], Target.transform.position) < Vector3.Distance(path.corners[i + 1], Target.transform.position))
//            {
//                minimumDistance = Vector3.Distance(path.corners[i], Target.transform.position);
//            }
//        }

//        return minimumDistance;
//    }
//    #endregion

//    void FindTargets()
//    {
//        visibleTargets = senseComp.visibleTargets;
//    }

//    void SetTarget(RobotOld target)
//    {
//        if (target != null)
//        {
//            robot.SetRobotTarget(target);
//            Target = target.GetGameObject();
//        }
//    }

//    private void TargetRandomRobot()
//    {
//        var test = RobotOld.registeredRobots.Values;
//        RobotOld[] array = new RobotOld[test.Count];
//        test.CopyTo(array, 0);
//        int range = Random.Range(0, array.Length);
//        if (array[range] == robot)
//        {
//            for (int i = 0; i < array.Length; i++)
//            {
//                if (array[i] != null)
//                {
//                    if (array[i] != robot)
//                    {
//                        range = i;
//                        break;
//                    }
//                }
//            }
//        }

//        robot.SetRobotTarget(array[range]);
//        Target = array[range].GetGameObject();
//    }
//}
