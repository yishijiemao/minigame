using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEndRound : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public EnemyEndRound(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;

    }

    public void OnEnter()
    {
        EventCenter.GetInstance().EventTrigger("IsRoundEnd");
        EventCenter.GetInstance().EventTrigger<string>("test", "µ–»ÀΩ· ¯");
        EventCenter.GetInstance().EventTrigger("getFiring");
    }

    public void OnUpdate()
    {



    }

    public void OnExit()
    {
        parameter.turn += 1;
        EventCenter.GetInstance().EventTrigger("noRoundEnd");
        EventCenter.GetInstance().EventTrigger("NextTurn");
    }
}
