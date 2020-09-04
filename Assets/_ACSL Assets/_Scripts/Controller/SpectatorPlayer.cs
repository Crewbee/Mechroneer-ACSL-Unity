//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////using Photon.Pun;
//using TMPro;

//TODO: REMAKE THIS

//[RequireComponent(typeof(MechroneerPlayer))]
//public class SpectatorPlayer : MonoBehaviour //MonoBehaviourPun
//{
//    MechroneerPlayer m_controllingPlayer;
//    MechroneerDriver m_driver;
//    [SerializeField]
//    MechroneerPlayer m_playerBeingSpectated;
//    [SerializeField]
//    RobotOld m_currentRobotBeingSpectated;

//    TextMeshProUGUI m_text;
//    // Start is called before the first frame update
//    void Start()
//    {
//        m_controllingPlayer = GetComponent<MechroneerPlayer>();
//        m_driver = FindObjectOfType<MechroneerDriver>();
//        m_text = BattleSceneManager.instance.spectatorText;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!photonView.IsMine)
//            return;
        

//        if (m_controllingPlayer.playerID != PhotonNetwork.LocalPlayer.ActorNumber)
//            return;
        

//        if (m_controllingPlayer.currentRobot != null)
//            return;
        
//        MouseKeyPlayerController controller = m_controllingPlayer.playerController as MouseKeyPlayerController;


//        if (BattleSceneManager.instance.gameMode.gameState.matchState == OldGameState.MatchState.Ended)
//            return;
        
//        if (controller.LeftArmAbility())
//            SpectateNextPlayer();
//        if (controller.RightArmAbility())
//            SpectatePreviousPlayer();
//        //if (controller.BodyAbility())
//        //    SpectateNextRobot();
//        //if (controller.LegAbility())
//        //    SpectatePreviousRobot();
        
//    }

//    private void FixedUpdate()
//    {
//        if (!photonView.IsMine)
//            return;
//        if (m_controllingPlayer.playerID != PhotonNetwork.LocalPlayer.ActorNumber)
//            return;

//        //List<int> robotsToRemove = new List<int>();
//        //foreach (Robot robot in m_controllingPlayer.activeRobots)
//        //{
//        //    if (robot == null)
//        //        robotsToRemove.Add(m_controllingPlayer.activeRobots.IndexOf(robot));
//        //}

//        //foreach (int index in robotsToRemove)
//        //{
//        //    m_controllingPlayer.activeRobots.RemoveAt(index);
//        //}

//        if (m_controllingPlayer.currentRobot != null)
//            return;

//        if (BattleSceneManager.instance.gameMode.gameState.matchState == OldGameState.MatchState.Ended)
//            return;

//        if (m_playerBeingSpectated == null)
//            FindAPlayer();
//        if (m_currentRobotBeingSpectated == null)
//            FindARobot();
//        if (!HasActiveRobots(m_playerBeingSpectated))
//            FindAPlayer();


//    }

//    public void SpectateNextPlayer()
//    {
//        List<OldPlayer> tempPlayers = BattleSceneManager.instance.gameMode.gameState.playerArray;
//        List<MechroneerPlayer> mechroneerPlayers = new List<MechroneerPlayer>();

//        foreach (OldPlayer player in tempPlayers)
//        {
//            if (player is MechroneerPlayer)
//                mechroneerPlayers.Add(player as MechroneerPlayer);
//        }

//        int currentPlayerIndex = mechroneerPlayers.FindIndex(
//            (MechroneerPlayer other) => 
//            { if (other == m_controllingPlayer)
//                    return false;
//                else
//                    return other == m_playerBeingSpectated;
//            });

//        int nextPlayerIndex = currentPlayerIndex;
//        do
//        {
//            nextPlayerIndex = (nextPlayerIndex + 1) % mechroneerPlayers.Count;

//            if (nextPlayerIndex == currentPlayerIndex)
//                return;

//            m_playerBeingSpectated = mechroneerPlayers[nextPlayerIndex];

//            if (!HasActiveRobots(m_playerBeingSpectated))
//                m_playerBeingSpectated = null;


//        } while (m_playerBeingSpectated == null || m_playerBeingSpectated == m_controllingPlayer);

//        FindARobot();
//    }

//    public void SpectatePreviousPlayer()
//    {
//        List<OldPlayer> tempPlayers = BattleSceneManager.instance.gameMode.gameState.playerArray;
//        List<MechroneerPlayer> mechroneerPlayers = new List<MechroneerPlayer>();

//        foreach (OldPlayer player in tempPlayers)
//        {
//            if (player is MechroneerPlayer)
//                mechroneerPlayers.Add(player as MechroneerPlayer);
//        }

//        int currentPlayerIndex = mechroneerPlayers.FindIndex(
//            (MechroneerPlayer other) =>
//            {
//                if (other == m_controllingPlayer)
//                    return false;
//                else
//                    return other == m_playerBeingSpectated;
//            });

