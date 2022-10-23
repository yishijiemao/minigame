using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class EmenyUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Bin bin;
    public GameObject @object;
    public Image image;
    public void OnPointerClick(PointerEventData eventData)
    {
        ClickUI.instance.display(getGagck());
    }

    public ClickPagck getGagck()
    {
        ClickPagck pagck = new ClickPagck();
        pagck.str = bin.namE;
        pagck.Object = @object;
        pagck.type = TypeClick.noActivation;
        

        return pagck;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
