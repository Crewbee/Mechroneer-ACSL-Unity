using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;


public class NetworkManager : MonoBehaviour
{
    public bool useCloudSettings = true;
    public bool startOffline = true;
    public bool autoSyncScenes;
    public NetworkCloudSettings cloudSettings;
    public NetworkServerSettings serverSettings;
    public NetworkManagerCallbacks callbacks { get; private set; }

    public static NetworkManager _instance { get; private set; }


    // Start is called before the first frame update
    void Awake()
    {

        NetworkManagerCallbacks findCallbacks;
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
            findCallbacks = GetComponent<NetworkManagerCallbacks>();
            if (findCallbacks)
                Destroy(findCallbacks.gameObject);
            return;
        }
        PhotonNetwork.AutomaticallySyncScene = autoSyncScenes;

        findCallbacks = GetComponent<NetworkManagerCallbacks>();
        if (findCallbacks)
            callbacks = findCallbacks;
        else
            callbacks = gameObject.AddComponent<NetworkManagerCallbacks>();

        AppSettings settings = new AppSettings();
        if (useCloudSettings)
        {
            settings.AppVersion = cloudSettings.appVersion;
            settings.AppIdRealtime = cloudSettings.appIDRealtime;
            settings.AppIdChat = cloudSettings.appIDChat;
            settings.AppIdVoice = cloudSettings.appIDVoice;
            settings.EnableLobbyStatistics = cloudSettings.enableLobbyStatistics;
            settings.Protocol = cloudSettings.protocol;
        }
        else
        {
            settings.Server = serverSettings.server;
            settings.Protocol = serverSettings.protocol;
            settings.EnableLobbyStatistics = serverSettings.enableLobbyStatistics;
        }
        PhotonNetwork.PhotonServerSettings.StartInOfflineMode = startOffline;
        PhotonNetwork.PhotonServerSettings.AppSettings = settings;


    }

    public void Connect()
    {
        StartCoroutine(CheckInternetConnection((isOnline) =>
        {
            if (isOnline)
            {
                PhotonNetwork.PhotonServerSettings.StartInOfflineMode = false;
                PhotonNetwork.OfflineMode = false;

                if (!PhotonNetwork.IsConnected)
                    PhotonNetwork.ConnectUsingSettings();
            }
        }));
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public void JoinLobby()
    {
        if (!PhotonNetwork.InRoom && !PhotonNetwork.OfflineMode && PhotonNetwork.IsConnectedAndReady)
            PhotonNetwork.JoinLobby();
    }

    public void LeaveLobby()
    {
        if (PhotonNetwork.InLobby && !PhotonNetwork.OfflineMode)
            PhotonNetwork.LeaveLobby();
    }

    public void CreateRoom(NetworkRoomSettings settings)
    {
        if (!PhotonNetwork.InRoom && (PhotonNetwork.Server != ServerConnection.GameServer || PhotonNetwork.OfflineMode))
        {
            if (settings == null)
            {
                Debug.LogError("Settings passed is null");
                return;
            }
            RoomOptions options = new RoomOptions();
            options.IsVisible = settings.isVisible;
            options.IsOpen = settings.isOpen;
            options.PublishUserId = settings.publishUserID;
            options.MaxPlayers = settings.maxPlayers;
            options.PlayerTtl = settings.playerTTL;
            options.EmptyRoomTtl = settings.emptyRoomTTL;
            options.CustomRoomProperties = settings.customRoomProperties;
            options.CustomRoomPropertiesForLobby = settings.customRoomPropertiesForLobby;
            PhotonNetwork.CreateRoom(settings.roomName, options, null);
        }
    }

    public void JoinOrCreateRoom(NetworkRoomSettings settings)
    {
        if (!PhotonNetwork.InRoom && (PhotonNetwork.Server != ServerConnection.GameServer || PhotonNetwork.OfflineMode))
        {
            if (settings == null)
            {
                Debug.LogError("Settings passed is null");
                return;
            }

            RoomOptions options = new RoomOptions();
            options.IsVisible = settings.isVisible;
            options.IsOpen = settings.isOpen;
            options.PublishUserId = settings.publishUserID;
            options.MaxPlayers = settings.maxPlayers;
            options.PlayerTtl = settings.playerTTL;
            options.EmptyRoomTtl = settings.emptyRoomTTL;

            PhotonNetwork.JoinOrCreateRoom(settings.roomName, options, null);
        }
    }

    public void JoinRoom(string roomName)
    {
        if (!PhotonNetwork.InRoom && (PhotonNetwork.Server != ServerConnection.GameServer || PhotonNetwork.OfflineMode))
            PhotonNetwork.JoinRoom(roomName);
    }

    public void JoinRandomRoom()
    {
        if (!PhotonNetwork.InRoom && (PhotonNetwork.Server != ServerConnection.GameServer || PhotonNetwork.OfflineMode))
            PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
    }

    public void GoOffline()
    {
        PhotonNetwork.OfflineMode = true;
    }

    static public NetworkRoomSettings CreateRoomSettings()
    {
        return ScriptableObject.CreateInstance<NetworkRoomSettings>();
    }

    public IEnumerator CheckInternetConnection(System.Action<bool> action)
    {
        const string webServer = "https://www.algonquincollege.com";

        bool result;
        using (var request = UnityWebRequest.Head(webServer))
        {
            request.timeout = 5;
            yield return request.SendWebRequest();
            result = !request.isNetworkError && !request.isHttpError && request.responseCode == 200;
        }
        action(result);
    }
}