using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRound : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public PlayerRound(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;

    }

    public void OnEnter()
    {
        EventCenter.GetInstance().EventTrigger<string>("test", "Íæ¼Ò»ØºÏ");
        EventCenter.GetInstance().EventTrigger("upJiaoli");
        EventCenter.GetInstance().EventTrigger("isYourTurn");
        songManager.instance.AKPlayRound();
    }

    public void OnUpdate()
    {



    }

    public void OnExit()
    {
        songManager.instance.AKPlayRoundEnd();
        EventCenter.GetInstance().EventTrigger("notYourTurn");
    }
}
