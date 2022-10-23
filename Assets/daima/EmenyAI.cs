using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EmenyAI : MonoBehaviour
{
    public List<AIpageck> aIpagecks = new List<AIpageck>();
    public EmenyOBJ emeny;
    public RoadPoint roadPoint;

    private void Start()
    {
        emeny = GetComponent<EmenyOBJ>();
        
    }
    private void Update()
    {
        
    }
   

    public Transform getAI()
    {
        roadPoint = emeny.nowcheng.GetComponent<RoadPoint>();
        RoadPoint ro = null;
        foreach (var a in aIpagecks)
        {
            switch(a.type)
            {
                case TypeAI.指定:
                    return a.point.transform;
                case TypeAI.支援:
                    ro=null;
                    var c = clickRoad(a.quanZhi);
                    Debug.Log(9999999999);
                    foreach (var b in c)
                    {
                        if(b.Key.isFire)
                        {
                            if (ro == null)
                            {
                                ro = b.Key;
                            }
                            if (c[ro] <= b.Value)
                            {
                                if (c[ro] == b.Value)
                                {
                                    int ram = UnityEngine.Random.Range(0, 2);
                                    ro = ram == 1 ? ro : b.Key;
                                }
                                else
                                    ro = b.Key;
                            }
                               
                                    
                            
                                
                        }
                    }
                    if (ro != null)
                        return ro.transform;
                    break;
                case TypeAI.攻打弱:
                    ro = null;
                    var d = clickRoad(a.quanZhi);
                    foreach (var b in d)
                    {
                        if (b.Key.ischeng)
                        {
                            if (ro == null || ro.binYouShiCount() <= b.Key.binYouShiCount())
                            {
                                if (ro.binYouShiCount() == b.Key.binYouShiCount())
                                {
                                    int ram = UnityEngine.Random.Range(0, 2);
                                    ro = ram == 1 ? ro : b.Key;
                                }

                            }

                        }
                    }
                    if (ro != null)
                        return ro.transform;
                    break;
                case TypeAI.攻打进:
                    ro = null;
                    var e = clickRoad(a.quanZhi);
                    foreach (var b in e)
                    {
                        
                        if (b.Key.ischeng)
                        {
                            if (ro == null)
                            {
                                ro = b.Key;
                            }
                            if (e[ro] <= b.Value)
                            {
                                if (e[ro] == b.Value)
                                {
                                    int ram = UnityEngine.Random.Range(0, 2);
                                    ro = ram == 1 ? ro : b.Key;
                                }
                                else
                                    ro = b.Key;
                            }

                        }
                    }
                    if (ro != null)
                        return ro.transform;
                    break;
            }
        }
        return null;
    }
    public Dictionary<RoadPoint, int> clickRoad(int quan)
    {
       
        Dictionary<RoadPoint, int> points = new Dictionary<RoadPoint, int>();
        cliCkRRoad(roadPoint, quan, points);
        points.Remove(roadPoint);
        return points;
    }
    public void cliCkRRoad(RoadPoint point, int quan, Dictionary<RoadPoint, int> points)
    {

        if (!points.ContainsKey(point))
        {
            points.Add(point, quan);
        }
        else
        {
            if (quan > points[point])
            {
                points[point] = quan;
            }
            return;
        }
        if (quan == 0)
            return;
        foreach (var a in point.roads)
        {
            cliCkRRoad(a.roadPoint, quan - 1, points);
        }
    }
}
public enum TypeAI
{
    支援,攻打弱,攻打进,指定
}

[Serializable]
public class AIpageck
{
    public TypeAI type;
    public int quanZhi;
    public RoadPoint point;

}