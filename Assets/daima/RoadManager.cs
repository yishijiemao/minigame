using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RoadManager : MonoBehaviour
{
    public List<Road> roads = new List<Road>();
    public static RoadManager instance;
    public List<chengbao> chengbaos;
    public List<RoadPoint> roadPoints;
    public List<road> roadsss;
    public List<RoadPoint> isFirePoints;
    public RoadPoint GouHuoPoint;
    public LineRenderer lineRenderer;
    public RoadPoint startPoint;
    public RoadPoint BirdPoint;
    public List<FirePackge> Fires= new List<FirePackge>();
    public int FireCount;
    public bool isRoundEnd;
    public GameObject Bird;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
           
            Destroy(gameObject);
          
        }
        EventCenter.GetInstance().AddEventListener<chengbao>("chengbaoInit", chengbaoInit);
        EventCenter.GetInstance().AddEventListener<RoadPoint>("roadPointsInit", roadPointsInit);
        EventCenter.GetInstance().AddEventListener("initZhiKui", initZhiKui);
        EventCenter.GetInstance().AddEventListener("initZhiKuiClone", initZhiKuiClone);
        EventCenter.GetInstance().AddEventListener("initBin", initBin);
        EventCenter.GetInstance().AddEventListener("initBinClone", initBinClone);
        EventCenter.GetInstance().AddEventListener<Road>("addRoad", addRoad);
        EventCenter.GetInstance().AddEventListener("orderNextOver", orderNextOver);
        EventCenter.GetInstance().AddEventListener("getFiring", getFiring);
        EventCenter.GetInstance().AddEventListener("NextTurn", NextTurn);
        EventCenter.GetInstance().AddEventListener<RoadPoint>("GouHuoPoint", setGouHuoPoint);
        EventCenter.GetInstance().AddEventListener("GouHuoEnd", GouHuoEnd);
        EventCenter.GetInstance().AddEventListener<RoadPoint>("setStartPoint", setStartPoint);
        EventCenter.GetInstance().AddEventListener<List<RoadPoint>>("disRoad", disRoad);
        EventCenter.GetInstance().AddEventListener("disRoadClose", disRoadClose);
        EventCenter.GetInstance().AddEventListener("IsRoundEnd", IsRoundEnd);
        EventCenter.GetInstance().AddEventListener("noRoundEnd", noRoundEnd);
        EventCenter.GetInstance().AddEventListener("WatchOver", WatchOver);
    }
    public void chengbaoInit(chengbao cheng)
    {
        
        chengbaos.Add(cheng);
    }
   
    public void roadPointsInit(RoadPoint cheng)
    {
        roadPoints.Add(cheng);
    }

    public void setStartPoint(RoadPoint point)
    {
        startPoint = point;
        foreach(var a in roadPoints)
        {
            a.display();
        }
    }
    public void disRoad(List<RoadPoint> ros)
    {
        
        lineRenderer.positionCount = ros.Count+roads.Count;
        lineRenderer.enabled = true;
        Vector3[] po = new Vector3[ros.Count + roads.Count];
        for (int i = 0; i < roads.Count; i++)
        {
            po[i] = roads[i].roadPoint.transform.position;
        }
        for (int i=0;i<ros.Count;i++)
        {
            po[i+ roads.Count] =ros[i].transform.position;
        }
        lineRenderer.SetPositions(po);
    }
    public void setRoad(List<RoadPoint> ros)
    {
        foreach (var a in ros)
        {
            Road ro = new Road();
            ro.roadPoint = a;
            roads.Add(ro);
        }
        startPoint = ros[ros.Count - 1];
    }

    public void birdPoint(RoadPoint point)
    {
        BirdPoint = point;
        foreach(var a in roadPoints)
        {
            a.BirdTime();
        }
    }

    public void BirdTo(RoadPoint point)
    {
        if(point!=BirdPoint)
        {
            GameObject @object = Instantiate(Bird, BirdPoint.transform.position,Quaternion.identity);
            @object.transform.LookAt(point.transform);
            @object.GetComponent<birdObj>().endPoint = point;
            @object.GetComponent<birdObj>().fly();

            songManager.instance.AKClickfeige();
            EventCenter.GetInstance().EventTrigger("bridCost");
            foreach (var a in roadPoints)
            {
                a.isbird = false;
            }
        }
    }
    public void disRoadClose()
    {
        lineRenderer.enabled = false;
    }
    public void initZhiKui()
    {
        foreach(var a in chengbaos)
        {
            a.isInit = true;
        }
    }
    public void NextTurn()
    {
        foreach (var a in chengbaos)
        {
            a.roadPoint.nextTurn();
        }
    }
    public void initZhiKuiClone()
    {
        foreach (var a in chengbaos)
        {
            a.isInit = false;
        }
    }
    public void initBin()
    {
        foreach (var a in chengbaos)
        {
            a.iszhihuiguan = true;
        }
    }
    public void IsRoundEnd()
    {
        isRoundEnd = true;
    }
    public void noRoundEnd()
    {
        isRoundEnd = false;
    }
    public void initBinClone()
    {
        foreach (var a in chengbaos)
        {
            a.checkZhuiHui();
        }
    }
    public void orderNextOver()
    {
        roads.Clear();
        foreach(var a in roadPoints)
        {
            a.overNext();
        }
    }
    public void addRoad(Road road)
    {
        roads.Add(road);
    }
    public void setGouHuoPoint(RoadPoint Point)
    {
        GouHuoPoint = Point;
    }
    
    public void GouHuoEnd()
    {
        foreach(var a in roadPoints)
        {
            if(a.isGouHuo)
            {
                a.GouHuoEnd();
            }
        }
    }
    public List<Road> getRoads()
    {
        List<Road> roadll = new List<Road>(roads);
        return roadll;
    }


    public void getFiring()
    {
        Debug.Log(666);
        isFirePoints.Clear();
       
        foreach(var a in roadPoints)
        {
            if(a.isFire)
            {
                isFirePoints.Add(a);
                
            }
        }
        if(isFirePoints.Count==0)
        {
            FSM.instance.TransitionState();
            return;
        }
        FireCount = 0;
        Fires.Clear();
        disPlayFiring();
    }
    public void disPlayFiring()
    {
        EventCenter.GetInstance().EventTrigger<Vector3>("CamMoveToPoint", isFirePoints[FireCount].transform.position);
        Fires.Add(isFirePoints[FireCount].fireDisplay());

    }
    public void WatchOver()
    {
        if(isRoundEnd)
        {
            FireCount += 1;
            if(FireCount==isFirePoints.Count)
            {
                EventCenter.GetInstance().EventTrigger<List<FirePackge>>("getFireUI", Fires);
                FSM.instance.TransitionState();
                return;
            }
            else
            {

                Fires.Add(isFirePoints[FireCount].fireDisplay());
                disPlayFiring();
            }
            
        }
    }

    public List<RoadPoint> findPoint(TypeOfBrith type)
    {
        List<RoadPoint> r=new List<RoadPoint>();
        switch(type)
        {
            case TypeOfBrith.优势低:
                int min=99;
                foreach(var a in roadPoints)
                {
                    if(a.ischeng)
                    {
                        int cout=0;
                        foreach(var b in a.bins)
                        {
                            cout += b.bin.youshi;
                        }
                        if (min > cout)
                            min = cout;
                    }
                }
                foreach (var a in roadPoints)
                {
                    if (a.ischeng)
                    {
                        int cout = 0;
                        foreach (var b in a.bins)
                        {
                            cout += b.bin.youshi;
                        }
                        if (min == cout)
                            r.Add(a);
                    }
                }
                        break;
            case TypeOfBrith.指挥近:
                foreach (var a in roadPoints)
                {
                    if (a.iszhihui)
                        r.Add(a);
                }

                    break;
            case TypeOfBrith.指挥远:
                foreach (var a in roadPoints)
                {
                    if (a.iszhihui)
                        r.Add(a);
                }
                break;
            case TypeOfBrith.攻打优势:
                foreach (var a in roadPoints)
                {
                    if (a.isFire&&a.isYouShi)
                        r.Add(a);
                }
                break;
            case TypeOfBrith.攻打劣势:
                foreach (var a in roadPoints)
                {
                    if (a.isFire && !a.isYouShi)
                        r.Add(a);
                }
                break;
        }


        return r;
    }

    
}
[Serializable]
public class road
{
   public RoadPoint point1;
   public RoadPoint point2;
   public int juli;
}
    