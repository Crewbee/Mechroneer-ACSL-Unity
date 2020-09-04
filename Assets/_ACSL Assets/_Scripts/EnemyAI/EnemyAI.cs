//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.AI;

//TODO: DO NOT DELETE
//public class EnemyAI : AI
//{
//    //private Robot robot;
//    //private RobotAI m_RobotAI;
//    private MyTimer specialAttackTimer;
//    private RobotMovement robotMovement;
//    private EnergyComponent energyComponent;
//    private HealthComponentOld healthComponent;
//    private SenseComponentOld senseComponent;
//    private bool specialAttackAlt = true;

//    private float lastDamage = 0f;

//    //clamped from 1.0f to -1.0f where 1.0f is full offensive and -1.0f is full defensive
//    public float aggression = 1.0f;

//    //Amount that making a decision can vary on
//    public float variance = 1.0f;
//    //Value that Aggression increases by guaranteed, is then impacted by Variance
//    public float enmityGrowth = 0.05f;

//    //Stores max health to be used for getting health percent
//    private float maxHealth = 0;

//    #region PUBLIC_VARIABLES
//    public float coverUpdateRate = 1f;
//    public GameObject Target; // Define this at runtime
//    public int coverSampleCount = 32;
//    public int coverSampleLayers = 2;
//    public float coverSampleRadius = 20f;
//    [Range(-0.5f, 0.5f)]
//    public float coverQuality = 0.5f;
//    #endregion

//    #region PRIVATE_VARIABLES
//    private NavMeshAgent navMeshAgent;
//    private Vector3[] coverSamples;
//    [SerializeField]
//    private bool isVisible = false;
//    private bool m_FindingCover = false;
//    #endregion

//    // Start is called before the first frame update
//    public override void Init(RobotOld robot)
//    {

//        // Cache local robot data
//        if (robot)
//        {
//            robotMovement = robot.GetComponent<RobotMovement>();
//            navMeshAgent = robot.GetComponent<NavMeshAgent>();
//            healthComponent = robot.GetComponent<HealthComponentOld>();
//            energyComponent = robot.GetComponent<EnergyComponent>();
//            senseComponent = robot.GetComponent<SenseComponentOld>();
//            navMeshAgent = robot.GetComponent<NavMeshAgent>();

//            maxHealth = healthComponent.maxHealth;
//            specialAttackTimer = new MyTimer();
//        }
//        else
//        {
//            Debug.LogError("Robot not found!");
//        }

//        // Initialize EnemyAI
//        coverSamples = new Vector3[coverSampleCount * coverSampleLayers];
//        aggression = Mathf.Clamp(aggression, -1.0f, 1.0f);
//        //StartCoroutine(UpdateSamples(coverUpdateRate));
//    }

//    // Update is called once per frame
//    public override void Update()
//    {
//        // Get new target data
//        if (senseComponent.visibleTargets.Count() > 0)
//        {
//            if (senseComponent.visibleTargets.First())
//                Target = senseComponent.visibleTargets.First().gameObject;
//        }

//        //Alternates which special attack is fired at a given time when the player is visible
//        if (specialAttackTimer != null)
//        {
//            if (!specialAttackTimer.active)
//            {
//                specialAttackTimer.StartTimer(1.2f);
//                specialAttackAlt = !specialAttackAlt;
//            }
//            specialAttackTimer.Update();
//        }
//        RaycastHit hit = new RaycastHit();
//        if (robot)
//        {
//            if (Target)
//            {
//                if (Physics.Raycast(robot.transform.position, Target.transform.position - robot.transform.position, out hit))
//                {
//                    if (hit.transform.gameObject == Target /*&& Vector3.Angle(m_Target.transform.forward, m_Target.transform.position - transform.position) <= 60 / 2*/)
//                    {
//                        isVisible = true;
//                        //Debug.Log("AI can see you");
//                    }
//                    else
//                    {
//                        isVisible = false;
//                        //Debug.Log("AI can't see you");
//                    }
//                }

//                CalculateCurrentAggression();
//                robot.TurnToFaceTarget();

//                //if (m_RobotAI.m_CurrentBehavior == RobotAI.NodeName.IsAutoAttack)
//                robot.ArmAbility();
//                //else
//                //    m_RobotData.ArmAbility(false);

