using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour
{
    public LevelManager levelManager { get; protected set; }
    public GameState gameState { get; protected set; }

    protected Player playerPrefab;

    public PlayerUI playerUI { get; protected set; }
    public Controller localController { get; protected set; }

    virtual public void Init(LevelManager levelManager, GameState gameStatePrefab, Controller controllerPrefab, Player playerPrefab, PlayerUI playerUIPrefab)
    {
        this.levelManager = levelManager;
        gameState = Instantiate(gameStatePrefab);
        gameState.Init(this);

        localController = Instantiate(controllerPrefab);
        this.playerPrefab = playerPrefab;

        playerUI = Instantiate(playerUIPrefab);
        playerUIPrefab.Init(this, gameState, localController);

        Player player = Instantiate(playerPrefab);
        localController.PossessPlayer(player);
        playerUI.PossessPlayer(player);
    }

    public void Init(LevelManager levelManager, GameState gameStatePrefab, Player playerPrefab)
    {
        this.levelManager = levelManager;
        this.playerPrefab = playerPrefab;
        gameState = Instantiate(gameStatePrefab);
        gameState.Init(this);
    }

    virtual public void StartGame()
    {
        gameState.OnMatchStart();
    }

    virtual public void EndGame()
    {
        gameState.OnMatchEnd();
    }
}
