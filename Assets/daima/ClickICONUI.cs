using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickICONUI : MonoBehaviour,IPointerClickHandler
{
    public Image image;
    public string fun;

    public void OnPointerClick(PointerEventData eventData)
    {
        EventCenter.GetInstance().EventTrigger(fun);
    }
}
