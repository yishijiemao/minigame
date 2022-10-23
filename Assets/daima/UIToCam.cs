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
        // 需要性能优化 仅在物体移动或相机移动后调用即可
        UIToCamManager.instance.Reposition(this.transform.position,@object);
    }
    



}
