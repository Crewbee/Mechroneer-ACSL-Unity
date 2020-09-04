using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CoverState : AIState
{

    #region PUBLIC_VARIABLES
    public float coverUpdateRate = 1f;
    public GameObject Target; // Define this at runtime
    public int coverSampleCount = 40;
    public int coverSampleLayers = 3;
    public float coverSampleRadius = 20f;
    [Range(-0.5f, 0.5f)]
    public float coverQuality = 0.5f;
    public List<Transform> visibleTargets = new List<Transform>();

    #endregion

    #region PRIVATE_VARIABLES
    private NavMeshAgent navMeshAgent;
    private Vector3[] coverSamples;
    [SerializeField]
    private MyTimer UpdateSampleTimer;
    private MyTimer InViewTimer;
    private MyTimer DashTimer;
    private SenseComponent senseComp;
    private EnergyComponent energyComp;
    private MechroneerAIController aiController;
    RobotPart leg;
    RobotPart body;
    #endregion
    // Start is called before the first frame update

    public CoverState()
    {
        UpdateSampleTimer = new MyTimer();
        InViewTimer = new MyTimer();
        DashTimer = new MyTimer();
        coverSamples = new Vector3[coverSampleCount * coverSampleLayers];
    }

    public override void Init(Robot robot, MechroneerAIController controller)
    {
        navMeshAgent = robot.GetComponent<NavMeshAgent>();
        energyComp = robot.energyComponent;
        senseComp = robot.senseComponent;
        aiController = controller;

        if (robot.GetMainTarget() == null)
        {

            FindTargets();
            if (visibleTargets.Count != 0)
            {
                SetTarget(robot, visibleTargets[0].GetComponent<Robot>());
            }
            else
            {
                TargetRandomRobot(robot);
            }
        }

        robot.robotParts.TryGetValue(RobotPartType.Body, out body);
        robot.robotParts.TryGetValue(RobotPartType.LeftArm, out leg);
        UpdateSamples(coverUpdateRate, robot);
    }

    // Update is called once per frame
    public override void Update(Robot robot, MechroneerController.IActions player)
    {
        UpdateSampleTimer.Update();
        InViewTimer.Update();
        DashTimer.Update();

        if (UpdateSampleTimer.timeLeft <= 0)
        {
            UpdateSamples(coverUpdateRate, robot);
        }

        if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {

            if (Target != null)
            {
                if (IsTargetVisible(robot, 10.0f))
                {
                    InViewTimer.StartTimer(1.0f);
                }
            }
            else
            {
                TargetRandomRobot(robot);
                InViewTimer.StartTimer(1.0f);
            }

            if (InViewTimer.timeLeft > 0)
            {
                FindCover(robot);
                if (IsTargetVisible(robot, 10.0f) && energyComp.currentValue > 150 && !DashTimer.active)
                {
                    if (leg.specialAbilityTimer.timeLeftSeconds <= 0.0f)
                    {
                        player.SelectAbility4();
                        robot.FireAbility(robot, navMeshAgent.destination);
                        DashTimer.StartTimer(1.75f);
                    }
                }

                if (energyComp.currentValue > 120)
                {
                    if (body.specialAbilityTimer.timeLeftSeconds <= 0.0f)
                    {
                        player.SelectAbility3();
                        if (navMeshAgent.path.corners.Length != 0)
                        robot.FireAbility(robot, navMeshAgent.path.corners[navMeshAgent.path.corners.Length - 1]);
                    }
                }
            }
            else
            {
                //get back into the fight faster
                aiController.Aggression = 1.0f;
            }

        }
    }

    private void UpdateSamples(float updateRate, Robot robot)
    {
        if (robot != null)
        {
            // Update layer
            for (int i = 1; i <= coverSampleLayers; i++)
            {
                // Update radius
                for (int j = 0; j < coverSamples.Length / i; j++)
                {
                    float angle = (Mathf.Deg2Rad * -robot.transform.rotation.eulerAngles.y) + (j * Mathf.PI * 2f / coverSamples.Length * i);
                    Vector3 newPos = robot.transform.position + new Vector3(Mathf.Cos(angle) * coverSampleRadius / i, 0, Mathf.Sin(angle) * coverSampleRadius / i);
                    coverSamples[j * i] = newPos;
                }
            }
            UpdateSampleTimer.StartTimer(coverUpdateRate);
        }
    }

    private void FindCover(Robot robot)
    {
        NavMeshHit hit = new NavMeshHit();
        List<Vector3> candidates = new List<Vector3>();

        // Test all samples and populate distances
        for (int i = 0; i < coverSamples.Length; i++)
        {
            // Find nearest wall to sample position 
            if (NavMesh.FindClosestEdge(coverSamples[i], out hit, NavMesh.GetAreaFromName("Not Walkable")))
            {
                // test normal direction against enemy location (avg enemy location)
                if (Vector3.Dot(hit.normal, (Target.transform.position - robot.transform.position)) < -coverQuality)
                {
                    Debug.DrawLine(robot.transform.position, hit.position, Color.yellow);
                    candidates.Add(hit.position);
                }
            }
        }

        if (candidates.Count == 0)
        {
            Debug.Log("NO CANDIDATES!");
            //Debug.Break();

            return;
        }

        // Determine shortest path to cover
        for (int i = 0; i < candidates.Count; i++)
        {
            NavMeshPath path = new NavMeshPath();

            // If path invalid retry
            if (!navMeshAgent.CalculatePath(candidates[i], path))
            {
                continue;
            }

            // Remove candidates that would move us closer to the target (with variance of 5%)
            if (MinimumTargetDistance(path) < Vector3.Distance(robot.transform.position, Target.transform.position))// && candidates.Count - 1 > 0)
            {
                candidates.RemoveAt(i);
            }
        }

        // Sort by path length
        candidates = candidates.OrderBy(x => Vector3.Distance(x, robot.transform.position)).ToList();

        // Remove candidates if they are in view of the target
        for (int i = 0; i < candidates.Count(); i++)
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(candidates[i], Target.transform.position - candidates[i], out hitInfo, Vector3.Distance(Target.transform.position, candidates[i])) && candidates.Count - 1 > 0)
            {
                candidates.RemoveAt(i);
            }
        }

        if (candidates.Count() == 0)
        {
            Debug.Log("NO CANDIDATES!");
            Debug.Break();
            return;
        }

        // Move to cover!
        //m_FindingCover = true;
        robot.MoveToPoint(candidates[0]);
    }

    public float MinimumTargetDistance(NavMeshPath path)
    {
        float minimumDistance = 0.0f;

        for (int i = 0; i < path.corners.Length; i++)
        {
            if (i + 1 > path.corners.Length - 1)
            {
                return minimumDistance;
            }

            if (Vector3.Distance(path.corners[i], Target.transform.position) < Vector3.Distance(path.corners[i + 1], Target.transform.position))
            {
                minimumDistance = Vector3.Distance(path.corners[i], Target.transform.position);
            }
        }

        return minimumDistance;
    }

    private void TargetRandomRobot(Robot robot)
    {
        Dictionary<int, Robot>.ValueCollection test = RobotRegistry.data.Values;
        Robot[] array = new Robot[test.Count];
        test.CopyTo(array, 0);
        int range = Random.Range(0, array.Length);
        if (array[range] == robot)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    if (array[i] != robot)
                    {
                        range = i;
                        break;
                    }
                }
            }
        }

        robot.SetAutoTarget(array[range]);
        Target = array[range].GetGameObject();
    }

    void SetTarget(Robot target, Robot self)
    {
        if (target != null)
        {
            self.SetAutoTarget(target);
            Target = target.GetGameObject();
        }
    }

    void FindTargets()
    {
        visibleTargets = senseComp.visibleTargets;
    }

    public bool IsTargetVisible(Robot robot, float range = Mathf.Infinity)
    {
        #region Checks If Target Is Within Line Of Sight
        bool isVisible = false;

        RaycastHit hit = new RaycastHit();
        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        if (Physics.Raycast(robot.transform.position, Target.transform.position - robot.transform.position, out hit, 20.0f, layerMask))
        {
            if (hit.distance < range)
            {
                if (hit.transform.gameObject == Target /*&& Vector3.Angle(m_Target.transform.forward, m_Target.transform.position - transform.position) <= 60 / 2*/)
                {
                    isVisible = true;
                    //Debug.Log("AI can see you");
                }
                else
                {
                    isVisible = false;
                    //Debug.Log("AI can't see you");
                }
            }
            else
            {
                isVisible = false;
            }

        }
        return isVisible;
        #endregion
    }
}
