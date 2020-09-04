using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Photon.Pun;
using UnityEngine.SceneManagement;
//using Photon.Realtime;

//TODO Delete this script
//public class UntoggleLogIn : MonoBehaviour //MonoBehaviourPunCallbacks
//{
//    public UIPanelOld objectToToggle;
//    public int panelToPush;
//    public InputField usernameInput;
//    public WarningBox usernameWarning;
//    public override void OnConnectedToMaster()
//    {
//        UIManagerOld.Instance.TogglePanel(objectToToggle);
//        UIManagerOld.Instance.Push(panelToPush);
//    }

//    public override void OnCustomAuthenticationFailed(string debugMessage)
//    {
//        base.OnCustomAuthenticationFailed(debugMessage);
//        NetworkManager._instance.GoOffline();

//        UIManagerOld.Instance.TogglePanel(objectToToggle);
//        UIManagerOld.Instance.Push(panelToPush);
//    }

//    public void ToggleLogIn()
//    {
//        StartCoroutine(NetworkManager._instance.CheckInternetConnection((isConnected) => {
//            if (!isConnected)
//            {
//                NetworkManager._instance.GoOffline();

//                //UIManager.Instance.TogglePanel(objectToToggle);
//                UIManagerOld.Instance.Push(panelToPush);
//            }
//            else
//            {

//                if (!PhotonNetwork.IsConnectedAndReady)
//                    UIManagerOld.Instance.TogglePanel(objectToToggle);
//                else
//                    UIManagerOld.Instance.Push(panelToPush);
//            }
//        }));
//    }

//    public void VerifyLogin()
//    {
//        if (usernameInput.text.Length <= 0)
//        {
//            usernameWarning.Display();
//        }
//        else
//        {
//            NetworkManager._instance.Connect();
//            ToggleLogIn();
//        }
//    }
//}


