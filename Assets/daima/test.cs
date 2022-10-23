using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{
    public Text text;
   
  
  
    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<string>("test", display);
    }
    public void display(string str)
    {
        text.text = str;
    }
}
