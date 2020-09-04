using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UISwitchPlayerType : MonoBehaviour
{
    public TextMeshProUGUI text;
    public LobbyMainPanel mainState;

    delegate void UpdateFunc();
    UpdateFunc pollPlayer;
    // Start is called before the first frame update
    void OnEnable()
    {
        pollPlayer = PollForPlayer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pollPlayer();
    }

    void PollForPlayer()
    {
        if (mainState.localPlayer)
        {
            if (mainState.localPlayer.isSpectator)
                text.text = "BECOME Player";
            else
                text.text = "BECOME Spectator";
            pollPlayer = DefaultUpdate;
        }
    }

    void DefaultUpdate()
    {
    }

    public void SwitchPlayerType()
    {
        mainState.SwitchPlayerType();
        if (mainState.localPlayer)
        {
            if (mainState.localPlayer.isSpectator)
                text.text = "BECOME Player";
            else
                text.text = "BECOME Spectator";
        }
    }
}
