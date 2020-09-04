using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbySettingsDisplay : UISubPanel
{
    public LobbyMaker lobbyMaker;
    public InputField roomName;
    public Text selectedSceneText;
    public Text selectedGameModeText;
    // Start is called before the first frame update
    void Start()
    {
        if (lobbyMaker)
        {
            if (selectedSceneText)
                selectedSceneText.text = MapListSpawner.GetSceneName(lobbyMaker.startingMapID);
            if (selectedGameModeText)
                selectedGameModeText.text = lobbyMaker.startingGameMode.name;
            lobbyMaker.OnMapIDChanged += LobbyMaker_OnMapIDChanged;
            lobbyMaker.OnGameModeChanged += LobbyMaker_OnGameModeChanged;
        }
    }

    private void LobbyMaker_OnGameModeChanged(MechroneerGameModeData gameMode)
    {
        selectedGameModeText.text = lobbyMaker.startingGameMode.name;
    }

    private void LobbyMaker_OnMapIDChanged(int mapID)
    {
        selectedSceneText.text = MapListSpawner.GetSceneName(lobbyMaker.startingMapID);
    }
}
