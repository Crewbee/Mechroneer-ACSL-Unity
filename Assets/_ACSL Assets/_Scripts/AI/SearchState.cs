using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : AIState
{
    #region SENSE_VARIABLES
    public LayerMask targetLayerMask;
    public LayerMask obfuscationLayerMask;
    [Space(5)]
    [Min(0)]
    public float visionDelay = 0.2f;
    public float visionRadius = 30f;
    [Range(0, 360)]
    public float visionAngle = 360f;

    public List<Transform> visibleTargets = new List<Transform>();
    #endregion

    #region PRIVATE_VARIABLES
    private Vector3[] coverSamples;
    [SerializeField]
    private MyTimer DashTimer;
    private MyTimer findTargetTimer;
    private MyTimer altSpecial;
    private bool alt = true;
    private GameObject Target;

    private EnergyComponent energyComp;
    private SenseComponent senseComp;
    private MechroneerAIController aiController;

    private float leftArmRange;
    private float rightArmRange;

    RobotPart leftArm;
    RobotPart rightArm;
    RobotPart leg;
    #endregion

    // Start is called before the first frame update

    public SearchState()
    {
        DashTimer = new MyTimer();
        findTargetTimer = new MyTimer();
        altSpecial = new MyTimer();
    }

    public override void Init(Robot robot, MechroneerAIController controller)
    {
        targetLayerMask = LayerMask.NameToLayer("Robot");
        obfuscationLayerMask = LayerMask.NameToLayer("Default");

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

        DashTimer.StartTimer(Random.Range(0, 3.0f));
        findTargetTimer.StartTimer(0.2f);
        altSpecial.StartTimer(2.0f);
        robot.robotParts.TryGetValue(RobotPartType.LeftArm, out leftArm);
        robot.robotParts.TryGetValue(RobotPartType.RightArm, out rightArm);
        robot.robotParts.TryGetValue(RobotPartType.Leg, out leg);
        leftArmRange = ((RobotArm)leftArm).autoAttackData.range.y - 2.0f;
        rightArmRange = ((RobotArm)rightArm).autoAttackData.range.y - 2.0f;
    }

    // Update is called once per frame
    public override void Update(Robot robot, MechroneerController.IActions player)
    {
        DashTimer.Update();
        findTargetTimer.Update();
        altSpecial.Update();
        ////Update the robot's target in case it changed
        //Target = robot.target.GetGameObject();

        //Determine how close we should get
        if (Target != null)
        {

            float magnitude = 0;

            if (leftArmRange < rightArmRange)
            {
                magnitude = leftArmRange;
            }
            else
            {
                magnitude = rightArmRange;
            }

            if (altSpecial.timeLeftSeconds <= 0.0f)
            {
                altSpecial.StartTimer(2.0f);
            }

            //Get the point we need and move there
            Vector3 dir = Vector3.Normalize(Target.transform.position - robot.transform.position);
            Vector3 point = Target.transform.position - (dir * magnitude);

            if (Vector3.Distance(point, robot.transform.position) > magnitude)
            {
                robot.MoveToPoint(point);
            }
            else
            {
                if (alt)
                {
                    point = robot.transform.right + robot.transform.forward * 5.0f + point;
                }
                else
                {
                    point = -robot.transform.right + robot.transform.forward * 5.0f + point;
                }
                robot.MoveToPoint(point);
            }

            if (IsTargetVisible(robot))
            {
                if (altSpecial.timeLeftSeconds > 1.0f)
                {
                    alt = true;
                }
                else
                {
                    alt = false;
                }

                if (alt)
                {
                    if (leftArm.specialAbilityTimer != null)
                    {
                        if (leftArm.specialAbilityTimer.timeLeftSeconds <= 0.0f)
                            player.SelectAbility1();
                        robot.FireAbility(Target.GetComponent<Robot>(), Target.transform.position);
                    }
                }
                else
                {
                    if (rightArm.specialAbilityTimer != null)
                    {
                        if (rightArm.specialAbilityTimer.timeLeftSeconds <= 0.0f)
                            player.SelectAbility2();
                        robot.FireAbility(Target.GetComponent<Robot>(), Target.transform.position);
                    }
                }
            }

            if (!IsTargetVisible(robot) && energyComp.currentValue > 150 && DashTimer.timeLeft <= 0.0f)
            {
                if (leg.specialAbilityTimer.timeLeft <= 0.0f)
                {
                    player.SelectAbility4();
                    robot.FireAbility(robot, Target.transform.position);
                    DashTimer.StartTimer(2.0f);
                }
            }

        }
        if (findTargetTimer.timeLeft <= 0.0f)
        {
            FindTargets();
            if (visibleTargets.Count != 0)
            {
                if (visibleTargets[0] != robot)
                {
                    if (visibleTargets[0])
                        SetTarget(visibleTargets[0].GetComponent<Robot>(), robot);
                }
            }
            else if (Target == null)
            {
                TargetRandomRobot(robot);
            }
            findTargetTimer.StartTimer(0.2f);
        }
        if (energyComp.currentValue < 100.0f)
        {
            aiController.Aggression -= 0.15f * Time.deltaTime;
        }
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
