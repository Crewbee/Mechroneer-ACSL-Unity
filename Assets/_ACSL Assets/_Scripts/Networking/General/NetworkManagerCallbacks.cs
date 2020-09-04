using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


[RequireComponent(typeof(NetworkManager))]
public class NetworkManagerCallbacks : MonoBehaviour, IConnectionCallbacks, ILobbyCallbacks, IMatchmakingCallbacks
{
    public bool joinLobbyOnConnect = true;
    public List<RoomInfo> cachedRoomLists { get; private set; }
    public List<TypedLobbyInfo> cachedLobbyInfo { get; private set; }
    public string errorMessage { get; private set; }
#if DEBUG
    public bool showDebugMessages = false;
#endif

    private void Awake()
    {
    }
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    #region IConnectionCallbacks
    public void OnConnected()
    {
        Log("OnConnected()");
    }

    public void OnConnectedToMaster()
    {
        Log("OnConnectedToMaster()");
        if (joinLobbyOnConnect)
            NetworkManager._instance.JoinLobby();
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        Log("OnDisconnected() cause: " + cause);
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        Log("OnRegionListReceived()");
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        Log("OnCustomAuthenticationResponse()");
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        Log("OnCustomAuthenticationFailed(): " + debugMessage);
    }
    #endregion
    #region ILobbyCallbacks
    public void OnJoinedLobby()
    {
        Log("OnJoinedLobby()");
    }

    public void OnLeftLobby()
    {
        Log("OnLeftLobby()");
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Log("OnRoomListUpdate()");
        cachedRoomLists = roomList;
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Log("OnLobbyStatisticsUpdate()");
        cachedLobbyInfo = lobbyStatistics;
    }
    #endregion
    #region IMatchmakingCallbacks

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        Log("OnFriendListUpdate()");
    }

    public void OnCreatedRoom()
    {
        Log("OnCreatedRoom()");
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        Log("OnCreateRoomFailed() code: " + returnCode + "\n" + message);
    }

    public void OnJoinedRoom()
    {
        Log("OnJoinedRoom()");
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        Log("OnJoinRoomFailed() code: " + returnCode + "\n" + message);
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Log("OnJoinRandomFailed() code: " + returnCode + "\n" + message);
    }

    public void OnLeftRoom()
    {
        Log("OnLeftRoom()");
    }
    #endregion


    private void Log(object message)
    {
#if DEBUG
        if (showDebugMessages)
        {
            Debug.Log(message);
            //DebugText.AddMessage(message.ToString(), 3);
        }
#endif
    }
}
