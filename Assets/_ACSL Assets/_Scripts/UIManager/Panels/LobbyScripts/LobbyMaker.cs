using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyMaker : UIPanel, Photon.Realtime.IMatchmakingCallbacks
{
    public bool isOnline = false;

    public string lobbyName;
    public MechroneerGameModeData startingGameMode;
    public int startingMapID;

    public UIManager inLobbyUIManager;
    public UIPanel mainLobbyPanel;

    public int maximumLobbyNameCharacterCount = 24;
    public int minimumLobbyNameCharacterCount = 1;

    public WarningBox lengthWarning;
    public WarningBox generalWarning;

    public delegate void LobbyNameChanged(string lobbyName);
    public event LobbyNameChanged OnLobbyNameChanged;

    public delegate void GameModeChanged(MechroneerGameModeData gameMode);
    public event GameModeChanged OnGameModeChanged;

    public delegate void MapIDChanged(int mapID);
    public event MapIDChanged OnMapIDChanged;

    virtual protected void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    virtual protected void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    } 
    public void MakeLobby()
    {
        // Check room name length
        if (lobbyName.Length < minimumLobbyNameCharacterCount || lobbyName.Length > maximumLobbyNameCharacterCount)
        {
            lengthWarning.Display();
            return;
        }

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            LobbySettings.SetNewGameMode(startingGameMode);
            LobbySettings.SetNewMap(startingMapID);

            inLobbyUIManager.Push(mainLobbyPanel);
        }
        else
        {
            NetworkRoomSettings roomSettings = NetworkManager.CreateRoomSettings();

            if (!isOnline)
            {
                roomSettings.isOpen = false;
                roomSettings.isVisible = false;
            }
            else
            {
                roomSettings.isOpen = true;
                roomSettings.isVisible = true;
            }

            roomSettings.roomName = lobbyName;
            roomSettings.maxPlayers = 8;

            roomSettings.customRoomProperties.Add(LobbySettings.gameModeKey, startingGameMode.name);
            roomSettings.customRoomProperties.Add(LobbySettings.mapIDKey, startingMapID);
            roomSettings.customRoomProperties.Add(LobbySettings.isOnlineKey, isOnline);

            roomSettings.customRoomPropertiesForLobby = new string[2];
            roomSettings.customRoomPropertiesForLobby[0] = LobbySettings.gameModeKey;
            roomSettings.customRoomPropertiesForLobby[1] = LobbySettings.mapIDKey;

            NetworkManager._instance.CreateRoom(roomSettings);
        }
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        generalWarning.ChangeText(message);
        generalWarning.Display();
    }

    public void OnJoinedRoom()
    {
        inLobbyUIManager.Push(mainLobbyPanel);
    }

    public override void OnPushed()
    {
        base.OnPushed();
        GetComponentInChildren<UISubPanel>(true).SetSubPanelActive(true);
    }

    #region Unused Photon Callbacks
    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnCreatedRoom()
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
    #endregion

    public string SetLobbyName { set { lobbyName = value; OnLobbyNameChanged?.Invoke(value); } }
    public int SetMapID { set { startingMapID = value; OnMapIDChanged?.Invoke(value); } }
    public MechroneerGameModeData SetGameMode { set { startingGameMode = value; OnGameModeChanged?.Invoke(value); } }
}
