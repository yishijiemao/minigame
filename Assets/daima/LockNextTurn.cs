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
        text.text = "�ó�ʼ����Դ������ĵ�һ֧����";
        
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
        text.text = "�������";
        isGouHuo = true;
    }
    public void LockOrder()
    {
        isOrder = true;
        LostPra.SetActive(true);
        text.text = "ȷ���ƶ�·��";
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
