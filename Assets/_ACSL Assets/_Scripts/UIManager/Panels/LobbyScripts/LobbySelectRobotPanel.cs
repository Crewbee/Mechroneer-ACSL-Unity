using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class LobbySelectRobotPanel : UIPanel, IInRoomCallbacks
{
    public UIManager battleSubPanelUIManager;
    public LobbyMainPanel lobbyMain;
    public LobbyRobotSelectPlayer localPlayer;

    public float selectRobotTime;
    public float startTime;
    int m_playersReady = 0;

    MyTimer m_timer;

    public Text timerText;
    public ChangeScene sceneChanger;
    bool m_allPlayersLocked;

    override protected void Awake()
    {
        base.Awake();
    }
    protected virtual void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    protected virtual void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnPushed()
    {
        base.OnPushed();
        if (m_timer == null)
            m_timer = new MyTimer();
        localPlayer = lobbyMain.localPlayer.GetComponent<LobbyRobotSelectPlayer>();
        m_playersReady = 0;

        foreach (LobbyMainPlayer player in lobbyMain.players)
        {
            LobbyRobotSelectPlayer selectPlayer = player.GetComponent<LobbyRobotSelectPlayer>();
            selectPlayer.selectRobotPanel = this;
            selectPlayer.Init();
        }
        m_allPlayersLocked = false;
        if (!PhotonNetwork.IsConnectedAndReady || PhotonNetwork.IsMasterClient)
            m_timer.StartTimer(selectRobotTime, AutoLockInAll);
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.IsMasterClient)
            MulticastClientStartTimer(selectRobotTime);
    }

    public override void OnPopped()
    {
        base.OnPopped();
        m_timer.StopTimer();
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_timer.Update();
        if (!PhotonNetwork.IsConnectedAndReady || PhotonNetwork.IsMasterClient)
        {
            if (m_playersReady == lobbyMain.players.Count && m_allPlayersLocked == false)
            {
                m_allPlayersLocked = true;
                m_timer.StopTimer();
                m_timer.StartTimer(startTime, LoadScene);
                if (PhotonNetwork.IsConnectedAndReady)
                    MulticastClientStartTimer(startTime);
            }
        }
        timerText.text = ((int)m_timer.timeLeftSeconds).ToString();
    }

    [PunRPC]
    private void AutoLockInAll()
    {
        m_allPlayersLocked = true;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView.Get(this).RPC("AutoLockInAll", RpcTarget.Others);
        }
        localPlayer.AutoSelectRobots();

        if (!PhotonNetwork.IsConnectedAndReady || PhotonNetwork.IsMasterClient)
            m_timer.StartTimer(startTime, LoadScene);
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.IsMasterClient)
            MulticastClientStartTimer(startTime);
    }

    void Abort()
    {
        battleSubPanelUIManager.Pop();
    }

    [PunRPC]
    void ClientStartTimer(float startTime, double photonStartTime)
    {
        if (m_timer == null)
            m_timer = new MyTimer();
        double delay = PhotonNetwork.Time - photonStartTime;
        m_timer.StartTimer(startTime - (float)delay);
    }

    void MulticastClientStartTimer(float time)
    {
        PhotonView.Get(this).RPC("ClientStartTimer", RpcTarget.Others, time, PhotonNetwork.Time);
    }

    [PunRPC]
    void LoadScene()
    {
        Debug.Log("Who calls this?");
        lobbyMain.aborted = false;
        int sceneToLoad = LobbySettings.GetMapID();
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonView.Get(this).RPC("LoadScene", RpcTarget.Others);

            PhotonNetwork.IsMessageQueueRunning = false;
        }
        sceneChanger.ChangeToScene(sceneToLoad);
    }

    public void PlayerLockedIn()
    {
        m_playersReady++;
    }

    public void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
    }

    public void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Abort();
    }

    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
    }

    public void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
    }

    public void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
    }
}
