using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerSubPanel : MechroneerSubPanel
{
    MyTimer countDownTimer;
    public TextMeshProUGUI countDownText;
    MechroneerGameMode m_gameMode;
    public void Init(MechroneerController controller, MechroneerGameMode gameMode)
    {
        base.Init(controller);
        countDownTimer = new MyTimer();
        m_gameMode = gameMode;
    }

    public void StartTimer(float time, MyTimer.OnComplete onComplete)
    {
        countDownTimer.StartTimer(time, onComplete);
    }

    // Update is called once per frame
    void Update()
    {
        countDownText.text = "Game Starts in:\n" + (int)countDownTimer.timeLeftSeconds;
        countDownTimer.Update();
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        foreach (var controller in m_gameMode.controllers)
        {
            controller.SetControllerEnabled(false);
        }
    }

    protected override void OnDeactivated()
    {
        base.OnDeactivated();
        foreach (var controller in m_gameMode.controllers)
        {
            controller.SetControllerEnabled(true);
        }
    }
}
