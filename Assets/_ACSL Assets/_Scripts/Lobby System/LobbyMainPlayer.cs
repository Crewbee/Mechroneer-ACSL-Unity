using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobbyMainPlayer : MonoBehaviourPun
{
    public static int UNDEFINED_ID = -1;

    public string playerName {get; private set; }
    public int ID { get; private set; }
    public int team { get; private set; }
    public bool placedArena;
    public bool isSpectator { get; private set; }

    public int teamValue;
    public void Awake()
    {
        isSpectator = false;
    }

    [PunRPC]
    public void RegisterPlayer(int ID, string name, int team, bool spectator)
    {
        if (PhotonNetwork.IsConnectedAndReady)
            photonView.RPC("RegisterPlayer", RpcTarget.OthersBuffered, ID, name, team, spectator);
        this.ID = ID;
        playerName = name;
        this.team = team;
        isSpectator = spectator;
    }

    [PunRPC]
    public void SetIsSpectator(bool spectator)
    {
        if (PhotonNetwork.IsConnectedAndReady)
            photonView.RPC("SetIsSpectator", RpcTarget.OthersBuffered, spectator);
        isSpectator = spectator;
    }

}
