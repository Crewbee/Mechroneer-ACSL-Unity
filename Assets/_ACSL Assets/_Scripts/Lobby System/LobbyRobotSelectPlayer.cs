using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(LobbyMainPlayer))]
public class LobbyRobotSelectPlayer : MonoBehaviourPun//MonoBehaviour 
{
    public bool lockedIn;
    public RobotData robotToUse;
    public LobbySelectRobotPanel selectRobotPanel;
    public int playerID { get => m_lobbyMainPlayer.ID; }
    public string playerName { get => m_lobbyMainPlayer.playerName; }
    public int teamID { get => m_lobbyMainPlayer.team; }
    public bool isSpectator { get => m_lobbyMainPlayer.isSpectator; }

    private LobbyMainPlayer m_lobbyMainPlayer;
    private void Awake()
    {
        m_lobbyMainPlayer = GetComponent<LobbyMainPlayer>();
    }
    public void Start()
    {
        lockedIn = false;
    }
    public void Init()
    {
        if (playerID < 0)
        {
            AutoSelectRobots();
        }
        if (isSpectator)
        {
            lockedIn = true;
            if (PhotonNetwork.IsConnectedAndReady)
                ServerLockIn();
            else
                selectRobotPanel.PlayerLockedIn();
        }
    }

    public void AutoSelectRobots()
    {
        if (robotToUse == null && !lockedIn)
            SelectRobot(UserData._instance.robots[Random.Range(0, UserData._instance.robots.Count)]);

        LockIn();
    }

    public void LockIn()
    {
        if (lockedIn)
            return;
        if (robotToUse == null)
            return;

        lockedIn = true;
        if (PhotonNetwork.IsConnectedAndReady)
            ServerLockIn();
        else
            selectRobotPanel.PlayerLockedIn();
    }

    [PunRPC]
    private void ServerLockIn()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            lockedIn = true;
            selectRobotPanel.PlayerLockedIn();
        }
        else
        {
            photonView.RPC("ServerLockIn", RpcTarget.MasterClient);
        }
    }

    public void SelectRobot(RobotData robot)
    {
        if (!lockedIn)
            robotToUse = robot;
    }
}
