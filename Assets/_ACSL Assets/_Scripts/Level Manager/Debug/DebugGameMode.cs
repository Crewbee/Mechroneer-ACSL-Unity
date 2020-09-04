using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class DebugGameMode : MechroneerGameMode
{
    public override void EndGame()
    {
        base.EndGame();
    }

    //public override void SpawnPlayers()
    //{
    //    Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
    //    if (!LobbySettings.GetIsOnline())
    //    {
    //        foreach (var player in m_robotSelectPlayers)
    //        {
    //            Robot robot = PhotonNetwork.Instantiate("RobotBase", GetSpawnPosition(), Quaternion.identity).GetComponent<Robot>();
    //            robot.Init(this, player.lobbyMainPlayer.ID, player.lobbyMainPlayer.team, player.robotToUse);
    //            if (player.lobbyMainPlayer.ID == PhotonNetwork.LocalPlayer.ActorNumber)
    //            {
    //                Controller controller = Instantiate(Resources.Load<Controller>("Debug Controller"));
    //                controller.PossessPlayer(robot);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        foreach (var player in m_robotSelectPlayers)
    //        {
    //            if (player.lobbyMainPlayer.ID != PhotonNetwork.LocalPlayer.ActorNumber)
    //                continue;

    //            if (player.lobbyMainPlayer.isSpectator)
    //            {
    //            }
    //            else
    //            {
    //                Robot robot = PhotonNetwork.Instantiate("RobotBase", GetSpawnPosition(), Quaternion.identity).GetComponent<Robot>();
    //                robot.Init(this, player.lobbyMainPlayer.ID, player.lobbyMainPlayer.team, player.robotToUse);
    //            }
    //            ServerPlayerInitialized();

    //            break;
    //        }
    //    }
    //}

    public override void StartGame()
    {
        base.StartGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
