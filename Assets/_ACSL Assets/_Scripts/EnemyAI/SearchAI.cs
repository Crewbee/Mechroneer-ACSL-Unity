//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.AI;

//public class SearchAI : AI
//{
//    //AI behavior that hunts out other robots to use their auto attacks on them.
//    //Primarily focuses on lone single targets if their range is low but will
//    //fire into groups of enemies if their range is high. Will hunt a single
//    //user defined target if it exists.

//    //Uses dashes to close in distance, but more frequently if low range or
//    //not in sight.

//    #region SENSE_VARIABLES
//    public LayerMask targetLayerMask;
//    public LayerMask obfuscationLayerMask;
//    [Space(5)]
//    [Min(0)]
//    public float visionDelay = 0.2f;
//    public float visionRadius = 30f;
//    [Range(0, 360)]
//    public float visionAngle = 360f;

//    public List<Transform> visibleTargets = new List<Transform>();
//    #endregion

//    #region PRIVATE_VARIABLES
//    private Vector3[] coverSamples;
//    [SerializeField]
//    private MyTimer DashTimer;
//    private MyTimer findTargetTimer;
//    private MyTimer altSpecial;
//    private bool alt = true;
//    private GameObject Target;

//    private EnergyComponent energyComp;
//    private SenseComponentOld senseComp;

//    private float leftArmRange;
//    private float rightArmRange;

//    RobotPart leftArm;
//    RobotPart rightArm;
//    RobotPart leg;
//    #endregion


//    public override void Init(RobotOld robot)
//    {
//        targetLayerMask = LayerMask.NameToLayer("Robot");
//        obfuscationLayerMask = LayerMask.NameToLayer("Default");

//        if (robot)
//        {
//            this.robot = robot;
//            energyComp = robot.energyComponent;
//            senseComp = robot.senseComponent;

//            if (robot.target == null)
//            {
//                Dictionary<int,RobotOld>.ValueCollection robots = RobotOld.registeredRobots.Values;

//                FindTargets();
//                if (visibleTargets.Count != 0)
//                {
//                    SetTarget(visibleTargets[0].GetComponent<RobotOld>());
//                }
//                else
//                {
//                    TargetRandomRobot();
//                }
//            }
//        }
//        DashTimer = new MyTimer();
//        findTargetTimer = new MyTimer();
//        altSpecial = new MyTimer();
//        DashTimer.StartTimer(Random.Range(0, 3.0f));
//        findTargetTimer.StartTimer(0.2f);
//        altSpecial.StartTimer(2.0f);
//        leftArm = robot.GetComponent<RobotOld>().GetRobotPart(RobotPartType.LeftArm);
//        rightArm = robot.GetComponent<RobotOld>().GetRobotPart(RobotPartType.RightArm);
//        leg = robot.GetComponent<RobotOld>().GetRobotPart(RobotPartType.Leg);
//        leftArmRange = ((RobotArm)leftArm).autoAttackData.range.y - 2.0f;
//        rightArmRange = ((RobotArm)rightArm).autoAttackData.range.y - 2.0f;
       
//    }

//    public override void Update()
//    {
//        DashTimer.Update();
//        findTargetTimer.Update();
//        altSpecial.Update();
//        ////Update the robot's target in case it changed
//        //Target = robot.target.GetGameObject();

//        //Determine how close we should get
//        if (Target != null)
//        {

//            float magnitude = 0;

//            if (leftArmRange < rightArmRange)
//            {
//                magnitude = leftArmRange;
//            }
//            else
//            {
//                magnitude = rightArmRange;
//            }

//            if (altSpecial.timeLeftSeconds <= 0.0f)
//            {
//                altSpecial.StartTimer(2.0f);
//            }

//            //Get the point we need and move there
//            Vector3 dir = Vector3.Normalize(Target.transform.position - robot.transform.position);
//            Vector3 point = Target.transform.position - (dir * magnitude);

//            //Debug.Log(Vector3.Distance(point, robot.transform.position) + ", " + magnitude);

//            if (Vector3.Distance(point, robot.transform.position) > magnitude)
//            {
//                robot.MoveToPoint(point);
//            }
//            else
//            {
//                if (alt)
//                {
//                    point = robot.transform.right + robot.transform.forward * 5.0f + point;
//                }
//                else
//                {
//                    point = -robot.transform.right + robot.transform.forward * 5.0f + point;
//                }
//                robot.MoveToPoint(point);
//            }

//            //.AimAbility(RobotPartType.LeftArm);

//            if (robot.IsTargetVisible())
//            {
//                if (altSpecial.timeLeftSeconds > 1.0f)
//                {
//                    alt = true;
//                }
//                else
//                {
//                    alt = false;
//                }

//                if (alt)
//                {
//                    if (leftArm.specialAbilityTimer != null)
//                    {
//                        if (leftArm.specialAbilityTimer.timeLeftSeconds <= 0.0f)
//                            robot.FireAbility(RobotPartType.LeftArm, Target.GetComponent<RobotOld>(), Target.transform.position);
//                    }
//                }
//                else
//                {
//                    if (rightArm.specialAbilityTimer != null)
//                    {
//                        if (rightArm.specialAbilityTimer.timeLeftSeconds <= 0.0f)
//                            robot.FireAbility(RobotPartType.RightArm, Target.GetComponent<RobotOld>(), Target.transform.position);
//                    }
//                }
//            }
//            //}
//            //else
//            //{
//            //    //Leave this state and enter the Combat state
//            //    //robot.robotStates.TryGetValue("Combat", out robot.CurrentBrainBehavior);
//            //}

//            if (!robot.IsTargetVisible() && energyComp.energy > 150 && DashTimer.timeLeft <= 0.0f)
//            {
//                if (leg.specialAbilityTimer.timeLeft <= 0.0f)
//                {
//                    robot.FireAbility(RobotPartType.Leg, robot, Vector3.zero);
//                    DashTimer.StartTimer(2.0f);
//                }
//            }

//        }
//            if (findTargetTimer.timeLeft <= 0.0f)
//            {
//                FindTargets();
//                if (visibleTargets.Count != 0)
//                {
//                    if (visibleTargets[0] != robot)
//                    {
//                        if (visibleTargets[0])
//                        SetTarget(visibleTargets[0].GetComponent<RobotOld>());
//                    }
//                }
//                else if (Target == null)
//                {
//                    TargetRandomRobot();
//                }
//                findTargetTimer.StartTimer(0.2f);
//            }
//        if (energyComp.energy < 100.0f)
//        {
//            robot.Aggression -= 0.05f * Time.deltaTime;
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

//    void SetTarget(RobotOld target)
//    {
//        if (target != null)
//        {
//            robot.SetRobotTarget(target);
//            Target = target.GetGameObject();
//        }
//    }

//    void FindTargets()
//    {
//        visibleTargets = senseComp.visibleTargets;
//    }
//}
