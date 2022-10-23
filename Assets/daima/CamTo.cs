using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTo : MonoBehaviour
{
    public Camera imageCamera;
    public Transform point;
    public Transform look;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<Transform, Transform>("CamTo", lookAt);
    }
    public void lookAt(Transform tr,Transform lo)
    {
        point = tr;
        look = lo;
    }

    private void Update()
    {
        if(point)
        {
            imageCamera.transform.position = point.position;
            imageCamera.transform.LookAt(look);
        }
    }
}
