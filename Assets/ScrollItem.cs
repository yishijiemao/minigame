using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScrollItem : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
   public Image image;

    public Text nameText;

    public CanvasGroup canvasGroup;

    public int Itemindex;

    public int infoIndex;

    public string descripition;

    public RectTransform rectTransform;

    private bool isDrag;

    private ScrollContrl scroll;
    private void Awake()
    {
     
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetInfo(Sprite sprite,string name,string descr,int infoIndex,ScrollContrl scroll)

    {
        image.sprite = sprite;
        nameText.text = name;
        this.descripition = descr;
        this.infoIndex = infoIndex;
        this.scroll = scroll;
    }
    public void SetAlpha( float alpha)
    {
        canvasGroup.alpha = alpha;  
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = false;
        scroll.OnPointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("iamin");
        if(!isDrag)
        {
            scroll.Select(Itemindex, infoIndex, rectTransform);
            Debug.Log("iamin2");
            
        }
        scroll.OnPointerUp(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        scroll.OnDrag(eventData);
    }

    

  
}
