//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////using Photon.Pun;
////using Photon.Realtime;

//TODO: Remake class
//public class TutorialGameMode : OldGameMode
//{
//    public TutorialGameMode(OldGameState gameState) : base(gameState) { }
//    int m_initializedPlayers = 0;
//    // Start is called before the first frame update
//    void Start()
//    {
//        //base.StartGame();

//        if (!PhotonNetwork.IsMasterClient)
//            BattleSceneManager.instance.gameMode = this;
//        SpawnPlayer();
//            //SpawnAI();
//            //(gameState as TestGameState).Init();
//            StartGame();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //if (gameState.matchState == GameState.MatchState.Ended)
//        //{
//        //    var state = (gameState as TestGameState);
//        //    if (state.winnerID == PhotonNetwork.LocalPlayer.ActorNumber)
//        //    {
//        //        // Win
//        //        UIManager.Instance.Push(1);
//        //    }
//        //    else
//        //    {
//        //        // Lose
//        //        UIManager.Instance.Push(2);
//        //    }
//        //}
//    }

//    void SpawnPlayer()
//    {
//        LobbyRobotSelectPlayer lobbyPlayer = GameObject.FindObjectOfType<LobbyRobotSelectPlayer>();
//        SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();

//            GameObject spawnedPlayerObject = PhotonNetwork.Instantiate("MechroneerPlayer", Vector3.zero, Quaternion.identity);
//            MechroneerPlayer spawnedPlayer = spawnedPlayerObject.GetComponent<MechroneerPlayer>();
//            spawnedPlayer.playerID = 0;
//            SpawnPlayerRobots(lobbyPlayer, spawnPoints, spawnedPlayer);

//            MouseKeyPlayerController playerController = new MouseKeyPlayerController();
//            spawnedPlayer.controller = playerController;
//            spawnedPlayer.Init();
//            spawnedPlayer.controller.Init(spawnedPlayer);

//            BattleSceneManager.instance.playerHUD.Init(spawnedPlayer);
//            PhotonNetwork.Destroy(lobbyPlayer.photonView);
//    }

//    //void SpawnAI()
//    //{
//    //    LobbyRobotSelectPlayer[] playerList = GameObject.FindObjectsOfType<LobbyRobotSelectPlayer>();
//    //    SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();

//    //    foreach (LobbyRobotSelectPlayer lobbyPlayer in playerList)
//    //    {
//    //        GameObject spawnedPlayerObject = PhotonNetwork.Instantiate("MechroneerPlayer", Vector3.zero, Quaternion.identity);
//    //        MechroneerPlayer spawnedPlayer = spawnedPlayerObject.GetComponent<MechroneerPlayer>();
//    //        spawnedPlayer.playerID = lobbyPlayer.lobbyMainPlayer.ID;
//    //        spawnedPlayer.MulticastSetTeam(lobbyPlayer.lobbyMainPlayer.team);

//    //        SpawnPlayerRobots(lobbyPlayer.GetComponent<LobbyRobotSelectPlayer>(), spawnPoints, spawnedPlayer);
//    //        spawnedPlayer.controller = new EnemyAIController();
//    //        spawnedPlayer.Init();
//    //        spawnedPlayer.controller.Init(spawnedPlayer);

//    //        //ServerPlayerInitialized();
//    //        PhotonNetwork.Destroy(lobbyPlayer.photonView);
//    //    }
//    //}
//    private void SpawnPlayerRobots(LobbyRobotSelectPlayer player, SpawnPoint[] spawnLocations, MechroneerPlayer mechroneerPlayer)
//    {
//        int robotCount = 0;

//        List<SpawnPoint> spawnPointList = new List<SpawnPoint>();
//        spawnPointList.AddRange(spawnLocations);

//        RobotData data = player.robotToUse;
//        {
//            //int spawnPoint = Random.Range(0, spawnLocations.Length);
//            SpawnPoint selectedSpawnPoint = SelectSpawnPoint(spawnPointList);

//            GameObject spawnedRobotBase = PhotonNetwork.Instantiate("RobotBase", selectedSpawnPoint.spawnPoint, Quaternion.identity);
//            RobotOld spawnedRobot = spawnedRobotBase.GetComponent<RobotOld>();
//            spawnedRobotBase.layer = LayerMask.NameToLayer("Robot"); // Should be done in the prefab

//            spawnedRobot.robotData = data;

//            if (LobbySettings.GetIsOnline())
//                spawnedRobot.BuildNetworked(PhotonNetwork.LocalPlayer.ActorNumber, robotCount);
//            else
//                spawnedRobot.BuildNetworked(player.lobbyMainPlayer.ID, robotCount);
//            spawnedRobot.Init();
//            robotCount++;

//            foreach (Transform child in spawnedRobot.transform)
//            {
//                child.gameObject.layer = LayerMask.NameToLayer("Robot");
//            }

//            spawnedRobot.controllingPlayer = mechroneerPlayer;
//            mechroneerPlayer.currentRobot = spawnedRobot;
//        }
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

//    override public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {

//    }
//}
