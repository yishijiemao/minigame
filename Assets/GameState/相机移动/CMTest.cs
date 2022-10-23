using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CMTest : MonoBehaviour
{
    public Transform CMtransform;
    float Speed = 50.0f;
    public Transform CMQue;
    public Vector3 tage;
    public bool isMove;
    public float edge;
    bool edgeMove = false;
    public Transform maxPos;
    public Transform minPos;
    public bool isTime;
    // Start is called before the first frame update
    void Start()
    {
        CMtransform = GetComponent<Transform>();
        EventCenter.GetInstance().AddEventListener<Vector3>("CamMoveToPoint", CamMoveToPoint);

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 InputDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) InputDir.z = 1.0f;
        if (Input.GetKey(KeyCode.S)) InputDir.z = -1.0f;
        if (Input.GetKey(KeyCode.A)) InputDir.x = -1.0f;
        if (Input.GetKey(KeyCode.D)) InputDir.x = 1.0f;
        if(Input.GetAxis("Mouse ScrollWheel")<0)
        {
            InputDir.y = 5;
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            InputDir.y = -5;
            
        }
        if (Input.GetKeyDown(KeyCode.Space))	//移动开关
        {
            edgeMove = !edgeMove;
        }
        if (edgeMove)	//如果打开
        {
            //屏幕左下角为坐标(0, 0)
            if (Input.mousePosition.x > Screen.width - edge)//如果鼠标位置在右侧
            {
                InputDir.x = 1.0f;//就向右移动
            }
            if (Input.mousePosition.x < edge)
            {
                InputDir.x = -1.0f;
            }
            if (Input.mousePosition.y > Screen.height - edge)
            {
                InputDir.z = 1.0f;
            }
            if (Input.mousePosition.y < edge)
            {
                InputDir.z = -1.0f;
            }

        }
        Vector3 moveDir = transform.forward * InputDir.z + transform.right * InputDir.x+ transform.up * InputDir.y;
        CMtransform.position += moveDir * Speed * Time.deltaTime;
        if(isMove)
        {
            moveTo();
        }
        
    }
    private void LateUpdate()
    {
        transform.position=new Vector3( Mathf.Clamp(transform.position.x, minPos.position.x, maxPos.position.x), Mathf.Clamp(transform.position.y, minPos.position.y, maxPos.position.y), Mathf.Clamp(transform.position.z, minPos.position.z, maxPos.position.z));
    }
    //聚焦对象
    private void CamMoveToPoint(Vector3 target)
    {

        tage = target;
        isMove = true;
        isTime = false;
        Invoke("timeUp", 1f);
        
        
    }
    public void timeUp()
    {
        isTime = true;
    }
    public void moveTo()
    {
        Vector3 a = UIToCamManager.instance.CamPoint(tage);
        if(a.x<100&&a.y<100&&a.x>-100&&a.y>-100||isTime)
        {
            Invoke("watch", 2f);
            isMove = false;
            return;
        }
            
        Vector3 b = new Vector3(a.x, 0, a.y);
        transform.position = Vector3.MoveTowards(transform.position, b, 1);
    }
    public void watch()
    {
        EventCenter.GetInstance().EventTrigger("WatchOver");
    }
   
}
