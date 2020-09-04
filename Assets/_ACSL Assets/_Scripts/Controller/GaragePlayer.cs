//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GaragePlayer : OldPlayer
//{
//    // Control camera state
//    // hold robot selection
//    // passing robot selection to temp-player
//    // 

//    public Camera garageCamera;
//    public RobotData selectedRobot;
//    //public LobbyRobotSelectState lobby;


//    // Start is called before the first frame update
//    void Start()
//    {
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //if (lobby == null)
//        //{
//        //    lobby = FindObjectOfType<LobbyRobotSelectState>();
//        //}
//    }

//    private void SelectRobot()
//    {
//        int layerMask = 1 << LayerMask.NameToLayer("robot");
//        layerMask = int.MaxValue ^ layerMask;
//        if (Physics.Raycast(garageCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, layerMask))
//        {
//            MenuRobot menuRobot = hit.transform.GetComponent<MenuRobot>();
//            selectedRobot = hit.transform.GetComponent<MenuRobot>().menuRobotData;

//            if (menuRobot == null)
//            {
//                menuRobot = hit.transform.GetComponentInParent<MenuRobot>();
//                selectedRobot = hit.transform.GetComponentInParent<MenuRobot>().menuRobotData;
//            }

//            //lobby.localPlayer.AddRobot(menuRobot.menuRobotData);
//        }
//    }
//}
