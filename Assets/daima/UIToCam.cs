using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToCam : MonoBehaviour
{
   
    public GameObject ui;
    
   
    GameObject @object;
    private void Start()
    {
       @object= UIToCamManager.instance.newUIToCam(ui, this.transform.position);
       
    }

    private void Update()
    {
        // ��Ҫ�����Ż� ���������ƶ�������ƶ�����ü���
        UIToCamManager.instance.Reposition(this.transform.position,@object);
    }
    



}
