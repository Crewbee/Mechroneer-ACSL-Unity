//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////using Photon.Realtime;
////using Photon.Pun;
////using ExitGames.Client.Photon;

//public class UIPanelPunCallbacks : UIPanel, IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks, ILobbyCallbacks, IWebRpcCallback
//{
//    protected virtual void OnEnable()
//    {
//        PhotonNetwork.AddCallbackTarget(this);
//    }
//    protected virtual void OnDisable()
//    {
//        PhotonNetwork.RemoveCallbackTarget(this);
//    }
//    virtual public void OnConnected()
//    {
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

//    virtual public void OnJoinedRoom()
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
