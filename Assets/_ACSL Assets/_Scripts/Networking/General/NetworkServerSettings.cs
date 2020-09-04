using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;


[System.Serializable]
public class NetworkServerSettings
{
    public string server;
    public ConnectionProtocol protocol = ConnectionProtocol.Udp;
    public bool enableLobbyStatistics;
}
