using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class birdUI : MonoBehaviour
{
    public void Start()
    {
        chi = FSM.instance.parameter.Brid;
        EventCenter.GetInstance().AddEventListener("isYourTurn", isYourTurn);
        EventCenter.GetInstance().AddEventListener("notYourTurn", notYourTurn);
        EventCenter.GetInstance().AddEventListener("birdCost", cost);
        button.onClick.AddListener(onClick);
    }
    public bool isyourTurn;
    public Text text;
    public int chi;
    public Button button;
    public void isYourTurn()
    {
        if(chi>0)
            button.interactable = true;
    }
    public void notYourTurn()
    {
        button.interactable = false;
    }
    public void onClick()
    {
        EventCenter.GetInstance().EventTrigger("isBird");
    }
    public void cost()
    {
        chi -= 0;
    }
    private void Update()
    {
        
        text.text = chi.ToString();
    }
}
