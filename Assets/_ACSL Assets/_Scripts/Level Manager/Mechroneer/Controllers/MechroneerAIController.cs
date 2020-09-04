using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechroneerAIController : MechroneerController
{
    Robot m_robot;
    AIState m_currentState;
    Dictionary<string, AIState> m_states;
    public float Aggression;
    HealthComponent m_healthComponent;
    float m_lastDamage;

    private void Awake()
    {
        m_states = new Dictionary<string, AIState>();

        SearchState search = new SearchState();
        CoverState cover = new CoverState();

        m_states.Add("Search", search);
        m_states.Add("Cover", cover);
    }

    override public void PossessPlayer(Controller.IActions actions)
    {
        base.PossessPlayer(actions);

        if (actions is Robot)
        {
            m_robot = actions as Robot;
        }
        else
            return;


        m_states["Search"].Init(m_robot, this);
        m_states["Cover"].Init(m_robot, this);

        Aggression = 1.0f;
        m_lastDamage = 0.0f;
        m_healthComponent = m_robot.healthComponent;

        m_states.TryGetValue("Search", out m_currentState);
    }

    protected override void FixedUpdateController()
    {
        if (!m_cachedGameObject)
        {
            PossessPlayer(null);
            return;
        }


        IActions player = possessedPlayer as IActions;
        m_currentState.Update(m_robot, player);
    }

    protected override void UpdateController()
    {
        CalculateCurrentAggression();
        if (Aggression > 0.0f)
        {
            ChangeState("Search");
        }
        else
        {
            ChangeState("Cover");
        }
    }

    public void ChangeState(string stateName)
    {
        if (m_states.ContainsKey(stateName))
        {
            m_currentState = m_states[stateName];
        }
    }

    public void CalculateCurrentAggression() //calculate aggression level
    {
        float damage = m_lastDamage - m_healthComponent.currentValue;
        Aggression -= damage * 0.005f;
        m_lastDamage = m_healthComponent.currentValue;

        Aggression += (0.01f * m_healthComponent.percent + Random.Range(-0.05f, 0.05f)) * Time.fixedDeltaTime;
        Aggression = Mathf.Clamp(Aggression, -1.0f, 1.0f);
    }
}

public abstract class AIState
{
    protected MechroneerAIController m_controller;
    public abstract void Init(Robot robot, MechroneerAIController controller);
    public abstract void Update(Robot robot, MechroneerController.IActions player);
}