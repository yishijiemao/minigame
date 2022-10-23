using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class General : MonoBehaviour
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
    public Transform nowcheng;
    public Transform nowchengUI;
    public bool isCatch;
    public bool isTurn;
    public GameObject Body;
    void Start()
    {

        @object = UIToCamManager.instance.newUIToCam(ui, this.transform.position);
        @object.GetComponent<GeneralUI>().bin = bin;
        @object.GetComponent<GeneralUI>().@object = gameObject;
        @object.GetComponent<GeneralUI>().image.sprite = bin.Ui;
        @object.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        binManager.instance.addBin(bin);
        EventCenter.GetInstance().AddEventListener("upJiaoli", upJiaoli);
        EventCenter.GetInstance().AddEventListener("notYourTurn", notYourTurn);
    }

    protected void Update()
    {
       
        if (!chengIN)
        {
            UIToCamManager.instance.Reposition(this.transform.position, @object);
        }
        if(!IsRecting && nowcheng && !chengIN)
        {
            inRoad(nowcheng.GetComponent<RoadPoint>());
        }
       

        EnemyMovement();
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
        pagck.type = TypeClick.zhihui;
        if (isCatch||!isTurn)
        {
            pagck.type = TypeClick.noActivation;
        }
        return pagck;

    }
    public void EnemyMovement()
    {
        if (jiaoli <= 0)
        {
            IsRecting = false;
            if (target)
                agent.SetDestination(agent.transform.position);
        }
        if (target && IsRecting)
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
        
        nowcheng.GetComponent<RoadPoint>().disZhiHui(this);
        nowcheng = null;
        target = path[0].roadPoint.transform;
        Id = 0;
        IsRecting = true;
    }

    public void beCatch()
    {
        @object.GetComponent<GeneralUI>().isCatch = true;
        isCatch = true;
    }
    public void beSave()
    {
        @object.GetComponent<GeneralUI>().isCatch = false;
        isCatch = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag=="roadPoint")
        {
            if (nowcheng != other.transform)
            {
                nowcheng = other.transform;
                jiaoli--;
            }
            if (nowcheng.GetComponent<RoadPoint>().evemy.Count > 0)
            {
                nowcheng.GetComponent<RoadPoint>().inFire();
                IsRecting = false;
            }
        }
    }

    public void inRoad(RoadPoint cheng)
    {
        if (cheng)
        {
            cheng.addZhiHui(this);
            chengIN = true;
            @object.SetActive(false);

        }

    }
    public void upJiaoli()
    {
        jiaoli = bin.jiaoli;
        IsRecting = true;
        isTurn =true;
    }
    public void dead()
    {
        binManager.instance.dead(bin);
        Destroy(@object);
        Destroy(gameObject);
    }


}
