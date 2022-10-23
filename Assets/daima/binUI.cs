using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class binUI : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Bin bin;
    public GameObject @object;
    public Image image;
    public bool iszhihui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickUI.instance.display(getGagck());
    }

    public ClickPagck getGagck()
    {
        ClickPagck pagck = new ClickPagck();
        pagck.str = bin.namE;
        pagck.Object = @object;
        if(iszhihui)
        {
            pagck.type = TypeClick.bin;
        }
        else
        {
            pagck.type = TypeClick.noActivation;
        }

        return pagck;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }
}
