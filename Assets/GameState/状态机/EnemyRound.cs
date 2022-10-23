using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRound : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public EnemyRound(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;

    }

    public void OnEnter()
    {
        EventCenter.GetInstance().EventTrigger<string>("test", "µ–»Àªÿ∫œ");
        EventCenter.GetInstance().EventTrigger("enemyJiaoLiUp");
    }

    public void OnUpdate()
    {



    }

    public void OnExit()
    {

    }
}
