using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    PlayerStart,PlayerRound,PlayerEnd,EnemyStart,EnemyRound,EnemyEnd  //玩家开始回合、玩家回合、玩家结束回合、敌方开始回合、敌方回合、敌方结束回合（敌方回合可以整合）
}


[Serializable]
public class Parameter
{
    public int turn=0;
    public int zhiyuan;
    public int Brid;
    public bool isGouHuo;
    //共同属性
    
}
public class FSM : MonoBehaviour
{
    public static FSM instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance = null)
            {
                Destroy(gameObject);
            }
        }
    }
    public Parameter parameter;
    
    private IState currentState;//当前状态
    private StateType stateType;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();//创建状态对象
    void Start()
    {
        states.Add(StateType.PlayerStart, new PlayerStart(this));//添加状态                                                                
        states.Add(StateType.PlayerRound, new PlayerRound(this));
        states.Add(StateType.PlayerEnd, new PlayerEnd(this));
        states.Add(StateType.EnemyStart, new EnemyStart(this));
        states.Add(StateType.EnemyRound, new EnemyRound(this));
        states.Add(StateType.EnemyEnd, new EnemyEndRound(this));
        EventCenter.GetInstance().AddEventListener<StateType>("changeType", TransitionState);
        currentState= states[StateType.PlayerStart];
        stateType = StateType.PlayerStart;
        currentState.OnEnter();
    }
    
    
    void Update()
    {
        
        currentState.OnUpdate();
        EventCenter.GetInstance().EventTrigger<string>("zhiyuanUI", parameter.zhiyuan.ToString());
    }
    public void TransitionState(StateType type)
    {
        if (currentState != null) currentState.OnExit();
        stateType = type;
        currentState = states[type];
        currentState.OnEnter();
    }
    public void TransitionState()
    {
        if (currentState != null) currentState.OnExit();
        if (stateType == StateType.EnemyEnd)
        {
            stateType = StateType.PlayerStart;
        }
        else
            stateType = stateType+1;
        
        currentState = states[stateType];
        currentState.OnEnter();
    }
    public bool costZiYuan(int index)
    {
        if(parameter.zhiyuan>=index)
        {
            parameter.zhiyuan -= index;
            return true;
        }
        return false;
    }

}
