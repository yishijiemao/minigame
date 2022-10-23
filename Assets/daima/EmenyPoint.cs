using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenyPoint : MonoBehaviour
{
    public Dictionary<RoadPoint,int> cheng=new Dictionary<RoadPoint, int>();
    
    public RoadPoint roadPoint;
    public int zongFanWei;
    public int id;
    void Start()
    {
        roadPoint = GetComponent<RoadPoint>();
        init(roadPoint, 0);
        EnemyManager.instance.EnemyPointInit(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void init(RoadPoint point,int jiaoli)
    {
        if(!cheng.ContainsKey(point))
        {
            cheng.Add(point,jiaoli);
        }
        else
        {
           if(jiaoli<cheng[point])
            {
                cheng[point] = jiaoli;
            }
           else
            return;
        }
        foreach(var a in point.roads)
        {
            init(a.roadPoint,jiaoli+1);
        }
        
    }
    public int find(RoadPoint point)
    {
        return cheng[point];
    }

    public void creartBin(Bin bin,List<AIpageck> aIpageck)
    {

        GameObject @object = Instantiate(bin.obj, transform.position, Quaternion.identity);
        if(aIpageck!=null)
        if(aIpageck.Count!=0)
        {
            @object.GetComponent<EmenyAI>().aIpagecks = aIpageck;
        }

    }
}
public enum TypeOfBrith
{
    优势低,指挥近,指挥远,攻打劣势,攻打优势,指定
}
