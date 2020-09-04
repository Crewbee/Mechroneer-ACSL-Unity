//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class EnemyAIController : AIController
//{

//    //Stores max health to be used for getting health percent

//    #region PUBLIC_VARIABLES
//    public MechroneerPlayer OwningPlayer; // Define this at runtime
//    #endregion


//    public override void Init(OldPlayer player)
//    {
//        if (player != null)
//        {
//            OwningPlayer = (player as MechroneerPlayer);
//            //currentBrainBehaviour = new EnemyAI();
//            //currentBrainBehaviour.Init(player as MechroneerPlayer);
//        }

//        if (OwningPlayer.currentRobot)
//        //foreach (Robot robot in OwningPlayer.activeRobots)
//        {
//            RobotOld robot = OwningPlayer.currentRobot;
//            robot.robotStates.TryGetValue("Search", out robot.CurrentBrainBehavior);
//            //SetRandomTarget(robot);
//        }
//    }



//    public override void Update()
//    {
//        if (OwningPlayer.currentRobot)
//        //foreach (Robot robot in OwningPlayer.activeRobots)
//        {
//            RobotOld robot = OwningPlayer.currentRobot;
//            {
//                //Auto attack, happens whenever main target is in range
//                robot.TurnToFaceTarget();

//                if (robot.target != null)
//                robot.ArmAbility();
//                //Aggression calculated regardless of control, used to decide upon actions once AI is given control
//                robot.CalculateCurrentAggression();
                

//                //Doesn't continue if you are in control of the robot; is completely ignored for AI
//                if (robot != OwningPlayer.currentRobot || OwningPlayer.playerID <= -1)
//                {

//                    //Update current AI behavior
//                    if (robot.CurrentBrainBehavior != null && robot != null)
//                    {
//                        //SetRandomTarget(robot);
//                        robot.CurrentBrainBehavior.Update();
//                    }

//                    //Consider robot heuristics and change states depending on circumstances
//                    //More heuristics such as amount of robot density in one area, range, 
//                    //and skill range will be included later on.
//                    if (robot.Aggression > 0.0f)
//                    {
//                        robot.robotStates.TryGetValue("Search", out robot.CurrentBrainBehavior);
//                    }
//                    else
//                    {
//                        robot.robotStates.TryGetValue("Cover", out robot.CurrentBrainBehavior);
//                    }
//                }
//            }
//        }
//    }

//}
