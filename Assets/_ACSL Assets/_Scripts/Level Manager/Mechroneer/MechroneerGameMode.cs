using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class MechroneerGameMode : GameMode
{
    [SerializeField]
    protected bool m_isTeamMode = false;
    public bool isTeamGameMode { get => m_isTeamMode; }

    protected LobbyRobotSelectPlayer[] m_robotSelectPlayers { get; private set; }
    SpawnPoint[] m_spawnPoints;
  
    public List<Controller> controllers { get; protected set; }
    public float countdownStartTime = 5.0f;

    MechroneerUI m_mechroneerUI;
    private int m_playerInitializedCount;

    private bool m_hasRequestedRematch;
    private int m_rematchRequestedCount;

    protected MechroneerGameModeData m_gameModeData;

    virtual public void Init(LevelManager levelManager, MechroneerGameModeData gameModeData)
    {
        m_robotSelectPlayers = FindObjectsOfType<LobbyRobotSelectPlayer>();
        m_spawnPoints = FindObjectsOfType<SpawnPoint>();
        m_gameModeData = gameModeData;

        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;

        if (!PhotonNetwork.InRoom)
        {
            Init(levelManager, m_gameModeData.gameState, m_gameModeData.robot);
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            this.levelManager = levelManager;
            gameState = PhotonNetwork.InstantiateSceneObject(m_gameModeData.gameState.name, Vector3.zero, Quaternion.identity).GetComponent<GameState>();
            gameState.Init(this);
            this.playerPrefab = gameModeData.robot;
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            this.levelManager = levelManager;
            this.gameState = FindObjectOfType<MechroneerGameState>();
            gameState.Init(this);
            this.playerPrefab = gameModeData.robot;
        }
        //if (LobbySettings.GetIsOnlineMatch())
        //{
            if (RobotRegistry.data == null)
                RobotRegistry.data = new Dictionary<int, Robot>();
        //}

        controllers = new List<Controller>();
        localController = Instantiate(m_gameModeData.controller);
        m_mechroneerUI = Instantiate(m_gameModeData.UI) as MechroneerUI;
        m_mechroneerUI.Init(this, gameState, localController);
        m_playerInitializedCount = 0;

        m_hasRequestedRematch = false;
        m_rematchRequestedCount = 0;

        SpawnControllers();

        //if (PhotonNetwork.IsMasterClient)
        //    PhotonView.Get(this).RPC("ClientInit", RpcTarget.Others);

        if (!PhotonNetwork.InRoom || !LobbySettings.GetIsOnlineMatch())
            StartGame();
        else
        {
            ServerPlayerInitialized();
        }
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        if (arg0.name == "GarageScene")
            return;
        SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
        if (!PhotonNetwork.InRoom)
        {
            foreach (var player in this.m_robotSelectPlayers)
            {
                Destroy(player.gameObject);
            }
            m_robotSelectPlayers = null;
        }
        else
        {
            RobotRegistry.data = null;
        }
    }

    void SpawnControllers()
    {
        foreach (var player in m_robotSelectPlayers)
        {
            Controller controller = null;
            if (!PhotonNetwork.InRoom || !LobbySettings.GetIsOnlineMatch())
            {
                if (player.playerID == 1)
                {
                    controller = localController;
                }
                else if (player.playerID < 0)
                {
                    controller = Instantiate(m_gameModeData.aiController);
                }
                else
                    continue;
            }
            else if (LobbySettings.GetIsOnlineMatch())
            {
                if (player.playerID == PhotonNetwork.LocalPlayer.ActorNumber)
                    controller = localController;
                else
                    continue;
            }
            controllers.Add(controller);

            SpawnPlayer(player, controller);
            if (LobbySettings.GetIsOnlineMatch())
                break;
        }
    }

    Robot SpawnPlayer(LobbyRobotSelectPlayer player, Controller controller)
    {
        if (PhotonNetwork.InRoom)
        {
            if (player.isSpectator)
            {
                if (player.playerID == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    Debug.Log("Possosing Null");
                    m_mechroneerUI.PossessPlayer(null);
                }
                return null;
            }
            else
            {
                Robot robot = PhotonNetwork.Instantiate(playerPrefab.name, GetSpawnPosition(), Quaternion.identity).GetComponent<Robot>();
                robot.Init(player.playerID, player.teamID, player.playerName, player.robotToUse);
                if (controller)
                    controller.PossessPlayer(robot);
                if (player.playerID == PhotonNetwork.LocalPlayer.ActorNumber)
                    m_mechroneerUI.PossessPlayer(robot);
                return robot;
            }
        }
        else
        {
            if (player.isSpectator)
            {
                if (player.playerID == 1)
                {
                    Debug.Log("Possosing Null");
                    m_mechroneerUI.PossessPlayer(null);
                }
                return null;
            }
            else
            {
                Robot robot = Instantiate(playerPrefab, GetSpawnPosition(), Quaternion.identity) as Robot;
                robot.Init(player.playerID, player.teamID, player.playerName, player.robotToUse);
                if (controller)
                    controller.PossessPlayer(robot);
                if (player.playerID == 1)
                    m_mechroneerUI.PossessPlayer(robot);
                return robot;
            }
        }
    }

    protected Vector3 GetSpawnPosition()
    {
        if (PhotonNetwork.InRoom && LobbySettings.GetIsOnlineMatch())
        {
            return m_spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].spawnPoint;
        }
        else
        {
            SpawnPoint randomPoint = null;
            foreach (SpawnPoint spawnPoint in m_spawnPoints)
            {
                randomPoint = spawnPoint;
                if (!DetectRobotInVacinity(randomPoint))
                    return randomPoint.spawnPoint;
            }
            return m_spawnPoints[Random.Range(0, m_spawnPoints.Length)].spawnPoint;
        }
    }

    private bool DetectRobotInVacinity(SpawnPoint spawnPoint)
    {
        RaycastHit[] hits = Physics.SphereCastAll(spawnPoint.transform.position, 2, Vector3.forward, 0.01f, (1 << LayerMask.NameToLayer("Robot")));

        if (hits.Length > 0)
        {
            //Debug.Log("Robot Detected at: " + spawnPoint.name);
            return true;
        }
        //Debug.Log("There are no robots at: " + spawnPoint.name);
        return false;
    }

    [PunRPC]
    protected void PlayerInitialized()
    {
        m_playerInitializedCount++;
        if (m_playerInitializedCount == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            m_playerInitializedCount = 0;
            PhotonView.Get(this).RPC("StartGame", RpcTarget.Others);
            StartGame();
        }
    }
    private void ServerPlayerInitialized()
    {
        PhotonView.Get(this).RPC("PlayerInitialized", RpcTarget.MasterClient);
    }

    [PunRPC]
    public override void StartGame()
    {
        List<Robot> robots = new List<Robot>();
        robots.AddRange(FindObjectsOfType<Robot>());
        (gameState as MechroneerGameState).InitPlayers(robots);

        foreach (var robot in robots)
        {
            robot.onRobotDeath += Robot_onRobotDeath;
        }

        if (LobbySettings.GetIsOnlineMatch())
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonView.Get(this).RPC("StartTimerHUD", RpcTarget.Others, countdownStartTime, PhotonNetwork.Time);
                m_mechroneerUI.SwitchToTimerHUD(countdownStartTime, m_mechroneerUI.SwitchToPlayerHUD);
            }
        }
        else
        {
            m_mechroneerUI.SwitchToTimerHUD(countdownStartTime, m_mechroneerUI.SwitchToPlayerHUD);
        }

        base.StartGame();
    }

    [PunRPC]
    public void StartTimerHUD(float time, double photonTime)
    {
        float delay = (float)(PhotonNetwork.Time - photonTime);
        m_mechroneerUI.SwitchToTimerHUD(time - delay, m_mechroneerUI.SwitchToPlayerHUD);
    }

    private void Robot_onRobotDeath(Robot caller)
    {
        (levelManager as MechroneerLevelManager).OnRobotDeath();
    }

    public override void EndGame()
    {
        bool win = false;
        if (localController.possessedPlayer != null)
            win = localController.possessedPlayer is Robot;
        m_mechroneerUI.SwitchToWinLoseHUD(win);
        //if (PhotonNetwork.InRoom)
        //{
        //}
        //else
        //    m_mechroneerUI.SwitchToWinLoseHUD(localController.possessedPlayer.GetGameObject() == (gameState as MechroneerGameState).players[0].gameObject);
        //Debug.Log((gameState as MechroneerGameState).players[0].name);
        //Debug.Log(localController.possessedPlayer.GetGameObject().name);
        base.EndGame();
    }
    [PunRPC]
    public void Rematch()
    {
        if (LobbySettings.GetIsOnlineMatch())
        {
            m_hasRequestedRematch = false;
        }
        int controllerCount = 0;
        foreach (var player in m_robotSelectPlayers)
        {
            Controller controller = controllers[controllerCount];
            if (LobbySettings.GetIsOnlineMatch())
            {
                if (player.playerID == PhotonNetwork.LocalPlayer.ActorNumber)
                    controller = localController;
            }
            else
            {
                if (player.playerID == 1)
                    controller = localController;
                else if (controller == localController && player.playerID != 1)
                    controller = controllers[++controllerCount];
                controllerCount++;
            }

            if (controller.possessedPlayer != null)
            {
                if (controller.possessedPlayer is Robot)
                {
                    if (LobbySettings.GetIsOnlineMatch())
                    {
                        if (player.playerID == PhotonNetwork.LocalPlayer.ActorNumber)
                            PhotonNetwork.Destroy(controller.possessedPlayer.GetGameObject());

                    }
                    else
                    {
                        if (PhotonNetwork.InRoom)
                            PhotonNetwork.Destroy(controller.possessedPlayer.GetGameObject());
                        else
                            DestroyImmediate(controller.possessedPlayer.GetGameObject());

                    }
                }
            }

            if (LobbySettings.GetIsOnlineMatch())
            {
                if (player.playerID == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    SpawnPlayer(player, controller);
                    break;
                }
            }
            else
                SpawnPlayer(player, controller);

        }

        if (LobbySettings.GetIsOnlineMatch())
        {
            ServerPlayerInitialized();
        }
        else
            StartGame();
    }


    public void RequestRematch()
    {
        if (!PhotonNetwork.InRoom || !LobbySettings.GetIsOnlineMatch())
            Rematch();
        else
        {
            if (m_hasRequestedRematch)
                return;
            m_hasRequestedRematch = true;
            PhotonView.Get(this).RPC("IncreaseRematchRequestCount", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    protected void IncreaseRematchRequestCount()
    {
        m_rematchRequestedCount++;
        if (m_rematchRequestedCount == PhotonNetwork.CurrentRoom.PlayerCount - GetSpecatorCount())
        {
            m_rematchRequestedCount = 0;
            PhotonView.Get(this).RPC("Rematch", RpcTarget.Others);
            Rematch();
        }
    }

    int GetSpecatorCount()
    {
        int spectatorCount = 0;
        foreach (var player in m_robotSelectPlayers)
        {
            if (!player)
                continue;
            if (player.isSpectator)
                spectatorCount++;
        }

        return spectatorCount;
    }
}
