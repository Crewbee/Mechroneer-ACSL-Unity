using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGameState : MechroneerGameState
{
    public override void Init(GameMode gameMode)
    {
        base.Init(gameMode);
    }

    public override void InitPlayers(List<Robot> players)
    {
        base.InitPlayers(players);
    }

    public override void OnMatchEnd()
    {
        base.OnMatchEnd();
    }

    public override void OnMatchStart()
    {
        base.OnMatchStart();
    }

    protected override void OnPlayerDies(Robot caller)
    {
        base.OnPlayerDies(caller);
    }
}
