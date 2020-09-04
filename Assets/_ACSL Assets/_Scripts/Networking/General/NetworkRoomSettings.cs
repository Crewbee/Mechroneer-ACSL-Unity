using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;

[CreateAssetMenu(menuName = "ACSL Networking/Network Room Settings")]
public class NetworkRoomSettings : ScriptableObject
{
    public bool isVisible = true;
    public bool isOpen = true;
    public bool publishUserID = true;
    public byte maxPlayers = 0;
    public int playerTTL = 0;
    public int emptyRoomTTL = 0;
    public string roomName;
    public Hashtable customRoomProperties = new Hashtable();
    public string[] customRoomPropertiesForLobby;
}
