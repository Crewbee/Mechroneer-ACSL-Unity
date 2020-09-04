using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyMainPanel : UIPanel, IInRoomCallbacks
{
    public LobbyMainPlayer localPlayer;
    public List<LobbyMainPlayer> players;

    public UIManager inLobbyPanelUIManager;
    public UIManager battleSubPanelUIManager;
    public UIPanel robotSelectPanel;
    public RectTransform teamOnePlayerList;
    public RectTransform teamTwoPlayerList;
    public Text playerTextPrefab;
    public LobbyMainPlayer playerPrefab;

    int m_teamOneCount;
    int m_teamTwoCount;

    int m_spectatorCount;

    private bool m_playerCountChanged;
    private int m_aiID = -1;

    public bool aborted;

    protected virtual void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    protected virtual void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void FixedUpdate()
    {
        if (m_playerCountChanged)
        {
            RefreshPlayerList();
        }
    }


    public override void OnPopped()
    {
        base.OnPopped();
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            if (aborted)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    Destroy(players[i].gameObject);
                }
                players.Clear();
            }
        }
        else
        {
            if (aborted)
            {
                players.Clear();
                PhotonNetwork.LeaveRoom();
            }
        }
    }

    public override void OnPushed()
    {
        base.OnPushed();
        if (players == null)
            players = new List<LobbyMainPlayer>();
        aborted = true;
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            CountPlayers();
            SpawnLocalPlayer();
        }
        else
        {
            Invoke("CountPlayers", 0.016f);
            Invoke("SpawnLocalPlayer", 0.016f);
        }
    }

    void SpawnLocalPlayer()
    {
        if (PhotonNetwork.IsConnectedAndReady)
            localPlayer = CreatePlayer(PhotonNetwork.LocalPlayer.ActorNumber, PhotonNetwork.NickName, DeterminePlayerTeam(), m_spectatorCount < 2 && players.Count == 6);
        else
            localPlayer = CreatePlayer(1, "Mechadune", DeterminePlayerTeam(), m_spectatorCount < 2);
    }
    public void SwitchPlayerType()
    {
        Debug.Log("Switch player type");
        if (localPlayer.isSpectator == false)
        {
            if (m_spectatorCount == 2)
                return;
            localPlayer.SetIsSpectator(true);
            RefreshPlayerList();
            if (LobbySettings.GetIsOnlineMatch())
                PhotonView.Get(this).RPC("RefreshPlayerList", RpcTarget.Others);
        }
        else
        {
            if (players.Count - m_spectatorCount == 6)
                return;

            localPlayer.SetIsSpectator(false);
            RefreshPlayerList();
            if (LobbySettings.GetIsOnlineMatch())
                PhotonView.Get(this).RPC("RefreshPlayerList", RpcTarget.Others);
        }
    }

    public void SpawnAIPlayer()
    {
        if (LobbySettings.GetIsOnlineMatch())
            return;
        if (players.Count - m_spectatorCount >= 6)
            return;

        LobbyMainPlayer AIPlayer = CreatePlayer(m_aiID--, "AIPlayer" + Random.Range(0, 9999), DeterminePlayerTeam(), false);
    }
    public void RemoveAIPlayer()
    {
        if (LobbySettings.GetIsOnlineMatch())
            return;

        if (players.Count > 1)
        {
            Destroy(players[1].gameObject);
            players.RemoveAt(1);
            m_playerCountChanged = true;
        }
    }
    private LobbyMainPlayer CreatePlayer(int ID, string name, int team, bool spectator)
    {
        LobbyMainPlayer player;
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            player = Instantiate(playerPrefab);
        }
        else
        {
            player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<LobbyMainPlayer>();
        }
        player.RegisterPlayer(ID, name, team, spectator);
        players.Add(player);
        RefreshPlayerList();
        if (LobbySettings.GetIsOnlineMatch())
            PhotonView.Get(this).RPC("RefreshPlayerList", RpcTarget.Others);
        return player;
    }
    private int DeterminePlayerTeam()
    {
        if (m_teamTwoCount < m_teamOneCount)
            return 1;
        else
            return 0;
    }

    public void StartGame()
    {
        if (players.Count - m_spectatorCount == 1)
            return;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                inLobbyPanelUIManager.Push(robotSelectPanel);
                PhotonView.Get(this).RPC("ClientStartGame", RpcTarget.Others);
            }
        }
        else
        {
            inLobbyPanelUIManager.Push(robotSelectPanel);
        }
    }
    [PunRPC]
    void ClientStartGame()
    {
        inLobbyPanelUIManager.Push(robotSelectPanel);
    }

    public void LeaveGame()
    {
        aborted = true;
        battleSubPanelUIManager.Pop();
    }
    [PunRPC]
    public void RefreshPlayerList()
    {
        CountPlayers();
        DestroyPlayerText(teamOnePlayerList);
        DestroyPlayerText(teamTwoPlayerList);
        SpawnPlayerText();
        m_playerCountChanged = false;
    }
    private void CountPlayers()
    {
        m_teamOneCount = 0;
        m_teamTwoCount = 0;
        m_spectatorCount = 0;

        if (LobbySettings.GetIsOnlineMatch())
        {
            players.Clear();
            players.AddRange(FindObjectsOfType<LobbyMainPlayer>());
        }

        foreach (LobbyMainPlayer player in players)
        {
            if (player.team == 0)
                m_teamOneCount++;
            else
                m_teamTwoCount++;
            if (player.isSpectator)
                m_spectatorCount++;
        }
    }
    private void DestroyPlayerText(RectTransform playerList)
    {
        while (playerList.childCount > 0)
        {
            DestroyImmediate(playerList.GetChild(0).gameObject);
        }
    }
    private void SpawnPlayerText()
    {
        LobbyMainPlayer[] players = FindObjectsOfType<LobbyMainPlayer>();
        foreach (LobbyMainPlayer player in players)
        {
            if (player.team == LobbyMainPlayer.UNDEFINED_ID)
                continue;
            if (player.team == 0)
            {
                Text text = GameObject.Instantiate<Text>(playerTextPrefab, teamOnePlayerList);
                text.text = player.playerName;
            }
            else
            {
                Text text = GameObject.Instantiate<Text>(playerTextPrefab, teamTwoPlayerList);
                text.text = player.playerName;
            }
        }
    }

    public void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
    }

    public void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Invoke("RefreshPlayerList", 0.016f);
    }

    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
    }

    public void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
    }

    public void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
    }
}
