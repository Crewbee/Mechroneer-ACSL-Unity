using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapListSpawner : UISubPanel
{
    public Button sceneButtonPrefab;
    public GameObject sceneListContainer;
    public LobbyMaker lobbyMaker;

    private int _selectedMap;

    static readonly string assetString = "Assets/_ACSL Assets/_Scenes/";

    // Start is called before the first frame update
    void Start()
    {
        int amountOfScenes = SceneManager.sceneCountInBuildSettings;
        for (int i = 2; i < amountOfScenes - 1; i++)
        {
            Debug.Log(SceneUtility.GetScenePathByBuildIndex(i));
        }
        for (int i = 2; i < amountOfScenes - 1; i++)
        {
            Button spawnedButton = Instantiate<Button>(sceneButtonPrefab, sceneListContainer.transform);
            MapIndexUI indexUI = spawnedButton.GetComponent<MapIndexUI>();
            indexUI.mapIndex = i; ;
            spawnedButton.GetComponentInChildren<Text>().text = GetSceneName(i);
            spawnedButton.onClick.AddListener(() => { _selectedMap = indexUI.mapIndex; });
        }

        _selectedMap = lobbyMaker.startingMapID;
    }

    public void SelectMap()
    {
        lobbyMaker.SetMapID = _selectedMap;
    }

    static public string GetSceneName(int index)
    {
        string sceneName = SceneUtility.GetScenePathByBuildIndex(index);
        int period = sceneName.IndexOf('.', assetString.Length) - assetString.Length;
        return sceneName.Substring(assetString.Length, period);
    }
}
