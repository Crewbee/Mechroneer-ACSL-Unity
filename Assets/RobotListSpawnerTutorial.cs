//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
////using Photon.Pun;
//using UnityEngine.SceneManagement;

//public class RobotListSpawnerTutorial : MonoBehaviour
//{
//    public RectTransform content;
//    public Button robotTextPrefab;
//    public LobbyRobotSelectPlayer lobbyRobotSelectPlayer;
//    private LobbyRobotSelectPlayer m_lobbyRobotSelectPlayer;
//    public ChangeScene changeScene;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//        foreach (RobotData robot in UserData._instance.robots)
//        {
//            Button robotButton = Instantiate<Button>(robotTextPrefab, content);
//            RobotButton rb = robotButton.GetComponent<RobotButton>();
//            rb.nameValue = robot.RobotName;
//            rb.healthValue = robot.health.ToString();
//            rb.energyValue = robot.energy.ToString();
//            rb.rangeValue = robot.range.ToString();
//            rb.speedValue = robot.speed.ToString();
//            rb.UpdateText();
//            robotButton.onClick.AddListener(() => { m_lobbyRobotSelectPlayer.AddRobot(robot); robotButton.GetComponent<SelectedRobot>().RobotSelected(); });
//        }
//    }

//    public void StartTutorialScene()
//    {
//        changeScene.ChangeToScene(LobbySettings.GetMapID());
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    private void OnEnable()
//    {
//        GameObject templobby = PhotonNetwork.Instantiate(lobbyRobotSelectPlayer.name, Vector3.zero, Quaternion.identity);
//        m_lobbyRobotSelectPlayer = templobby.GetComponent<LobbyRobotSelectPlayer>();
//        m_lobbyRobotSelectPlayer.lobbyMainPlayer.ID = 0;
//        m_lobbyRobotSelectPlayer.enabled = true;
//    }
//    private void OnDisable()
//    {
//        //PhotonNetwork.Destroy(m_lobbyRobotSelectPlayer.gameObject);
//    }
//}
