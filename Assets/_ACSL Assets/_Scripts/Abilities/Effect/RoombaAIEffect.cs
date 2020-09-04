using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum RoombaStates
{
    RS_WANDERING = 0,
    RS_SENSING,
    RS_SEEKING
}

public class RoombaAIEffect : EffectBase
{
    #region EffectBase Variables

    SomethingAbility m_abilityData;
    EDRoombaAI m_baseData;

    #endregion

    #region RoombaVariables

    RoombaStates m_currentState;
    RoombaStates m_previousState;

    GameObject m_roomba;
    Robot m_owner;

    NavMeshAgent m_agent;
    NavMeshPath m_path;
    Vector3 m_navMeshSurfaceSize;

    Vector3 m_currentDirection;
    Quaternion m_lookDirection;

    float m_range;
    float m_speed;
    int m_pathIndex;

    #endregion

    #region Targets

    List<Robot> m_targets;
    Transform m_roombaTarget;

    #endregion

    public RoombaAIEffect(IEffectUser affectedObject, IEffectUser target, Vector3 mousepos, EDRoombaAI basedata, SomethingAbility abilitydata) : base(affectedObject)
    {

        #region Initalize Base Variables

        m_baseData = basedata;
        m_abilityData = abilitydata;
        #endregion

        #region Initalize Roomba Variables

        m_roomba = m_affectedObject.GetGameObject();
        m_owner = target.GetGameObject().GetComponent<Robot>();

        m_roomba.AddComponent<NavMeshAgent>();
        m_agent = m_roomba.GetComponent<NavMeshAgent>();
        m_path = new NavMeshPath();

        m_currentDirection = Vector3.zero;
        m_lookDirection = Quaternion.identity;

        m_range = basedata.RoombaDetectionRange;
        m_speed = basedata.RoombaSpeed;

        NavMeshSurface surface = GameObject.FindObjectOfType<NavMeshSurface>();// BattleSceneManager.instance._Level.GetComponent<NavMeshSurface>();
        m_navMeshSurfaceSize = surface.size;
        m_navMeshSurfaceSize = new Vector3(m_navMeshSurfaceSize.x * 2.5f, 0.0f, m_navMeshSurfaceSize.z * 2.0f);

        m_pathIndex = 1;
        #endregion

        #region Initalize Target Variables

        m_targets = new List<Robot>();
        m_roombaTarget = null;

        #endregion

        FindEnemyRobots();

        CheckForEnemiesInRange();

        #region Set Initial State
        if (m_roombaTarget == null)
        {
            m_currentState = RoombaStates.RS_WANDERING;
            m_previousState = RoombaStates.RS_SENSING;
            m_agent.CalculatePath(abilitydata.direction * m_range, m_path);
        }
        else
        {
            m_currentState = RoombaStates.RS_SEEKING;
            m_previousState = RoombaStates.RS_SENSING;
            m_agent.CalculatePath(m_roombaTarget.position, m_path);
        }
        #endregion

    }

