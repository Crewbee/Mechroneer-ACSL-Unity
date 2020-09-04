using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    [SerializeField]
    protected GameMode m_gameModePrefab;
    [SerializeField]
    protected GameState m_gameStatePrefab;
    [SerializeField]
    protected Player m_playerPrefab;
    [SerializeField]
    protected PlayerUI m_playerUIPrefab;
    [SerializeField]
    protected Controller m_controllerPrefab;

    public GameMode gameMode { get; protected set; }
    protected virtual void Awake()
    {
        Init();
    }
    virtual public void Init()
    {
        gameMode = Instantiate(m_gameModePrefab);
    }
}
