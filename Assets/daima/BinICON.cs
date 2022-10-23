using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BinICON : MonoBehaviour,IPointerClickHandler
{
    public Text text;
    public Text text1;
    public Image image;
    public GameObject @object;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        @object.SetActive(true);
        if(@object.GetComponent<BinOBJ>()!=null)
        {
            ClickUI.instance.display( @object.GetComponent<BinOBJ>().getGagck());
        }
        if (@object.GetComponent<General>() != null)
        {
            ClickUI.instance.display(@object.GetComponent<General>().getGagck());
        }
        if (@object.GetComponent<TeamOBJ>() != null)
        {
            ClickUI.instance.display(@object.GetComponent<TeamOBJ>().getGagck());
        }
    }
}
