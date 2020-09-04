using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

[System.Serializable]
public class NetworkCloudSettings
{
    public string appIDRealtime = "7ef4b669-95bd-4554-bf9a-6e8503d1fada";
    public string appIDChat = "";
    public string appIDVoice = "";
    public string appVersion = "0.1";
    public string fixedRegion = "";
    public ConnectionProtocol protocol = ConnectionProtocol.Udp;
    public bool enableLobbyStatistics = false;
}
/*
 * The following are input regions for fixedRegion string
 * 
 * asia (Asia)
 * au   (Australia)
 * cae  (Canada, East)
 * cn   (Chinese Mainland)
 * eu   (Europe)
 * in   (India)
 * jp   (Japan)
 * ru   (Russia)
 * rue  (Russia, East)
 * sa   (South America)
 * kr   (South Korea)
 * us   (USA, East)
 * usw  (USA, West)
 * 
 * Use ; to separate each region for multiple region inputs
 * For more details see https://doc.photonengine.com/en-us/pun/current/connection-and-authentication/regions
 */
