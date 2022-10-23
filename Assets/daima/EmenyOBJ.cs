using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EmenyOBJ : MonoBehaviour
{
    public Bin bin;
    public GameObject ui;
    public GameObject @object;
    public GameObject Body;
    NavMeshAgent agent;
    [SerializeField] Transform target;
    public float jiaoli;
    public bool IsRecting;
    public Transform nowcheng;
    public Transform nowchengUI;
    public EmenyAI emenyAI;
    public bool chengIN;
    public bool isFire;
    public bool next;
    public bool isGouHuo;
    void Start()
    {

        @object = UIToCamManager.instance.newUIToCam(ui, this.transform.position);
        @object.GetComponent<EmenyUI>().bin = bin;
        @object.GetComponent<EmenyUI>().@object = gameObject;
        @object.GetComponent<EmenyUI>().image.sprite =bin.Ui;
        @object.GetComponent<EmenyUI>().image.color = Color.red;
        @object.SetActive(false);
        emenyAI = GetComponent<EmenyAI>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        EventCenter.GetInstance().AddEventListener("GouHuo", GouHuo);
        EventCenter.GetInstance().AddEventListener("GouHuoEnd", GouHuoEnd);
        EnemyManager.instance.enemyInit(this);
    }

    protected void Update()
    {

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

    public void GouHuo()
    {
        isGouHuo = true;
    }
    public void GouHuoEnd()
    {
        isGouHuo = false;
    }

    public void EnemyMovement()
    {
        if (isGouHuo)
            return;

        if (jiaoli <= 0)
        {
            IsRecting = false;
            if(next)
            {
                EventCenter.GetInstance().EventTrigger("NextEmeny");
                next = false;
            }
        }
        if (target && IsRecting)
        {

            agent.SetDestination(target.gameObject.transform.position);
            Vector3 var = transform.position - target.gameObject.transform.position;
            if (var.magnitude < 1f)
            {
                if(next)
                {
                    EventCenter.GetInstance().EventTrigger("NextEmeny");
                    next = false;
                }
                
               
                IsRecting = false;
                
            }

        }
    }
    public void setTaget(Transform trans)
    {

        target = trans;
        chengIN = false;
        @object.SetActive(true);
        nowcheng.GetComponent<RoadPoint>().disEmeny(this);
        nowcheng = null;
        agent.enabled = true;
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
                if (nowcheng.GetComponent<RoadPoint>().bins.Count > 0|| nowcheng.GetComponent<RoadPoint>().ZhiHui.Count > 0|| nowcheng.GetComponent<RoadPoint>().teams.Count > 0)
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
        if (cheng)
        {
            cheng.addEmeny(this);
            chengIN = true;
            @object.SetActive(false);
            agent.enabled = false;
        }

    }
    public void upJiaoli()
    {
        if(!isFire)
        {
            jiaoli = bin.jiaoli;
            setTaget(emenyAI.getAI());
            next = true;
        }
    }
    public void dead()
    {
        EnemyManager.instance.dead(this);
        Destroy(@object);
        Destroy(gameObject);
    }
}
