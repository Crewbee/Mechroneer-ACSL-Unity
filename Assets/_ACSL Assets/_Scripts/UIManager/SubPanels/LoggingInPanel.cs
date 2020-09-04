using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LoggingInPanel : FadingUISubPanel, Photon.Realtime.IConnectionCallbacks
{
    public InputField usernameInput;
    public WarningBox usernameWarning;
    public UIManager manager;
    public UIPanel panelToPush;

    static bool m_hasLoggedIn = false;

    public void OnConnectedToMaster()
    {
        ToggleSubPanel();
        manager.Push(panelToPush);
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        NetworkManager._instance.GoOffline();

        ToggleSubPanel();
        manager.Push(panelToPush);
    }

    protected override void OnActivated()
    {
        if (!VerifyLogin())
            return;
        base.OnActivated();
        //ToggleSubPanel();
        //manager.Push(panelToPush);
        StartCoroutine(NetworkManager._instance.CheckInternetConnection((isOnline) =>
        {
            if (!isOnline)
            {
                NetworkManager._instance.GoOffline();
                ToggleSubPanel();
                manager.Push(panelToPush);
            }
            else
            {
                if (!PhotonNetwork.IsConnectedAndReady)
                {
                    NetworkManager._instance.Connect();
                }
                else
                {
                    ToggleSubPanel();
                    manager.Push(panelToPush);
                }
            }
            m_hasLoggedIn = true;
        }));
    }

    private bool VerifyLogin()
    {
        if (usernameInput.text.Length <= 3)
        {
            usernameWarning.Display();
            return false;
        }
        else
        {
            return true;
        }
    }

    protected override void OnDeactivated()
    {
        base.OnDeactivated();
    }

    public void OnConnected()
    {
    }

    public void OnDisconnected(DisconnectCause cause)
    {
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
    }
}
