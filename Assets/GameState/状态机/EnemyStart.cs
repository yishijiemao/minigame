using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public EnemyStart(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;

    }

    public void OnEnter()
    {
        EventCenter.GetInstance().EventTrigger<string>("test", "µÐÈË¿ªÊ¼");
        EventCenter.GetInstance().EventTrigger<int>("MonBir", parameter.turn);

    }

    public void OnUpdate()
    {



    }

    public void OnExit()
    {

    }
}
