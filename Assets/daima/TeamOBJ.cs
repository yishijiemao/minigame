using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TeamOBJ : MonoBehaviour
{
    public Bin bin;
    public List<Road> path = new List<Road>();
    public GameObject ui;
    NavMeshAgent agent;
    [SerializeField] Transform target;
    public GameObject @object;
    public int Id;
    public float jiaoli;
    public bool IsRecting;
    public bool chengIN;
    public bool isZhiHui;
    public bool isFire;
    public bool isCheng;
    public List<Bin> team = new List<Bin>();
    public Transform nowcheng;
    public Transform nowchengUI;
    public bool isInit;
    public bool isTurn;
    public bool isGuoHuo;
    void Start()
    {

        
    }

    public void init()
    {
        @object = UIToCamManager.instance.newUIToCam(ui, this.transform.position);
        @object.SetActive(false);
        @object.GetComponent<GeneralUI>().image.sprite = bin.Ui;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        EventCenter.GetInstance().AddEventListener("upJiaoli", upJiaoli);
        EventCenter.GetInstance().AddEventListener("notYourTurn", notYourTurn);
        
        isInit = true;
    }
    public void notYourTurn()
    {
        isTurn = false;

    }
    public ClickPagck getGagck()
    {

        ClickPagck pagck = new ClickPagck();
        pagck.str = bin.namE;
        pagck.Object = gameObject;
        if (isZhiHui&&isTurn)
        {
            pagck.type = TypeClick.bin;
        }
        else
        {
            pagck.type = TypeClick.noActivation;
        }

        return pagck;

    }
    protected void Update()
    {

        if (!isInit)
            return;

        if (!chengIN)
        {
            UIToCamManager.instance.Reposition(this.transform.position, @object);
        }

        if (!IsRecting && nowcheng && !chengIN)
        {
            inRoad(nowcheng.GetComponent<RoadPoint>());
        }

        EnemyMovement();


    }

    public void EnemyMovement()
    {
        if (jiaoli <= 0)
        {
            IsRecting = false;

            if (target)
                agent.SetDestination(agent.transform.position);
        }
        if (isFire)
        {
            agent.SetDestination(nowcheng.transform.position);
        }
        if (target && IsRecting && !isFire)
        {

            agent.SetDestination(target.gameObject.transform.position);
            Vector3 var = transform.position - target.gameObject.transform.position;
            if (var.magnitude < 1f)
            {
                if (Id + 1 < path.Count)
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
        path = new List<Road>();
        for (int i = 0; i < pathll.Count; i++)
        {
            Road rrr = new Road();
            rrr.juli = pathll[i].juli;
            rrr.roadPoint = pathll[i].roadPoint;
            path.Add(rrr);
        }

        target = path[0].roadPoint.transform;
        chengIN = false;
        @object.SetActive(true);
        nowcheng.GetComponent<RoadPoint>().disTeam(this);
        nowcheng = null;
        isZhiHui = false;
        isCheng = false;
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
        nowcheng.GetComponent<RoadPoint>().disTeam(this);
        nowcheng = null;
        isZhiHui = false;
        isCheng = false;

        @object.GetComponent<binUI>().iszhihui = false;
        Id = 0;
        IsRecting = true;
    }
    public void TeamBreak()
    {
        var a = nowcheng.GetComponent<RoadPoint>();

        a.disTeam(this);
        foreach(var b in team)
        {
            a.creartBin(b);
        }

        Destroy(@object);
        Destroy(gameObject);
    }
    public void inRoad(RoadPoint cheng)
    {
        if (cheng)
        {
            cheng.addTeam(this);

            chengIN = true;
            @object.SetActive(false);
            if (cheng.ischeng)
                isCheng = true;
        }

    }
    public void upJiaoli()
    {
        jiaoli = bin.jiaoli;
        isTurn = true;
        if (path.Count > 0)
            IsRecting = true;
    }

    public void ZhiHuiIn()
    {
        isZhiHui = true;
        
    }

    public void ZhiHuiOut()
    {
        isZhiHui = false;
      
    }

    public void dead()
    {
        foreach(var a in team)
        {
            binManager.instance.dead(a);
        }
        Destroy(@object);
        Destroy(gameObject);
    }

    
}