    // Update is called once per frame
    public override void Update()
    {

        switch (m_currentState)
        {

            case RoombaStates.RS_WANDERING:
                if (m_path.corners.Length > m_pathIndex) //if there are still path nodes
                {
                    #region Follow Path to Random Position on Map
                    if (m_roomba.transform.position != m_path.corners[m_pathIndex]) //if the roomba has not reached the next node
                    {
                        m_currentDirection = m_path.corners[m_pathIndex] - m_roomba.transform.position; //current forward is toward next node
                        m_currentDirection.Normalize();//normalize for unit vector
                        m_lookDirection = Quaternion.LookRotation(m_currentDirection);//create look rotation quaternion from unit vector
                        m_roomba.transform.rotation = m_lookDirection; //set roomba rotation to created quaternion
                        m_roomba.transform.position = m_roomba.transform.position + m_currentDirection.normalized * m_speed * Time.deltaTime;//update roomba's position to the next frames position

                        float DistanceToNode = Vector3.Distance(m_roomba.transform.position, m_path.corners[m_pathIndex]);

                        if (DistanceToNode < 1.0f)
                        {
                            m_pathIndex++;
                        }

                    }
                    #endregion
                }
                else //if there are no path nodes
                {
                    #region Wandering Path is Complete
                    m_previousState = RoombaStates.RS_WANDERING;
                    #endregion
                }

                #region Current State is Sensing Go To Sensing State
                m_currentState = RoombaStates.RS_SENSING;
                goto case RoombaStates.RS_SENSING;
                #endregion


            case RoombaStates.RS_SENSING:

                CheckForEnemiesInRange();

                if (m_roombaTarget == null) //if the target transform is still null
                {
                    //if the wandering path was completed
                    #region Reset Path To Random Position Set State To Wandering
                    if (m_previousState != RoombaStates.RS_SENSING)
                    {
                        m_pathIndex = 1;
                        m_agent.CalculatePath(FindRandomPosition(), m_path);
                        m_currentState = RoombaStates.RS_WANDERING;
                        m_previousState = RoombaStates.RS_SENSING;
                    }
                    #endregion

                    //otherwise
                    #region Do Not Reset Path Set State To Wandering
                    else if (m_previousState == RoombaStates.RS_SENSING)
                    {
                        m_currentState = RoombaStates.RS_WANDERING;
                    }
                    #endregion
                }
                else //if a target was found
                {
                    //Regardless of the previous path or state
                    #region Reset Path To Target Position Set State To Seeking
                        m_pathIndex = 1;
                        m_agent.CalculatePath(m_roombaTarget.position, m_path);
                        m_currentState = RoombaStates.RS_SEEKING;
                        m_previousState = RoombaStates.RS_SENSING;
              
                    #endregion
                }

                break;

            case RoombaStates.RS_SEEKING:

                if (m_path.corners.Length > m_pathIndex) //if there are still path nodes
                {
                    #region Follow Path to Target Position
                    if (m_roomba.transform.position != m_path.corners[m_pathIndex]) //if the roomba has not reached the next node
                    {
                        m_currentDirection = m_path.corners[m_pathIndex] - m_roomba.transform.position; //current forward is toward next node
                        m_currentDirection.Normalize();//normalize for unit vector
                        m_lookDirection = Quaternion.LookRotation(m_currentDirection);//create look rotation quaternion from unit vector
                        m_roomba.transform.rotation = m_lookDirection; //set roomba rotation to created quaternion
                        m_roomba.transform.position = m_roomba.transform.position + m_currentDirection.normalized * m_speed * Time.deltaTime;//update roomba's position to the next frames position

                        float DistanceToNode = Vector3.Distance(m_roomba.transform.position, m_path.corners[m_pathIndex]);

                        if (DistanceToNode < 1.0f)
                        {
                            m_pathIndex++;
                        }

                    }
                    #endregion
                }
                else //if there are no path nodes
                {
                    #region Seeking Path is Complete
                    m_previousState = RoombaStates.RS_SEEKING;
                    #endregion
                }

                #region Current State is Sensing Go To Sensing State
                m_currentState = RoombaStates.RS_SENSING;
                goto case RoombaStates.RS_SENSING;
                #endregion
        }
    }

    public override void FixedUpdate()
    {

    }

    void FindEnemyRobots()
    {
        #region Find All Registed Robot in Game
        Dictionary<int, Robot>.ValueCollection RegisteredRobots = RobotRegistry.data.Values;
        Robot[] array = new Robot[RegisteredRobots.Count];
        RegisteredRobots.CopyTo(array, 0);
        #endregion

        #region Add all enemies to Target array
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null && array[i] != m_owner)
            {
                m_targets.Add(array[i]);
            }
        }
        #endregion
    }

    void CheckForEnemiesInRange()
    {
        #region Get The Transform Of The Closest Enemy Within Range
        float minDistance = m_range; //set min distance to max range
        m_roombaTarget = null; //reset the target transform

        if (m_targets.Count > 0)
        {
            foreach (Robot enemy in m_targets) //for each enemy
            {
                float Dist = Vector3.Distance(m_roomba.transform.position, enemy.transform.position); // check distance

                if (Dist < minDistance) //if distance is less then current closest distance to enemy
                {
                    minDistance = Dist; //set closest distance to checked distance
                    m_roombaTarget = enemy.transform; //set target to current closest enemy
                }
            }
        }
        #endregion
    }

    Vector3 FindRandomPosition()
    {
        #region Returns A Random Position Within Range On Nav Mesh Surface
        float RandomX = Random.Range(-m_navMeshSurfaceSize.x, m_navMeshSurfaceSize.x);
        float RandomZ = Random.Range(-m_navMeshSurfaceSize.z, m_navMeshSurfaceSize.z);

        Vector3 RandomPosition = new Vector3(RandomX, 0.0f, RandomZ);

        Vector3 RandomeDirection = new Vector3(m_roomba.transform.position.x, 0.0f, m_roomba.transform.position.z);

        RandomeDirection = RandomeDirection - RandomPosition;
        RandomeDirection.Normalize();

        return RandomeDirection * m_range;
        #endregion
    }
}
