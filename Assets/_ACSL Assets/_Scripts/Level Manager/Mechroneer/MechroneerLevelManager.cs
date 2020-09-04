using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MechroneerLevelManager : LevelManager
{
    public SmokeStackScript smokestacks;
    override protected void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void Start()
    {
        Init();
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "GarageScene")
            return;

        PhotonNetwork.IsMessageQueueRunning = true;
    }

    virtual public void OnRobotDeath()
    {
        if(smokestacks)
        {
            smokestacks.PlaySmokeStacks();
        }
    }

    public override void Init()
    {
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                MechroneerGameModeData data = LobbySettings.GetGameMode();
                MechroneerGameMode gameMode = PhotonNetwork.InstantiateSceneObject(data.gameMode.name, Vector3.zero, Quaternion.identity).GetComponent<MechroneerGameMode>();
                gameMode.Init(this, data);
            }
            else
            {
                InvokeRepeating("FindAndInitGameMode", 0, 1.0f / 60.0f);
            }
        }
        else
        {
            MechroneerGameModeData data = LobbySettings.GetGameMode();
            MechroneerGameMode gameMode = Instantiate(data.gameMode);
            gameMode.Init(this, data);
        }
    }

    private void FindAndInitGameMode()
    {
        MechroneerGameMode gameMode = FindObjectOfType<MechroneerGameMode>();
        if (gameMode)
        {
            gameMode.Init(this, LobbySettings.GetGameMode());
            CancelInvoke("FindAndInitGameMode");
        }
    }
}
