using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnd : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public PlayerEnd(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;

    }

    public void OnEnter()
    {

        EventCenter.GetInstance().EventTrigger("IsRoundEnd");
        EventCenter.GetInstance().EventTrigger("getFiring");
        
    }

    public void OnUpdate()
    {



    }

    public void OnExit()
    {
        EventCenter.GetInstance().EventTrigger("noRoundEnd");
    }
}