//                if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
//                {
//                    if (aggression > 0.0f)
//                    {
//                        AggressiveBehavior();
//                    }
//                    else
//                    {
//                        DefensiveBehavior();
//                    }
//                }
//            }
//            else
//            {
//                MoveToRandomRobot();
//            }
//        }
//    }


//    private void MoveToRandomRobot()
//    {
//        if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
//        {
//            var test = RobotOld.registeredRobots.Values;
//            RobotOld[] array = new RobotOld[test.Count];
//            test.CopyTo(array, 0);
//            //List<GameObject> Robots = (BattleSceneManager.instance.gameMode.gameState as TestGameState).robots;
//            //Robots.Remove(robot.gameObject);
//            int range = Random.Range(0, array.Length);
//            if (array[range] == null)
//                return;
//            if (array[range] == robot.gameObject)
//                return;

//            robotMovement.MoveToPoint(array[range].transform.position);
//            //int range = Random.Range(0, Robots.Count);
//            //if (Robots[range] == null)
//            //    return;
//            //if (Robots[range] == robot.gameObject)
//            //    return;

//            //robotMovement.MoveToPoint(Robots[range].transform.position);
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        var temp = NearestPositionOfArea(robot.transform.position, 1);
//        Gizmos.DrawSphere(temp, 0.5f);
//        Gizmos.color = Color.yellow;
//    }

//    private IEnumerator UpdateSamples(float updateRate)
//    {
//        while (robot)
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
//            yield return new WaitForSeconds(updateRate);
//        }
//    }

//    private Vector3 NearestPositionOfArea(Vector3 point, int areaMask)
//    {
//        NavMeshHit hit = new NavMeshHit();
//        NavMesh.FindClosestEdge(point, out hit, areaMask);
//        return hit.position;
//    }

//    float GetHealthPercent(float health)
//    {
//        float totalHealth = 0;

//        if (health != 0)
//        {
//            totalHealth = health / maxHealth;
//        }

//        return totalHealth;
//    }

//    void CalculateCurrentAggression()
//    {
//        float damage = lastDamage - healthComponent.health;
//        aggression -= damage * 0.005f;
//        lastDamage = healthComponent.health;

//        aggression += ((enmityGrowth * GetHealthPercent(healthComponent.health)) + Random.Range(-variance, variance)) * Time.fixedDeltaTime;
//        aggression = Mathf.Clamp(aggression, -1.0f, 1.0f);
//    }

//    void AggressiveBehavior()
//    {
//        if (aggression > 0.0f)
//        {
//            if (Target && Target.GetComponent<RobotOld>())
//            {
//                //TODO: Reimplement when ability is replaced
//                //float leftArm = Target.GetComponent<Robot>().robotData.lArm.autoAttackData.range;
//                //float rightArm = Target.GetComponent<Robot>().robotData.rArm.autoAttackData.range;

//                float magnitude = 0;

//                if (isVisible)
//                {
//                    //TODO: Reimplement when ability is replaced
//                    //if (leftArm < rightArm)
//                    //{
//                    //    magnitude = leftArm;
//                    //}
//                    //else
//                    //{
//                    //    magnitude = rightArm;
//                    //}
//                }
//                else
//                {
//                    magnitude = 1.0f;
//                }

//                Vector3 dir = Vector3.Normalize(Target.transform.position - robot.transform.position);
//                Vector3 point = Target.transform.position - (dir * magnitude);

//                if (point.magnitude <= magnitude)
//                {
//                    if (specialAttackAlt)
//                    {
//                        point = robot.transform.right * 7.0f;
//                    }
//                    else
//                    {
//                        point = -robot.transform.right * 7.0f;
//                    }
//                }

//                robotMovement.MoveToPoint(point);
//                if (isVisible && energyComponent.energy > 100)
//                {
//                    //TODO: Reimplement a way for the AI to fire abilities
//                    //robot.LeftArmSpecialAttack(specialAttackAlt);
//                    //robot.RightArmSpecialAttack(!specialAttackAlt);

//                }

//                else if (!isVisible && energyComponent.energy > 150)
//                {
//                    //robot.LegAbility();
//                }
//            }
//        }
//        if (aggression > 0.2f)
//        {

//        }
//        if (aggression > 0.4f)
//        {

//        }
//        if (aggression > 0.6f)
//        {

//        }
//        if (aggression > 0.8f)
//        {

//        }
//    }

