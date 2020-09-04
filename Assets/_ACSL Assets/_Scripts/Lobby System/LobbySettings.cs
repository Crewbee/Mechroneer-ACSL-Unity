using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Hashtable = ExitGames.Client.Photon.Hashtable;
public static class LobbySettings
{
    public static string gameModeKey = "GameMode";
    public static string mapIDKey = "Map";
    public static string isOnlineKey = "OnlineKey";

    static MechroneerGameModeData m_gameModeData;
    static int m_mapID;

    public static void SetNewGameMode(MechroneerGameModeData newGameMode)
    {
        m_gameModeData = newGameMode;
        if (PhotonNetwork.InRoom)
            UpdateProperties(gameModeKey, newGameMode.name);
    }
    public static void SetNewMap(int newMapID)
    {
        m_mapID = newMapID;
        if (PhotonNetwork.InRoom)
            UpdateProperties(mapIDKey, newMapID);
    }

    public static MechroneerGameModeData GetGameMode()
    {
        if (PhotonNetwork.InRoom)
            return Resources.Load<MechroneerGameModeData>((string)(PhotonNetwork.CurrentRoom.CustomProperties[gameModeKey]));
        else
            return m_gameModeData;
    }
    public static int GetMapID()
    {
        if (PhotonNetwork.InRoom)
            return (int)PhotonNetwork.CurrentRoom.CustomProperties[mapIDKey];
        else
            return m_mapID;
    }
    public static bool GetIsOnlineMatch()
    {
        if (PhotonNetwork.InRoom)
            return (bool)PhotonNetwork.CurrentRoom.CustomProperties[isOnlineKey];
        else
            return false;
    }

    private static void UpdateProperties(string key, object updatedValue)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Hashtable updatedTable = PhotonNetwork.CurrentRoom.CustomProperties;
            updatedTable[key] = updatedValue;
            PhotonNetwork.CurrentRoom.SetCustomProperties(updatedTable);
        }
    }
}

