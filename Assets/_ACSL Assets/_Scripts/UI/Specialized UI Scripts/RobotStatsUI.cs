using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotStatsUI : MonoBehaviour
{
    public Robot robot;
    public DoubleBarSubPanel healthBar;
    public DoubleBarSubPanel energyBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.Init(robot.healthComponent);
        energyBar.Init(robot.energyComponent);
    }
}
