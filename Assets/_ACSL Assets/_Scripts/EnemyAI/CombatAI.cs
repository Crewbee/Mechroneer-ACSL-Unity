//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.AI;

//public class CombatAI : AI
//{
//    //AI behavior that hunts out other robots to use their auto attacks on them.
//    //Primarily focuses on lone single targets if their range is low but will
//    //fire into groups of enemies if their range is high. Will hunt a single
//    //user defined target if it exists.

//    //Uses dashes to close in distance, but more frequently if low range or
//    //not in sight.

//    #region PRIVATE_VARIABLES
//    //private bool strafeAlt = true;
//    [SerializeField]
//    private MyTimer strafeTimer;
//    private GameObject Target;

//    private EnergyComponent energyComp;

//    private float leftArmRange;
//    private float rightArmRange;
//    #endregion

//    public override void Init(RobotOld robot)
//    {
//        if (robot)
//        {
//            this.robot = robot;
//            energyComp = robot.energyComponent;

//            if (robot.target == null)
//            {
//                Dictionary<int, RobotOld>.ValueCollection robots = RobotOld.registeredRobots.Values;

//                Target = robots.ElementAt<RobotOld>(0).GetGameObject();
//                //robot.SkillShotTarget = Target;
//                robot.SetRobotTarget(robots.ElementAt<RobotOld>(0));
//            }
//        }
//        strafeTimer = new MyTimer();
//        strafeTimer.StartTimer(0.5f);
//        leftArmRange = robot.GetComponent<RobotOld>().GetRobotPart(RobotPartType.LeftArm).abilityData.range.y;
//        rightArmRange = robot.GetComponent<RobotOld>().GetRobotPart(RobotPartType.RightArm).abilityData.range.y;
//    }

//    public override void Update()
//    {
//        strafeTimer.Update();

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


//            //Get the point we need and move there
//            Vector3 dir = Vector3.Normalize(Target.transform.position - robot.transform.position);
//            Vector3 point = Target.transform.position - (dir * magnitude);

//            //Debug.Log(Vector3.Distance(point, robot.transform.position) + ", " + magnitude);

//            if (Vector3.Distance(point, robot.transform.position) > magnitude)
//            {
//                robot.MoveToPoint(point);
//            }
//        }
//    }

//}
