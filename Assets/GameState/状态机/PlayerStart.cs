using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public PlayerStart(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;

    }
    bool isone=true;
    public void OnEnter()
    {
        
        EventCenter.GetInstance().EventTrigger<string>("test", "��ҿ�ʼ");
        
    }

    public void OnUpdate()
    {
        if(parameter.turn==0)
        {

            if(isone)
            {
                songManager.instance.AKStart();
                EventCenter.GetInstance().EventTrigger("initZhiKui");
                EventCenter.GetInstance().EventTrigger("initBin");
                EventCenter.GetInstance().EventTrigger<string>("addLockStr", "ѡ����ҳ����ǳ�");
                isone = false;
            }
        }
        else
        {
            manager.TransitionState();
        }
           


    }

    public void OnExit()
    {
        if (parameter.turn == 0)
        {
            EventCenter.GetInstance().EventTrigger("initBinClone");
        }
    }
}
