using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class fireUIICON : MonoBehaviour, IPointerClickHandler
{
    public Text text;
    public FirePackge packge;


    public void OnPointerClick(PointerEventData eventData)
    {
        EventCenter.GetInstance().EventTrigger<FirePackge>("FireUIDisplay", packge);
    }
}
