using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZhiYuanUI : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        EventCenter.GetInstance().AddEventListener<string>("zhiyuanUI", zhiyuanUI);
    }

    public void zhiyuanUI(string str)
    {
        text.text = str;

    }
}
