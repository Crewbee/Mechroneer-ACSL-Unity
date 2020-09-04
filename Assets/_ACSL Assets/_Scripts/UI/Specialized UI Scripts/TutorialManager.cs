//using UnityEngine;
//using UnityEngine.UI;
//using System;
////using Photon.Pun;

//TODO: REMAKE LATER

//public class TutorialManager : MonoBehaviour //MonoBehaviourPunCallbacks
//{
//    bool m_FirstLaunch;
//    bool m_TutorialCompleted;
//    public int MapID;
//    public Canvas tutorialCanvas;
//    public GameObject[] popUps;
//    public UIManager tutorialStack;

//    void LoadPrefs()
//    {
//        m_FirstLaunch = Convert.ToBoolean(PlayerPrefs.GetInt("firstLaunch"));
//        m_TutorialCompleted = Convert.ToBoolean(PlayerPrefs.GetInt("tutorialCompleted"));
//    }

//    public override void OnJoinedRoom()
//    {
//        //tutorialStack.Push(1);
//    }

//    void Start()
//    {
//        LoadPrefs();

//        // If first launch start the tutorial
//        if (m_FirstLaunch)
//        {
//            StartTutorial();
//            PlayerPrefs.SetInt("firstLaunch", Convert.ToInt32(false));
//            PlayerPrefs.Save();
//        }
//    }

//    public void StartTutorial()
//    {
//        // Activate tutorial canvas
//        ///tutorialCanvas.gameObject.SetActive(true);

            
//            NetworkRoomSettings roomSettings = NetworkManager.CreateRoomSettings();

//            roomSettings.isOpen = false;
//            roomSettings.isVisible = false;

//            roomSettings.roomName = "Tutorial" + UnityEngine.Random.Range(0,1000);
//            roomSettings.maxPlayers = 1;

//            roomSettings.customRoomProperties.Add(LobbySettings.gameModeKey, "TutorialGameMode");
//            roomSettings.customRoomProperties.Add(LobbySettings.mapIDKey, MapID);
//            roomSettings.customRoomProperties.Add(LobbySettings.robotCountKey, 1);
//            roomSettings.customRoomProperties.Add(LobbySettings.isOnlineKey, false);

//            roomSettings.customRoomPropertiesForLobby = new string[3];
//            roomSettings.customRoomPropertiesForLobby[0] = LobbySettings.gameModeKey;
//            roomSettings.customRoomPropertiesForLobby[1] = LobbySettings.mapIDKey;
//            roomSettings.customRoomPropertiesForLobby[2] = LobbySettings.robotCountKey;

//            NetworkManager._instance.CreateRoom(roomSettings);
//    }

//    public void EndTutorial()
//    {
//        PlayerPrefs.SetInt("tutorialCompleted", Convert.ToInt32(false));
//        PlayerPrefs.Save();
//    }
//}
