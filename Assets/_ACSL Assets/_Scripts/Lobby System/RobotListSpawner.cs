using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotListSpawner : MonoBehaviour
{
    public RectTransform content;
    public Button robotTextPrefab;
    public LobbySelectRobotPanel lobby;
    // Start is called before the first frame update
    void Start()
    {
        foreach(RobotData robot in UserData._instance.robots)
        {
            Button robotButton = Instantiate<Button>(robotTextPrefab, content);
            RobotButton rb = robotButton.GetComponent<RobotButton>();
            rb.nameValue = robot.RobotName;
            rb.healthValue = robot.health.ToString();
            rb.energyValue = robot.energy.ToString();
            rb.rangeValue = robot.range.ToString();
            rb.speedValue = robot.speed.ToString();
            rb.UpdateText();
            robotButton.onClick.AddListener(() => { robotButton.GetComponent<SelectedRobot>().RobotSelected();  lobby.localPlayer.SelectRobot(robot); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
