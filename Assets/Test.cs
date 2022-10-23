using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
   public ScrollContrl scroll;
    public Sprite ImageTest;
    private void Awake()
    {
        if (scroll != null)
        {
            int num = 10;
            string[] name = new string[num];
            Sprite[] sprites = new Sprite[num];
            string[] descr = new string[num];
            for (int i = 0; i < num; i++)
            {
                name[i] = (i + 1).ToString();
                sprites[i] = ImageTest;
                descr[i] = "descriptiion:" + (i + 1).ToString();
                Debug.Log("Load" + i);
            }
            scroll.SetItemsInfo(name, sprites, descr);
            scroll.SelectAction += (index) =>
            {
                Debug.Log(index);
            };
        }
        
    }
}
