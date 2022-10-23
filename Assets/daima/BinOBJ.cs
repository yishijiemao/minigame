using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
public class BinOBJ : MonoBehaviour
{
    public Bin bin;
    public List<Road> path=new List<Road>();
    public GameObject ui;
    NavMeshAgent agent;
    public GameObject Body; 
    [SerializeField] Transform target;
    public GameObject @object;
    public int Id;
    public float jiaoli;
    public bool IsRecting;
    public bool chengIN;
    public bool isZhiHui;
    public bool isFire;
    public bool isCheng;
    public bool isTurn;
    public bool isGouHuo;
    public Transform nowcheng;
    public Transform nowchengUI;
    void Start()
    {
       
        @object = UIToCamManager.instance.newUIToCam(ui, this.transform.position);
        @object.GetComponent<binUI>().bin = bin;
        @object.GetComponent<binUI>().@object = gameObject;
        @object.SetActive(false);
        @object.GetComponent<binUI>().image.sprite = bin.Ui;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        binManager.instance.addBin(bin);
        EventCenter.GetInstance().AddEventListener("upJiaoli", upJiaoli);
        EventCenter.GetInstance().AddEventListener("notYourTurn", notYourTurn);
    }
    public ClickPagck getGagck()
    {
       
        
        ClickPagck pagck = new ClickPagck();
        pagck.str = bin.namE;
        pagck.Object = gameObject;
        if (isZhiHui&&isTurn||isGouHuo)
        {
            pagck.type = TypeClick.bin;
        }
        else
        {
            pagck.type = TypeClick.noActivation;
        }
       
        return pagck;

    }
    
    public void notYourTurn()
    {
        isTurn = false;

    }
    protected  void Update()
    {
      
        if(!chengIN)
        {
            UIToCamManager.instance.Reposition(this.transform.position, @object);
        }
       
        if(!IsRecting&&nowcheng&&!chengIN)
        {
            inRoad(nowcheng.GetComponent<RoadPoint>());
        }

        EnemyMovement();
       

    }
    
    public void EnemyMovement()
    {
        if (jiaoli <=0)
        {
            IsRecting = false;
        }
        if (target && IsRecting&&!isFire)
        {
            agent.SetDestination(target.gameObject.transform.position);
            Vector3 var = transform.position - target.gameObject.transform.position;
            if (var.magnitude < 1f)
            {
                if (Id+1 < path.Count)
                {
                    Id++;
                    target = path[Id].roadPoint.transform;
                }
                else
                    IsRecting = false;

            }
                
        }
    }
    public void getRoads()
    {
        
        List<Road> pathll = RoadManager.instance.getRoads();
        if (pathll.Count == 0)
            return;
        path = new List<Road>();
        for(int i=0;i<pathll.Count;i++)
        {
            Road rrr = new Road();
            rrr.juli = pathll[i].juli;
            rrr.roadPoint = pathll[i].roadPoint;
            path.Add(rrr);
        }
        agent.enabled = true;
        //Body.SetActive(true);
        target = path[0].roadPoint.transform;
        chengIN = false;
        @object.SetActive(true);
        nowcheng.GetComponent<RoadPoint>().disBin(this);
        nowcheng = null;
        isZhiHui = false;
        isCheng = false;
        
        @object.GetComponent<binUI>().iszhihui =false;
        Id = 0;
        IsRecting = true;
    }
    public void getRoadsToZhiYuan(List<RoadPoint> points)
    {
        path = new List<Road>();
        for (int i = 0; i < points.Count; i++)
        {
            Road rrr = new Road();
            rrr.roadPoint = points[i];
            path.Add(rrr);
        }
        agent.enabled = true;
        //Body.SetActive(true);
        target = path[0].roadPoint.transform;
        chengIN = false;
        @object.SetActive(true);
        nowcheng.GetComponent<RoadPoint>().disBin(this);
        nowcheng = null;
        isZhiHui = false;
        isCheng = false;

        @object.GetComponent<binUI>().iszhihui = false;
        Id = 0;
        IsRecting = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "roadPoint")
        {
            if (nowcheng != other.transform)
            {
                nowcheng = other.transform;
                jiaoli--;
                if (nowcheng.GetComponent<RoadPoint>().evemy.Count > 0)
                {
                    nowcheng.GetComponent<RoadPoint>().inFire();
                    IsRecting = false;
                    isFire = true;
                }
                    

            }
        }

    }
   
    public void inRoad(RoadPoint cheng)
    {
        if(cheng)
        {
            cheng.addBin(this);
            
            chengIN = true;
            @object.SetActive(false);
            agent.enabled = false;
            //Body.SetActive(false);
            if (cheng.ischeng)
                isCheng = true;
        }
        
    }
    public void upJiaoli()
    {
        jiaoli = bin.jiaoli;
        isTurn = true;
        if (path.Count > 0)
        {
            IsRecting = true;
            agent.enabled = true;
        }
            
    }
    
    public void ZhiHuiIn()
    {
        isZhiHui = true;
        @object.GetComponent<binUI>().iszhihui = true;
    }

    public void ZhiHuiOut()
    {
        isZhiHui = false;
        @object.GetComponent<binUI>().iszhihui = false;
    }
    public void LevelUp()
    {
        nowcheng.GetComponent<RoadPoint>().creartBin(bin.nextBins[0]);
        nowcheng.GetComponent<RoadPoint>().disBin(this);
        dead();
    }


    public void dead()
    {
        binManager.instance.dead(bin);
        Destroy(@object);
        Destroy(gameObject);
    }
    public void dis()
    {
        Destroy(@object);
        Destroy(gameObject);
    }
}

[Serializable]
public class Path
{
    public Transform start;
    public Transform Poitn;//路径点
    public float MoveTime;//移动时间
    public Vector3 Speed;//移动速度
}