using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToCamManager : MonoBehaviour
{
    public static UIToCamManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance = null)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
        canvas = GameObject.Find("CameraCanvas").transform;
        originOff = new Vector3(-Screen.width / 2, -Screen.height / 2 - 100);//中间向下偏移100像素 
    }
    Transform canvas;
    private Vector3 originOff;  // 当前UI系统(0,0)点 相对于屏幕左下角(0, 0)点的偏移量
    private void Start()
    {
        
       
        
    }
    public GameObject newUIToCam(GameObject @object, Vector3 target)
    {
        GameObject  @object11 = Instantiate(@object, canvas);
        Reposition(target,@object);
        return object11;
    }
    public GameObject newUIToCam(GameObject @object, Vector3 target,Transform tra)
    {
        GameObject @object11 = Instantiate(@object, tra);
        Reposition(target, @object);
        return object11;
    }
    // 根据目标物体 重定位UI
    public void Reposition(Vector3 target,GameObject @object)
    {
        Vector3 position = Camera.main.WorldToScreenPoint(target) + originOff;
        position.z = 0;
        @object.transform.localPosition = position;
        
    }
    public Transform getUIToCanvas()
    {
        return canvas;
    }
    public Vector3 CamPoint(Vector3 target)
    {
        Vector3 position = Camera.main.WorldToScreenPoint(target) + originOff;
        position.z = 0;
        return position;
    }
  
}
