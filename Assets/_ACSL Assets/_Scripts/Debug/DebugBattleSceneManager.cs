//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////using Photon.Pun;

//TODO: Delete this
//public class DebugBattleSceneManager : MonoBehaviour //MonoBehaviourPunCallbacks
//{
//    public PlayerHUD playerHUD;
//    public RobotData playerRobot;
//    public RobotData aiRobot;
//    public override void OnConnected()
//    {
//        base.OnConnected();


//    }
//    public override void OnJoinedRoom()
//    {
//        base.OnJoinedRoom();

//        SpawnPlayer();
//        SpawnAI();
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        NetworkManager._instance.GoOffline();
//        NetworkRoomSettings roomSettings = NetworkManager.CreateRoomSettings();

//        //if (!lobbyInScene.isOnline)
//        //{
//        //    roomSettings.isOpen = false;
//        //    roomSettings.isVisible = false;
//        //}
//        //else
//        //{
//        //    roomSettings.isOpen = true;
//        //    roomSettings.isVisible = true;
//        //}

//        roomSettings.roomName = "fuck";
//        roomSettings.maxPlayers = 6;

//        roomSettings.customRoomProperties.Add(LobbySettings.gameModeKey, "");
//        roomSettings.customRoomProperties.Add(LobbySettings.mapIDKey, 0);
//        roomSettings.customRoomProperties.Add(LobbySettings.robotCountKey, 1);
//        roomSettings.customRoomProperties.Add(LobbySettings.isOnlineKey, false);

//        roomSettings.customRoomPropertiesForLobby = new string[3];
//        roomSettings.customRoomPropertiesForLobby[0] = LobbySettings.gameModeKey;
//        roomSettings.customRoomPropertiesForLobby[1] = LobbySettings.mapIDKey;
//        roomSettings.customRoomPropertiesForLobby[2] = LobbySettings.robotCountKey;

//        NetworkManager._instance.CreateRoom(roomSettings);
//        //SpawnPlayer();
//        //SpawnAI();
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//    void SpawnPlayer()
//    {
//        SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();

//            GameObject spawnedPlayerObject = PhotonNetwork.Instantiate("MechroneerPlayer", Vector3.zero, Quaternion.identity);
//            MechroneerPlayer spawnedPlayer = spawnedPlayerObject.GetComponent<MechroneerPlayer>();
//            spawnedPlayer.GetComponent<SpectatorPlayer>().enabled = false;
//            spawnedPlayer.MulticastSetPlayerID(1);
//            spawnedPlayer.MulticastSetTeam(1);

//            SpawnPlayerRobots(playerRobot, spawnPoints, spawnedPlayer);
//            MouseKeyPlayerController playerController = new MouseKeyPlayerController();
//            spawnedPlayer.controller = playerController;
//            spawnedPlayer.Init();
//            spawnedPlayer.controller.Init(spawnedPlayer);

//            playerHUD.Init(spawnedPlayer);

//    }

//    void SpawnAI()
//    {
//        SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();

//        GameObject spawnedPlayerObject = PhotonNetwork.Instantiate("MechroneerPlayer", Vector3.zero, Quaternion.identity);
//        MechroneerPlayer spawnedPlayer = spawnedPlayerObject.GetComponent<MechroneerPlayer>();

//        spawnedPlayer.GetComponent<SpectatorPlayer>().enabled = false;
//        spawnedPlayer.playerID = -1;
//        spawnedPlayer.MulticastSetTeam(2);

//        SpawnPlayerRobots(aiRobot, spawnPoints, spawnedPlayer);
//        //spawnedPlayer.controller = new EnemyAIController();
//        //spawnedPlayer.Init();
//        //spawnedPlayer.controller.Init(spawnedPlayer);

//    }
//    private void SpawnPlayerRobots(RobotData player, SpawnPoint[] spawnLocations, MechroneerPlayer mechroneerPlayer)
//    {
//        int robotCount = 0;

//        List<SpawnPoint> spawnPointList = new List<SpawnPoint>();
//        spawnPointList.AddRange(spawnLocations);

//        RobotData data = player;
        
//        //int spawnPoint = Random.Range(0, spawnLocations.Length);
//        SpawnPoint selectedSpawnPoint = SelectSpawnPoint(spawnPointList);

//        GameObject spawnedRobotBase = PhotonNetwork.Instantiate("RobotBase", selectedSpawnPoint.spawnPoint, Quaternion.identity);
//        RobotOld spawnedRobot = spawnedRobotBase.GetComponent<RobotOld>();
//        spawnedRobotBase.layer = LayerMask.NameToLayer("Robot"); // Should be done in the prefab

//        spawnedRobot.robotData = data;
//        spawnedRobot.Init();
//        //if (LobbySettings.GetIsOnline())
//        //    spawnedRobot.BuildNetworked(PhotonNetwork.LocalPlayer.ActorNumber, robotCount);
//        //else
//        //spawnedRobot.Build(data);
//        spawnedRobot.BuildNetworked(mechroneerPlayer.playerID, robotCount);
//        robotCount++;

//        foreach (Transform child in spawnedRobot.transform)
//        {
//            child.gameObject.layer = LayerMask.NameToLayer("Robot");
//        }

//        spawnedRobot.controllingPlayer = mechroneerPlayer;
//        mechroneerPlayer.currentRobot = spawnedRobot;
//    }

//    private SpawnPoint SelectSpawnPoint(List<SpawnPoint> spawnPoints)
//    {
//        SpawnPoint randomPoint = null;
//        while (spawnPoints.Count > 0)
//        {
//            randomPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
//            spawnPoints.Remove(randomPoint);
//            if (!DetectRobotInVacinity(randomPoint))
//                return randomPoint;
//        }
//        return randomPoint;
//    }

//    private bool DetectRobotInVacinity(SpawnPoint spawnPoint)
//    {

//        RaycastHit[] hits = Physics.SphereCastAll(spawnPoint.transform.position, 1, Vector3.forward, 0.01f, LayerMask.NameToLayer("Robot") ^ int.MaxValue);

//        if (hits.Length > 0)
//        {
//            return true;
//        }
//        return false;
//    }

//}