//    void DefensiveBehavior()
//    {
//        if (aggression < -0.0f && aggression >= -0.2)
//        {
//            m_FindingCover = false;
//            if (Target)
//            {
//                //TODO: Reimplement when ability is replaced
//                //float leftArm = Target.GetComponent<Robot>().robotData.lArm.autoAttackData.range;
//                //float rightArm = Target.GetComponent<Robot>().robotData.rArm.autoAttackData.range;

//                float magnitude = 0;

//                if (isVisible)
//                {
//                    //if (leftArm < rightArm)
//                    //{
//                    //    magnitude = leftArm;
//                    //}
//                    //else
//                    //{
//                    //    magnitude = rightArm;
//                    //}
//                }
//                else
//                {
//                    magnitude = 1.0f;
//                }

//                Vector3 dir = Vector3.Normalize(Target.transform.position - robot.transform.position);
//                Vector3 point = Target.transform.position - (dir * magnitude);

//                if (point.magnitude <= magnitude)
//                {
//                    if (specialAttackAlt)
//                    {
//                        point = robot.transform.right * 7.0f;
//                    }
//                    else
//                    {
//                        point = -robot.transform.right * 7.0f;
//                    }
//                }

//                robotMovement.MoveToPoint(point);
//                if (isVisible && energyComponent.energy > 60)
//                {
//                    //TODO: Reimplement a way for the AI to fire abilities
//                    //robot.LegAbility();
//                }
//            }
//        }
//        if (aggression < -0.20001f)
//        {

//            Vector3 points = (robotMovement.finalPosition - robot.transform.position);
//            float dist = points.magnitude;

//            if (isVisible && m_FindingCover == false)
//            {
//                FindCover();
//            }

//            if (dist < 3.0f)
//            {
//                m_FindingCover = false;
//            }

//            if (isVisible && energyComponent.energy > 100)
//            {

//                //TODO: Reimplement a way for the AI to fire abilities
//                //robot.BodyAbility();
//                //robot.LegAbility();
//            }

//        }
//        if (aggression < -0.4f)
//        {

//        }
//        if (aggression < -0.6f)
//        {

//        }
//        if (aggression < -0.8f)
//        {

//        }
//    }

//    //private void Wander()
//    //{
//    //    float walkDistance = UnityEngine.Random.Range(m_MinWalkDistance, m_MaxWalkDistance);
//    //    Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * walkDistance;

//    //    randomDirection += transform.position;

//    //    // Generate random point to move to
//    //    NavMeshHit hit = new NavMeshHit();
//    //    NavMesh.SamplePosition(randomDirection, out hit, walkDistance, 1);
//    //    Vector3 finalPosition = hit.position;

//    //    // Move to position
//    //    m_NavMeshAgent.SetDestination(finalPosition);
//    //}

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
//        m_FindingCover = true;
//        robotMovement.MoveToPoint(candidates[0]);
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

//    public float GetPathLength(NavMeshPath path)
//    {
//        float pathLength = 0.0f;

//        for (int i = 0; i < path.corners.Length; i++)
//        {
//            if (i == 0)
//            {
//                // First measurement == 0.0f (calculation would break index range)
//                continue;
//            }
//            else
//            {
//                // Calculate the distance between the last node to the next
//                pathLength += Vector3.Distance(path.corners[i - 1], path.corners[i]);
//            }
//        }

//        return pathLength;
//    }

//    private void OnDrawGizmosSelected()
//    {
//            // Draw Facing Direction
//            Gizmos.color = Color.yellow;
//            Gizmos.DrawLine(robot.transform.position, robot.transform.position + robot.transform.forward.normalized * 2);

//            // Draw Path
//            Gizmos.color = Color.blue;

//            foreach (Vector3 corner in navMeshAgent.path.corners)
//            {
//                Gizmos.DrawSphere(corner, 0.25f);
//            }

//            for (int i = 0; i < navMeshAgent.path.corners.Length; i++)
//            {
//                if (i + 1 >= navMeshAgent.path.corners.Length)
//                {
//                    break;
//                }

//                Gizmos.DrawLine(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
//            }

//            // Draw Samples
//            Gizmos.color = Color.grey;

//            for (int i = 0; i < coverSamples.Length; i++)
//            {
//                Gizmos.DrawLine(robot.transform.position, coverSamples[i]);
//                Gizmos.DrawWireCube(coverSamples[i], Vector3.one * 0.1f);
//            }
//    }
//}




