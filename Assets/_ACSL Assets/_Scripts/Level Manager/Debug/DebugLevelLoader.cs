//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////using Photon.Pun;
////using Photon.Realtime;
////using ExitGames.Client.Photon;

//public class DebugLevelLoader : MechroneerLevelManager, IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks, ILobbyCallbacks, IWebRpcCallback
//{
//    public GameMode gameModePrefab;

//    protected override void Awake()
//    {

//    }
//    public virtual void OnConnected()
//    {

//    }

//    public virtual void OnJoinedRoom()
//    {
//        Init();
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

//        roomSettings.customRoomProperties.Add(LobbySettings.gameModeKey, gameModePrefab.name);
//        roomSettings.customRoomProperties.Add(LobbySettings.mapIDKey, 0);
//        roomSettings.customRoomProperties.Add(LobbySettings.robotCountKey, 1);
//        roomSettings.customRoomProperties.Add(LobbySettings.isOnlineKey, false);

//        roomSettings.customRoomPropertiesForLobby = new string[3];
//        roomSettings.customRoomPropertiesForLobby[0] = LobbySettings.gameModeKey;
//        roomSettings.customRoomPropertiesForLobby[1] = LobbySettings.mapIDKey;
//        roomSettings.customRoomPropertiesForLobby[2] = LobbySettings.robotCountKey;

//        NetworkManager._instance.CreateRoom(roomSettings);
//    }

//    protected virtual void OnEnable()
//    {
//        PhotonNetwork.AddCallbackTarget(this);
//    }
//    protected virtual void OnDisable()
//    {
//        PhotonNetwork.RemoveCallbackTarget(this);
//    }

//    virtual public void OnConnectedToMaster()
//    {
//    }

//    virtual public void OnCreatedRoom()
//    {
//    }

//    virtual public void OnCreateRoomFailed(short returnCode, string message)
//    {
//    }

//    virtual public void OnCustomAuthenticationFailed(string debugMessage)
//    {
//    }

//    virtual public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
//    {
//    }

//    virtual public void OnDisconnected(DisconnectCause cause)
//    {
//    }

//    virtual public void OnFriendListUpdate(List<FriendInfo> friendList)
//    {
//    }

//    virtual public void OnJoinedLobby()
//    {
//    }

//    virtual public void OnJoinRandomFailed(short returnCode, string message)
//    {
//    }

//    virtual public void OnJoinRoomFailed(short returnCode, string message)
//    {
//    }

//    virtual public void OnLeftLobby()
//    {
//    }

//    virtual public void OnLeftRoom()
//    {
//    }

//    virtual public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
//    {
//    }

//    virtual public void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
//    {
//    }

//    virtual public void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
//    {
//    }

//    virtual public void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
//    {
//    }

//    virtual public void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
//    {
//    }

//    virtual public void OnRegionListReceived(RegionHandler regionHandler)
//    {
//    }

//    virtual public void OnRoomListUpdate(List<RoomInfo> roomList)
//    {
//    }

//    virtual public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
//    {
//    }

//    virtual public void OnWebRpcResponse(OperationResponse response)
//    {
//    }
//}
