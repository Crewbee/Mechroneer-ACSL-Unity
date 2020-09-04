using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Match_State
{
    Pre_Game,
    In_Progress,
    Post_Game
}

public class GameState : MonoBehaviour
{
    public Match_State matchState { get; protected set; }
    protected GameMode gameMode { get; private set; }
    virtual public void Init(GameMode gameMode)
    {
        this.gameMode = gameMode;
        matchState = Match_State.Pre_Game;
    }

    virtual public void OnMatchStart()
    {
        matchState = Match_State.In_Progress;
    }

    virtual public void OnMatchEnd()
    {
        matchState = Match_State.Post_Game;
    }
}
