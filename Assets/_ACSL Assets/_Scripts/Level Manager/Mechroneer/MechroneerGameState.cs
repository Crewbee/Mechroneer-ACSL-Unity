using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechroneerGameState : GameState, IPunObservable
{
    public List<Robot> players;// { get; protected set; }
    public override void Init(GameMode gameMode)
    {
        base.Init(gameMode);
    }

    virtual public void InitPlayers(List<Robot> players)
    {
        this.players = players;
        foreach (var player in this.players)
        {
            player.onRobotDeath += OnPlayerDies;
        }
    }


    public override void OnMatchEnd()
    {
            base.OnMatchEnd();
    }

    public override void OnMatchStart()
    {
            base.OnMatchStart();
    }

    virtual public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(matchState);
        }
        else if (stream.IsReading)
        {
            matchState = (Match_State)stream.ReceiveNext();
        }
    }

    virtual protected void OnPlayerDies(Robot caller)
    {
        players.Remove(caller);
    }

}
