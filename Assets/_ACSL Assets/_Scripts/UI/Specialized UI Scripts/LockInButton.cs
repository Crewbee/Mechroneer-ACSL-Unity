using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInButton : MonoBehaviour
{
    public LobbySelectRobotPanel lobby;

    public void LockIn()
    {

        lobby.localPlayer.LockIn();
    }
}
