using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyFinder : UIPanel, Photon.Realtime.IMatchmakingCallbacks, Photon.Realtime.ILobbyCallbacks
{
    public LobbyMaker lobbyMaker;

    public RoomList roomListPrefab;
    public RectTransform content;
    private string m_selectedRoom;

    public UIManager battleSubPanelUIManager;
    public UIManager inLobbyPanelUIManager;
    public UIPanel inLobbyPanel;
    public UIPanel mainLobbyPanel;

    protected virtual void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    protected virtual void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnPopped()
    {
        base.OnPopped();
        lobbyMaker.isOnline = false;

        NetworkManager._instance.LeaveLobby();
    }

    public override void OnPoppedTo()
    {
        base.OnPoppedTo();
        NetworkManager._instance.JoinLobby();
    }
    public override void OnPushed()
    {
        base.OnPushed();
        lobbyMaker.isOnline = true;
        NetworkManager._instance.JoinLobby();
    }
    public void OnJoinedRoom()
    {
        battleSubPanelUIManager.Push(inLobbyPanel);
        inLobbyPanelUIManager.Push(mainLobbyPanel);
    }

    public override void OnPushedOnTop()
    {
        base.OnPushedOnTop();
    }
    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        DestroyChildren();
        SpawnRoomList(roomList);
    }
    public void JoinGame()
    {
        NetworkManager._instance.JoinRoom(m_selectedRoom);
    }

    public void CreateGame()
    {
        battleSubPanelUIManager.Push(inLobbyPanel);
    }

    void DestroyChildren()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
    void SpawnRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.PlayerCount == info.MaxPlayers)
                continue;
            if (info.IsOpen == false)
                continue;
            RoomList room = Instantiate<RoomList>(roomListPrefab, content);
            Button button = room.GetComponent<Button>();
            button.onClick.AddListener(() => { m_selectedRoom = info.Name; });

            room.roomName.text = info.Name;
            room.mapName.text = MapListSpawner.GetSceneName(((int)info.CustomProperties[LobbySettings.mapIDKey]));
            room.gameMode.text = (string)info.CustomProperties[LobbySettings.gameModeKey];
            room.playerCount.text = info.PlayerCount.ToString() + "/" + info.MaxPlayers.ToString();
        }
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnCreatedRoom()
    {
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
    }

    public void OnLeftRoom()
    {
    }

    public void OnJoinedLobby()
    {
    }

    public void OnLeftLobby()
    {
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
    }
}
