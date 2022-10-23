using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockNextTurn : MonoBehaviour
{
    public Dictionary<string, GameObject> Locks=new Dictionary<string, GameObject>();
    public Text text;
    public GameObject LostPra;
    public Button button;
    public bool isOrder;
    public bool isGouHuo;
    public bool isYorTurn;
    public bool isZhiHui;
    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<string>("addLockStr", addLockStr);
        EventCenter.GetInstance().AddEventListener<string>("disLockStr", disLockStr);
        EventCenter.GetInstance().AddEventListener("LockOrder", LockOrder);
        EventCenter.GetInstance().AddEventListener("GouHuo", GouHuo);
        EventCenter.GetInstance().AddEventListener("isYourTurn", isYorTurn11);
        EventCenter.GetInstance().AddEventListener("notYourTurn", notYorTurn11);
        button.onClick.AddListener(nextTurn);
    }

    
    public void addLockStr(string str)
    {
        LostPra.SetActive(true);
        text.text = str;
        isZhiHui = true;
    }
  
    public void disLockStr(string str)
    {
        text.text = "用初始的资源建立你的第一支军队";
        
    }
    public void isYorTurn11()
    {
        isYorTurn = true;
    }
    public void notYorTurn11()
    {
        isYorTurn = false;
    }
    public void GouHuo()
    {
        LostPra.SetActive(true);
        text.text = "触发烽火";
        isGouHuo = true;
    }
    public void LockOrder()
    {
        isOrder = true;
        LostPra.SetActive(true);
        text.text = "确定移动路线";
    }
   
    public void nextTurn()
    {
        if (isOrder)
        {
            EventCenter.GetInstance().EventTrigger("orderOver");
            LostPra.SetActive(false);
            isOrder = false;
            return;
        }
        if(isGouHuo)
        {
            EventCenter.GetInstance().EventTrigger("GouHuoEnd");
            LostPra.SetActive(false);
            isGouHuo = false;
            RoadManager.instance.disRoadClose();
            EventCenter.GetInstance().EventTrigger("ClickUIClean");
            return;
        }
        if (isZhiHui)
        {
            FSM.instance.TransitionState();
            LostPra.SetActive(false);
            isZhiHui = false;
            EventCenter.GetInstance().EventTrigger("ClickUIClean");
            return;
        }
        
            
       
        if(isYorTurn)
        {
            FSM.instance.TransitionState();
            EventCenter.GetInstance().EventTrigger("ClickUIClean");
        }
        
        
    }
}
