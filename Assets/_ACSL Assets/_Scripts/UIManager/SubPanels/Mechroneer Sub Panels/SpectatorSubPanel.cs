using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpectatorSubPanel : MechroneerSubPanel, MechroneerController.IActions
{
    public TextMeshProUGUI spectatorText;
    MechroneerDriver m_mechroneerCamera;
    bool m_isTeamMode;
    MechroneerGameState m_gameState;

    Robot m_currentlySpectatingRobot;
    protected override void OnActivated()
    {
        base.OnActivated();
        m_localController.PossessPlayer(this);
        m_localController.SetControllerEnabled(true);

        if (m_mechroneerCamera.m_CameraBehaviour.GetType() == typeof(RobotCameraBehaviour))
            m_mechroneerCamera.SwitchPerspective();
    }

    protected override void OnDeactivated()
    {
        base.OnDeactivated();
        m_localController.SetControllerEnabled(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindRandomPlayer();
    }

    public void SpectateNextPlayer()
    {
        int currentRobotIndex = GetCurrentRobotIndex();
        int nextRobotIndex = currentRobotIndex;
        do
        {
            nextRobotIndex = (nextRobotIndex + 1) % m_gameState.players.Count;
            m_currentlySpectatingRobot = m_gameState.players[nextRobotIndex];

        } while (m_currentlySpectatingRobot == null && currentRobotIndex != nextRobotIndex);

        SpectatePlayer(m_currentlySpectatingRobot);
    }

    public void SpectatePreviousPlayer()
    {
        int currentRobotIndex = GetCurrentRobotIndex();
        int nextRobotIndex = currentRobotIndex;
        do
        {
            nextRobotIndex = (nextRobotIndex - 1);
            if (nextRobotIndex < 0)
                nextRobotIndex = m_gameState.players.Count + nextRobotIndex;

            m_currentlySpectatingRobot = m_gameState.players[nextRobotIndex];

        } while (m_currentlySpectatingRobot == null && currentRobotIndex != nextRobotIndex);

        SpectatePlayer(m_currentlySpectatingRobot);
    }

    void FindRandomPlayer()
    {
        if (m_currentlySpectatingRobot)
            return;

        m_currentlySpectatingRobot = m_gameState.players[Random.Range(0, m_gameState.players.Count)];
        SpectatePlayer(m_currentlySpectatingRobot);
    }

    void SpectatePlayer(Robot robot)
    {
        m_mechroneerCamera.cameraCentroid.m_Targets.Clear();
        m_mechroneerCamera.cameraCentroid.m_Targets.Add(robot.transform);
        spectatorText.text = "SPECTATING:\n" + m_currentlySpectatingRobot.name;
    }

    int GetCurrentRobotIndex()
    {
        return m_gameState.players.FindIndex((Robot other) => { return m_currentlySpectatingRobot == other; });
    }

    public void PauseGame()
    {
        mechroneerUI.TogglePauseHUD();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnControllerEnabled()
    {
    }

    public void OnControllerDisabled()
    {
    }

    public void ZoomCamera(float input)
    {
    }

    public void RotateCamera(Vector2 input)
    {
    }

    public void ChangePerspective()
    {

    }

    public void MovePlayer(Vector3 mousePos)
    {
    }

    public void SelectAbility1()
    {
        SpectatePreviousPlayer();
    }

    public void SelectAbility2()
    {
        SpectateNextPlayer();
    }

    public void SelectAbility3()
    {
    }

    public void SelectAbility4()
    {
    }

    public void Init(MechroneerController controller, MechroneerGameState gameState)
    {
        base.Init(controller);
        m_mechroneerCamera = Camera.main.GetComponent<MechroneerDriver>();
        m_gameState = gameState;
    }
}
