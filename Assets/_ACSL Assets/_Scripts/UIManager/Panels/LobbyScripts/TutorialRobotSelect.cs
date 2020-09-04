using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRobotSelect : UIPanel
{
    public LobbyMainPlayer playerPrefab;
    public LobbyMainPlayer localPlayer;

    public float selectRobotTime;
    public float startTime; 

    public Text timerText;
    public ChangeScene sceneChanger;

    MyTimer m_timer;

    bool aborted;

    public RectTransform content;
    public Button robotTextPrefab;

    public MechroneerGameModeData gameModeData;

    protected override void Awake()
    {
        base.Awake();
        m_timer = new MyTimer();
    }

    private void Start()
    {
        foreach (RobotData robot in UserData._instance.robots)
        {
            Button robotButton = Instantiate<Button>(robotTextPrefab, content);
            RobotButton rb = robotButton.GetComponent<RobotButton>();
            rb.nameValue = robot.RobotName;
            rb.healthValue = robot.health.ToString();
            rb.energyValue = robot.energy.ToString();
            rb.rangeValue = robot.range.ToString();
            rb.speedValue = robot.speed.ToString();
            rb.UpdateText();
            robotButton.onClick.AddListener(() => { robotButton.GetComponent<SelectedRobot>().RobotSelected(); localPlayer.GetComponent<LobbyRobotSelectPlayer>().SelectRobot(robot); });
        }
    }

    public override void OnPopped()
    {
        LobbySettings.SetNewMap(2);
        base.OnPopped();
        if (aborted)
        {
            Destroy(localPlayer.gameObject);
        }
    }

    public override void OnPushed()
    {
        LobbySettings.SetNewMap(4);
        base.OnPushed();
        localPlayer = CreatePlayer(1, "Mechroneer", 0, false);
        aborted = true;
        m_timer.StartTimer(selectRobotTime, AutoLockInAll);
        LobbySettings.SetNewGameMode(gameModeData);
    }

    private LobbyMainPlayer CreatePlayer(int ID, string name, int team, bool spectator)
    {
        LobbyMainPlayer player;
        player = Instantiate(playerPrefab);
        player.RegisterPlayer(ID, name, team, spectator);
        return player;
    }

    private void Update()
    {
        m_timer.Update();
    }
    public void AutoLockInAll()
    {
        localPlayer.GetComponent<LobbyRobotSelectPlayer>().AutoSelectRobots();
        m_timer.StartTimer(startTime, LoadScene);
    }

    void LoadScene()
    {
        aborted = false;
        int sceneToLoad = LobbySettings.GetMapID();
        sceneChanger.ChangeToScene(sceneToLoad);
    }
}

