using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonNickName : MonoBehaviour
{
    public int maximumCharacterLimit = 16;
    public int minimumCharacterLimit = 4;
    private string NickName;

    static public PhotonNickName _instance;

    public string nickName
    {
        set
        {
            NickName = value;
            UserData._instance.Username = NickName;
            Debug.Log("Nickname set" + NickName);
            if (value.Length < minimumCharacterLimit)
                PhotonNetwork.LocalPlayer.NickName = "Player";
            else if (value.Length > maximumCharacterLimit)
                PhotonNetwork.LocalPlayer.NickName = value.Substring(0, maximumCharacterLimit);
            else
                PhotonNetwork.LocalPlayer.NickName = value;
        }
        get
        {
            return NickName;
        }
    }
}