//        int nextPlayerIndex = currentPlayerIndex;
//        do
//        {
//            nextPlayerIndex = (nextPlayerIndex - 1);
//            if (nextPlayerIndex < 0)
//                nextPlayerIndex += mechroneerPlayers.Count;
//            if (nextPlayerIndex == currentPlayerIndex)
//                return;

//            m_playerBeingSpectated = mechroneerPlayers[nextPlayerIndex];
            
//            if (!HasActiveRobots(m_playerBeingSpectated))
//                m_playerBeingSpectated = null;

//        } while (m_playerBeingSpectated == null || m_playerBeingSpectated == m_controllingPlayer);

//        FindARobot();
//    }

//    //public void SpectateNextRobot()
//    //{
//    //    if (!m_playerBeingSpectated)
//    //        return;
//    //    int currentRobotIndex = m_playerBeingSpectated.activeRobots.FindIndex((Robot other) => { return m_currentRobotBeingSpectated == other; });
//    //    int nextRobotIndex = currentRobotIndex;
//    //    do
//    //    {
//    //        nextRobotIndex = (nextRobotIndex + 1) % m_playerBeingSpectated.activeRobots.Count;
//    //        m_currentRobotBeingSpectated = m_playerBeingSpectated.activeRobots[nextRobotIndex];

//    //        if (nextRobotIndex == currentRobotIndex)
//    //            return;
//    //    } while (m_currentRobotBeingSpectated == null);

//    //    m_driver.cameraCentroid.m_Targets.Clear();
//    //    m_driver.cameraCentroid.m_Targets.Add(m_currentRobotBeingSpectated.transform);

//    //}
//    //public void SpectatePreviousRobot()
//    //{
//    //    if (!m_playerBeingSpectated)
//    //        return;
//    //    int currentRobotIndex = m_playerBeingSpectated.activeRobots.FindIndex((Robot other) => { return m_currentRobotBeingSpectated == other; });

//    //    int nextRobotIndex = currentRobotIndex - 1;
//    //    do
//    //    {
//    //        nextRobotIndex = nextRobotIndex - 1;

//    //        if (nextRobotIndex < 0)
//    //            nextRobotIndex += m_playerBeingSpectated.activeRobots.Count;
            
//    //        m_currentRobotBeingSpectated = m_playerBeingSpectated.activeRobots[nextRobotIndex];

//    //        if (nextRobotIndex == currentRobotIndex)
//    //            return;
//    //    } while (m_currentRobotBeingSpectated == null);

//    //    m_driver.cameraCentroid.m_Targets.Clear();
//    //    m_driver.cameraCentroid.m_Targets.Add(m_currentRobotBeingSpectated.transform);
//    //}

//    //public void SpectateAllRobots()
//    //{
//    //    if (!m_playerBeingSpectated)
//    //        return;
//    //    m_driver.cameraCentroid.m_Targets.Clear();
//    //    foreach (Robot robot in m_playerBeingSpectated.activeRobots)
//    //    {
//    //        m_driver.cameraCentroid.m_Targets.Add(robot.transform);
//    //    }
//    //}

//    private void FindAPlayer()
//    {
//        List<OldPlayer> tempPlayers = BattleSceneManager.instance.gameMode.gameState.playerArray;
//        List<MechroneerPlayer> mechroneerPlayers = new List<MechroneerPlayer>();

//        foreach (OldPlayer player in tempPlayers)
//        {
//            if (player is MechroneerPlayer)
//                mechroneerPlayers.Add(player as MechroneerPlayer);
//        }

//        if (mechroneerPlayers.Count < 2)
//            return;

//        foreach (MechroneerPlayer players in mechroneerPlayers)
//        {
//            if (players == m_controllingPlayer)
//                continue;
//            if (!HasActiveRobots(players))
//                continue;

//            m_playerBeingSpectated = players;
//            break;
//        }
//        if (m_playerBeingSpectated == null)
//            return;
//        FindARobot();
//    }
//    private void FindARobot()
//    {

//        m_text.text = "SPECTATING:\n" + m_playerBeingSpectated.name;
//        //if (m_playerBeingSpectated.currentRobot)
//        m_currentRobotBeingSpectated = m_playerBeingSpectated.currentRobot;
//        //foreach (Robot robot in m_playerBeingSpectated.activeRobots)
//        //{
//        //    if (robot != null)
//        //    {
//        //        m_currentRobotBeingSpectated = robot;
//        //        break;
//        //    }
//        //}
//        if (m_currentRobotBeingSpectated)
//        {

//            m_driver.cameraCentroid.m_Targets.Clear();
//            m_driver.cameraCentroid.m_Targets.Add(m_currentRobotBeingSpectated.transform);
//        }
//    }

//    private bool HasActiveRobots(MechroneerPlayer player)
//    {
//        return player.currentRobot;
//        //int nextPlayerRobotCount = player.activeRobots.Count;
//        //int nullCount = 0;

//        //foreach (Robot robot in player.activeRobots)
//        //{
//        //    if (robot == null)
//        //        nullCount++;
//        //}

//        //if (nullCount < player.activeRobots.Count)
//        //    return true;

//        //return false;

//    }
//}
