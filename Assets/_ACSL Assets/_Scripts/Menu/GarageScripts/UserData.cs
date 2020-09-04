using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    static public UserData _instance;
    //Robots
    public List<RobotData> robots;

    //Inventory
    public Inventory m_inventory;

    //Account
    public bool signedIN;
    public string Username;
    public int m_Experience { get; set; }

    public int m_Level { get; set; }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
        if (robots == null)
            robots = new List<RobotData>();
    }
}
