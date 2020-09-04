using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//using Photon.Realtime;

public class FreeForAllGameState : MechroneerGameState
{
    public override void OnMatchEnd()
    {
        Debug.Log("Match ended");
    }

    public override void OnMatchStart()
    {
        base.OnMatchStart();
    }


    protected override void OnPlayerDies(Robot caller)
    {
        base.OnPlayerDies(caller);
        if (players.Count < 2)
            gameMode.EndGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    if (matchState == Match_State.In_Progress)
        //    {
        //        int aliveCount = 0;
        //        foreach (Player player in players)
        //        {
        //            if (player)
        //                aliveCount++;
        //        }
        //        if (aliveCount < 2)
        //            gameMode.EndGame();
        //    }
        //}
    }
}
