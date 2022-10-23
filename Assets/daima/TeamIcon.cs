using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TeamIcon : MonoBehaviour, IPointerClickHandler
{
    public teamUI uI;

    public bool isClick;
    public Image image;
    public BinOBJ oBJ;
    public Text text;
    public Text text1;
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (isClick)
        {
            uI.dis(oBJ);
            transform.localScale = new Vector3(1, 1, 1);
            
        }
        else
        {
            uI.add(oBJ);
            transform.localScale = new Vector3(1.2f, 1.2f, 1);
           
        }

    }
}
