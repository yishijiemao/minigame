using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyManager : MonoBehaviour
{
    public List<EmenyPoint> emenies = new List<EmenyPoint>();
    public static EnemyManager instance;
    public MonstrBir bir;
    public List<EmenyOBJ> emenys=new List<EmenyOBJ>();
    public int com;
    public bool isBir;
    public int nowturn;
    public int couBir;
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
    }
    void Start()
    {
        EventCenter.GetInstance().AddEventListener<int>("MonBir", MonBir);
        EventCenter.GetInstance().AddEventListener("enemyJiaoLiUp", enemyJiaoLiUp);
        EventCenter.GetInstance().AddEventListener("NextEmeny", next);
        EventCenter.GetInstance().AddEventListener("WatchOver", WatchOver);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EnemyPointInit(EmenyPoint point)
    {
        emenies.Add(point);

    }
    public EmenyPoint findPoint(int id)
    {
        foreach(var a in emenies)
        {
            if (a.id == id)
                return a;
        }
        return null;
    }
    public void dead(EmenyOBJ point)
    {
         emenys.Remove(point);
    }
    public void MonBir(int turn)
    {
        if (turn < bir.waves.Count)
        {
            nowturn = turn;
            if (nowturn >= bir.waves.Count)
                songManager.instance.AKHard();
            if (bir.waves[nowturn].waves.Count==0)
            {
                FSM.instance.TransitionState();
                return;
            }
            
            couBir = 0;
            birEnemy();
            isBir = true;
            
        }
        else
        {
            FSM.instance.TransitionState();
        }
    }

    public void birEnemy()
    {
        var a = bir.waves[nowturn].waves[couBir];


        List<RoadPoint> points;
        EmenyPoint emenyPo=null;

        switch (a.brith)
        {
            case TypeOfBrith.优势低:
                points = RoadManager.instance.findPoint(a.brith);
                emenyPo = clickEnemy(points, true);
                emenyPo.creartBin(a.monster, a.aIpagecks);
                break;
            case TypeOfBrith.指挥近:
                points = RoadManager.instance.findPoint(a.brith);
                emenyPo = clickEnemy(points, true);
                emenyPo.creartBin(a.monster, a.aIpagecks);
                break;
            case TypeOfBrith.指挥远:
                points = RoadManager.instance.findPoint(a.brith);
                emenyPo = clickEnemy(points, true);
                emenyPo.creartBin(a.monster, a.aIpagecks);
                break;
            case TypeOfBrith.攻打优势:
                points = RoadManager.instance.findPoint(a.brith);
                if (points.Count==0)
                    points = RoadManager.instance.findPoint(a.brith);
                emenyPo = clickEnemy(points, true);
                emenyPo.creartBin(a.monster, a.aIpagecks);
                break;
            case TypeOfBrith.攻打劣势:
                points = RoadManager.instance.findPoint(a.brith);
                if (points.Count == 0)
                    points = RoadManager.instance.findPoint(a.brith);
                emenyPo = clickEnemy(points, true);
                emenyPo.creartBin(a.monster, a.aIpagecks);
                break;
            case TypeOfBrith.指定:
                emenyPo = a.point;
                a.point.creartBin(a.monster, a.aIpagecks);
                break;
        }
        EventCenter.GetInstance().EventTrigger<Vector3>("CamMoveToPoint", emenyPo.transform.position);
        songManager.instance.AKEmenyBir();
    }

    public void WatchOver()
    {
        if(!isBir)
        {
            return;
        }
        couBir += 1;
        if(couBir== bir.waves[nowturn].waves.Count)
        {
            FSM.instance.TransitionState();
            isBir = false;
            return;
        }
        else
        {
            
            birEnemy();
        }
    }
    public EmenyPoint clickEnemy(List<RoadPoint> points, bool big)
    {
        int[] quanzhis = new int[emenies.Count];
        foreach (var a in points)
        {
            for (int i = 0; i < emenies.Count; i++)
            {
                int quan = emenies[i].find(a);
                if (quanzhis[i] == 0 || (quan < quanzhis[i]) == big)
                    quanzhis[i] = quan;
            }
        }
        int mix;
        if (big == true)
            mix = 99;
        else
            mix = 0;
        for (int i = 0; i < emenies.Count; i++)
        {
            if((mix>quanzhis[i])==big)
            {
                mix = quanzhis[i];
                
            }
            
        }
        
        int k=-1;
        
        for (int i = 0; i < emenies.Count; i++)
        {
            if(mix==quanzhis[i])
            {
                if(k==-1)
                {
                    k = i;

                }
                else
                {
                    int ram = UnityEngine.Random.Range(0, 2);
                    k =  ram== 1 ? k : i;
                }
            }
        }
        return emenies[k];
    }

    public void enemyInit(EmenyOBJ oBJ)
    {
        emenys.Add(oBJ);
    }

    public void enemyJiaoLiUp()
    {
        if (emenys.Count != 0)
        {
            emenys[0].upJiaoli();
        }
        com = 0;
    }
    public void next()
    {
        com += 1;
        if (emenys.Count > com)
        {
            emenys[com].upJiaoli();
        }
        else
        {
            FSM.instance.TransitionState();
        }
    }
}
[Serializable]
public class MonstrBir
{
    public List<MonsterWave> waves = new List<MonsterWave>();
}

[Serializable]
public class MonsterWave
{
    public List<MonsterSSSS> waves = new List<MonsterSSSS>();
}

[Serializable]
public class MonsterSSSS
{
    public Bin monster;
    public TypeOfBrith brith;
    public EmenyPoint point;
    public List<AIpageck> aIpagecks = new List<AIpageck>();
}

