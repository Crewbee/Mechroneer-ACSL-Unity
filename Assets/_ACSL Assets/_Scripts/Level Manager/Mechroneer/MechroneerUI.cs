using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using Photon.Pun;

public class MechroneerUI : PlayerUI, IInRoomCallbacks, IMatchmakingCallbacks
{
    public RobotHUDSubPanel hudPanel;
    public SpectatorSubPanel spectatorPanel;
    public TimerSubPanel timerPanel;
    public WinLoseSubPanel winLosePanel;
    public PauseSubPanel pausePanel;

    UISubPanel activePanel;

    private bool m_rematchAvailable;
    public override void Init(GameMode gameMode, GameState gameState, Controller controller)
    {
        base.Init(gameMode, gameState, controller);

        activePanel = hudPanel;
        activePanel.SetSubPanelActive(true);
        m_rematchAvailable = true;

        MechroneerController mechroneerController = m_controller as MechroneerController;

        hudPanel.Init(mechroneerController);
        spectatorPanel.Init(mechroneerController, gameState as MechroneerGameState);
        timerPanel.Init(mechroneerController, gameMode as MechroneerGameMode);
        winLosePanel.Init(mechroneerController);
        pausePanel.Init(mechroneerController);
    }

    override public void PossessPlayer(Player player)
    {
        if (player != null)
        {
            Debug.Log("PossessPlayer");
            base.PossessPlayer(player);
            if (player)
                hudPanel.PossessPlayer(player as Robot);
        }
        else
        {
            //SwitchToSpectatorHUD();
        }
    }

    public void SwitchToPlayerHUD()
    {
        if (m_player)
        {
            Debug.Log("HUD");
            SwitchActivePanel(hudPanel);
        }
        else
        {
            //activePanel.SetSubPanelActive(false);
            //hudPanel.SetSubPanelActive(false);
            Debug.Log("Spectator");
            SwitchActivePanel(hudPanel);
            activePanel.SetSubPanelActive(false);
            Destroy(hudPanel.gameObject);
            SwitchToSpectatorHUD();
        }
    }
    public void SwitchToSpectatorHUD()
    {
        SwitchActivePanel(spectatorPanel);
    }
    public void SwitchToTimerHUD(float time, MyTimer.OnComplete onComplete)
    {
        SwitchActivePanel(timerPanel);
        timerPanel.StartTimer(time, onComplete);
    }
    public void SwitchToWinLoseHUD(bool didWin)
    {
        SwitchActivePanel(winLosePanel);
        winLosePanel.DidWin(didWin);
    }
    public void TogglePauseHUD()
    {
        pausePanel.ToggleSubPanel();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("GarageScene");
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
    }

    private void SwitchActivePanel(UISubPanel panelToSwitch)
    {
        Debug.Log("Switch in panels Active panel: "+ activePanel+" New panel: " + panelToSwitch);

        activePanel.SetSubPanelActive(false);
        activePanel = panelToSwitch;
        activePanel.SetSubPanelActive(true);
    }

    public void RequestRematch()
    {
        if (!m_rematchAvailable)
        {
            QuitGame();
            return;
        }
        (m_gameMode as MechroneerGameMode).RequestRematch();
    }

    #region Photon Callbacks
    virtual protected void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    virtual protected void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnLeftRoom()
    {
        SceneManager.LoadScene("GarageScene");
    }

    public void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        m_rematchAvailable = false;
    }
    #endregion

    #region Unused Photon Callbacks
    public void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
    }

    public void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
    }

    public void OnCreatedRoom()
    {
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnJoinedRoom()
    {
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
    }

    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
    }
    #endregion
}
