using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAIMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    public GameObject[] Thelowcity;
    NavMeshAgent agent;
    public bool IsRecting;
    public int times;
    private bool isReady=false;
    // Start is called before the first frame update
    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener("setEnemyReady", setEnemyReady);
    }
    void Start()
    {
        target = FindTheLowCity(Thelowcity);
        Debug.Log(target);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        IsRecting = true;

    }
    // Update is called once per frame
    void Update()
    {
        //if(isReady)
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        if (times == 0)
        {
            IsRecting = false;
            agent.SetDestination(agent.transform.position);
        }
        else 
        {
            IsRecting = true;
        }
        if (target && IsRecting)
        {
            
            agent.SetDestination(target.gameObject.transform.position);
        }
    }

    public Transform FindTheLowCity(GameObject[] list)
    {
        GameObject ret=list[0];
        foreach(GameObject val in list)
        {
            if(val.GetComponent<baseinformation>().bingli<ret.GetComponent<baseinformation>().bingli)
            {
                ret = val;
            }
        }
        return ret.transform;
    }

    public Transform FindTheNearCiy(GameObject[] list)
    {
        Debug.Log("isok");
        GameObject ret = list[0];
        float retdistance=0;
        foreach (GameObject a in list)
        {
            float distance = (this.transform.position - a.transform.position).magnitude;
            if (retdistance>distance||retdistance==0)
            {
                retdistance=distance;
                
                ret = a;
            }
        }
        return ret.transform;
    }//ÓÐbug
   

    public void setEnemyReady()
    {
        isReady = !isReady;
    }
}