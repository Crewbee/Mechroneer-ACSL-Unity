using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    protected Player m_player { get; private set; }
    protected Controller m_controller { get; private set; }
    protected GameMode m_gameMode { get; private set; }
    protected GameState m_gameState { get; private set; }
    virtual public void Init(GameMode gameMode, GameState gameState, Controller controller)
    {
        m_gameMode = gameMode;
        m_gameState = gameState;
        m_controller = controller;
    }

    virtual public void PossessPlayer(Player player)
    {
        m_player = player;
    }
}
