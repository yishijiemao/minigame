using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class RoadPoint : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public List<Road> roads;
    public GameObject ui;
    public GameObject tan;
    public GameObject teamOBjjjjj;
    public bool isclick=false;
    public GameObject @object;
    public Road last;
    public chengbao cheng;
    public bool ischeng;
    public List<BinOBJ> bins;
    public int bHpCost;
    public int bHpMax;
    public List<EmenyOBJ> evemy;
    public int eHpCost;
    public int eHpMax;
    public bool isGouHuo;
    public List<General> ZhiHui;
    public List<TeamOBJ> teams;
    public GameObject binUI;
    public GameObject binUIbag;
    public bool isFire;
    public bool iszhihui;
    public int fireID;
    public bool isYouShi;
    public bool isCatch;
    public bool isbird;
    public bool birdIn;
    protected void Start()
    {

        EventCenter.GetInstance().AddEventListener("jiantoudis", des);
        EventCenter.GetInstance().AddEventListener("notYourTurn", notYourTurn);
        @object = UIToCamManager.instance.newUIToCam(ui, this.transform.position);
        UIToCamManager.instance.Reposition(this.transform.position, @object);
        @object.SetActive(false);

        cheng = GetComponent<chengbao>();
        if (cheng)
        {
            ischeng = true;
            cheng.roadPoint = this;
        }
           
        binUIbag = UIToCamManager.instance.newUIToCam(binUI, this.transform.position);
        if(cheng)
        {
            var a= binUIbag.GetComponent<BinBagUI>();
            a.cheng.SetActive(true);
            a.chengText.text = "0";
            a.chengPass.text = "+" + cheng.addzhiyuan + "/t";
        }
        EventCenter.GetInstance().EventTrigger<RoadPoint>("roadPointsInit", this);
    }
    public void display()
    {
        
        isclick = true;
       

    }
    public void disRoad(Road l)
    {

        last = l;
        display();
    }
    protected void Update()
    {
        if (isclick)
            UIToCamManager.instance.Reposition(this.transform.position, @object);
            UIToCamManager.instance.Reposition(this.transform.position, binUIbag);
    }
    public void next()
    {
        EventCenter.GetInstance().EventTrigger<RoadPoint>("setStartPoint", this);
    }
    public void des()
    {
        @object.SetActive(false);
    }
    public void nextTurn()
    {
        cheng.upChenChi();
        var a = binUIbag.GetComponent<BinBagUI>();
        if(iszhihui)
        {
           FSM.instance.parameter.zhiyuan+=(int)cheng.takeZhiYuan();
        }
        a.chengText.text = cheng.zhiyuan.ToString();
        a.chengPass.text = "+" + cheng.addzhiyuan + "/t";
    }
    public List<RoadPoint> findRoad(RoadPoint tager)
    {
        Dictionary<RoadPoint, int> cheng = new Dictionary<RoadPoint, int>();
        initRoad(this, cheng, 0);
        int i = cheng[tager];
        RoadPoint po=tager;
        int c = i;
        List<RoadPoint> ro=new List<RoadPoint>();
        ro.Add(po);
        
        for(int j=0;j<c;j++)
        {
            foreach(var a in po.roads)
            {
                if(cheng[a.roadPoint]<i)
                {
                    i = cheng[a.roadPoint];
                    po = a.roadPoint;
                    ro.Add(po);
                    
                }
            }
        }
        
        return ro;

    }
    public void isBird()
    {
        RoadManager.instance.birdPoint(this);
    }
    public void BirdTime()
    {
        isbird = true;
    }
    public void initRoad(RoadPoint point,Dictionary<RoadPoint, int> cheng, int count)
    {
        if (!cheng.ContainsKey(point))
        {
            cheng.Add(point, count);
        }
        else
        {
            if (count < cheng[point])
            {
                cheng[point] = count;
            }
            else
                return;
        }
        foreach (var a in point.roads)
        {
            initRoad(a.roadPoint,cheng, count + 1);
        }
    }
  
    public void OnPointerClick(PointerEventData eventData)
    { 
        //if (fireID != 0 && isFire)
        //{
        //    fireDisplay();
        //    return;
        //}
        if(isbird)
        {
            RoadManager.instance.BirdTo(this);
        }
        if (isclick)
        {
            RoadManager.instance.setRoad(findRoad(RoadManager.instance.startPoint));
        }
        EventCenter.GetInstance().EventTrigger<List<BinICONPager>>("creatBinICON", getBinICONPager());


    }


    public void BirdIn()
    {
        birdIn = true;
        iszhihui = true;
        cheng.iszhihuiguan = true;
        foreach(var c in bins)
        {
             c.ZhiHuiIn();
        }
    }
    public List<BinICONPager> getBinICONPager()
    {

        List<BinICONPager> pager = new List<BinICONPager>();

        foreach(var a in ZhiHui)
        {
            pager.Add(new BinICONPager(a.gameObject, a.bin));
        }
        foreach (var a in bins)
        {
            pager.Add(new BinICONPager(a.gameObject, a.bin));
        }
        foreach (var a in teams)
        {
            var c = new BinICONPager(a.gameObject, a.bin);
            c.isTeam = true;
            c.team = a.team;
            pager.Add(c);
        }
        foreach (var a in evemy)
        {
            pager.Add(new BinICONPager(a.gameObject, a.bin));
        }
        return pager;
    }

    public int binYouShiCount()
    {
        int cou = 0;
        foreach(var a in bins)
        {
            cou += a.bin.youshi;
        }
        return cou;
    }
    public int enemyYouShiCount()
    {
        int cou = 0;
        foreach (var a in evemy)
        {
            cou += a.bin.youshi;
        }
        return cou;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isclick)
        {
            EventCenter.GetInstance().EventTrigger< List < RoadPoint >>("disRoad", findRoad(RoadManager.instance.startPoint));
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isclick)
        {
            EventCenter.GetInstance().EventTrigger("disRoadClose");
        }
    }
    public void addBin(BinOBJ bin)
    {
        bins.Add(bin);
        clickMan();
        if (iszhihui)
            bin.ZhiHuiIn();
        if(isCatch)
        {
            isCatch = false;
            foreach(var a in ZhiHui)
            {
                a.beSave();
            }
        }
        
        EventCenter.GetInstance().EventTrigger<List<BinICONPager>>("creatBinICON", getBinICONPager());
        if (isFire)
        {
            bHpMax += bin.bin.Hp;

        }
    }
    public void addTeam(TeamOBJ te)
    {
        teams.Add(te);
        clickMan();
        if (iszhihui)
            te.ZhiHuiIn();
        if (isCatch)
        {
            isCatch = false;
            foreach (var a in ZhiHui)
            {
                a.beSave();
            }
        }
        EventCenter.GetInstance().EventTrigger<List<BinICONPager>>("creatBinICON", getBinICONPager());
        if (isFire)
        {
            bHpMax += te.bin.Hp;

        }
    }
    public void disBin(BinOBJ bin)
    {
        bins.Remove(bin);
        clickMan();
    }
    public void disTeam(TeamOBJ te)
    {
        teams.Remove(te);
        clickMan();
    }

    public void addEmeny(EmenyOBJ emeny)
    {
        evemy.Add(emeny);
        Lost();
        if (isFire)
        {
            bHpMax += emeny.bin.Hp;

        }

        clickMan();

    }
    public void disEmeny(EmenyOBJ emeny)
    {
        evemy.Remove(emeny);
        clickMan();
    }
    public void disZhiHui(General general)
    {
        ZhiHui.Remove(general);
        if(ZhiHui.Count==0)
        {
            iszhihui = false;
            var dd = binUIbag.GetComponent<BinBagUI>();
            dd.zhiHui.SetActive(false);
            cheng.iszhihuiguan = false;
            foreach(var a in bins)
            {
                a.ZhiHuiOut();
            }
            foreach (var a in teams)
            {
                a.ZhiHuiOut();
            }
        }
    }
    public void creartBin(Bin bin)
    {
        GameObject @object = Instantiate(bin.obj, transform.position, Quaternion.identity);
       

    }
    public void teamUI()
    {
        TeamPacge pacge = new TeamPacge();
        foreach(var a in bins)
        {
            pacge.bins.Add(a);
        }
        pacge.point = this;

        EventCenter.GetInstance().EventTrigger<TeamPacge>("teamUIdisplay", pacge);
    }
    public void creartTeam(List<BinOBJ> bin)
    {
        if(bin.Count<=1)
        {
            return;
        }
        GameObject @object = Instantiate(teamOBjjjjj, transform.position, Quaternion.identity);

        var a = @object.GetComponent<TeamOBJ>();
        Bin bbb = new Bin();
        List<Bin> b = new List<Bin>(); 
        int hp=0, jiaoli=99, youshi=0;
        foreach(var c in bin)
        {
            hp += c.bin.Hp;
            jiaoli = jiaoli > c.jiaoli ? (int)c.jiaoli : jiaoli;
            youshi += c.bin.youshi;
            b.Add(c.bin);
            disBin(c);
            c.dis();
        }
        bbb.namE = "军团";
        bbb.Hp = hp;
        bbb.jiaoli = jiaoli;
        bbb.youshi = youshi;
        bbb.type = TypeOfBin.军团;
        bbb.Ui= (Sprite)Resources.Load($"image/ui/军团", typeof(Sprite));
        a.bin = bbb;
        a.team = b;
        Debug.Log(bbb.Hp + " " + bbb.jiaoli + " " + bbb.youshi);
        a.init();
    }
    public void tanchu(string str)
    {
        var kkk = UIToCamManager.instance.newUIToCam(tan, this.transform.position);
        kkk.GetComponent<NumberTipShow>().tipNumber.text = str;
    }

    public void clickMan()
    {
        var a = binUIbag.GetComponent<BinBagUI>();
        if (bins.Count == 0 && evemy.Count == 0&&teams.Count==0)
            a.man.SetActive(false);
        else
            a.man.SetActive(true);
        int teamCount = 0;
        foreach(var b in teams)
        {
            foreach (var c in b.team)
            {
                teamCount += 1;
            }
        }
        a.text.text = (bins.Count+teamCount).ToString();
        a.text1.text = evemy.Count.ToString();
        
    }
    public void inFire()
    {
        if(!isFire)
        {
            isFire = true;
            binUIbag.GetComponent<BinBagUI>().isFire();
            bHpMax = 0;
            eHpMax = 0;
            foreach(var a in bins)
            {
                a.isFire = true;
                bHpMax += a.bin.Hp;
            }
            foreach (var a in teams)
            {
                a.isFire = true;
                bHpMax += a.bin.Hp;
            }
            foreach (var a in evemy)
            {
                a.isFire = true;
                eHpMax += a.bin.Hp;
            }
            GouHuo(1);
            EventCenter.GetInstance().EventTrigger<RoadPoint>("GouHuoPoint", this);
            EventCenter.GetInstance().EventTrigger("GouHuo");
        }
       
    }
    public void GouHuo(int zhi)
    {
        if(!isGouHuo)
        {
            int i=0; 
            isGouHuo = true;
            var dd = binUIbag.GetComponent<BinBagUI>();
            dd.fengHuo.SetActive(true);
            if (bins.Count>0||teams.Count>0)
            {
               
                foreach(var a in bins)
                {
                    a.isGouHuo = true;
                }
                foreach(var a in teams)
                {
                    a.isGuoHuo = true;
                }
                i += 1;
                if(ischeng)
                {
                    if (cheng.fengHuoTai)
                        i += 1;
                }
            }
            else
            {
                if (zhi!=0)
                {
                    i = zhi - 1;
                }
                else
                {
                    return;
                }
            }
            foreach(var a in roads)
            {
                
                a.roadPoint.GouHuo(zhi - 1);

            }
        }
    }
    public void GouHuoEnd()
    {
        if (bins.Count > 0 || teams.Count > 0)
        {

            foreach (var a in bins)
            {
                a.isGouHuo = false;
            }
            foreach (var a in teams)
            {
                a.isGuoHuo = false;
            }
            
        }var dd = binUIbag.GetComponent<BinBagUI>();
            dd.fengHuo.SetActive(false);
        isGouHuo = false;
    }
    public void addZhiHui(General general)
    {
        ZhiHui.Add(general);
        if (isFire && bins.Count == 0)
        {
            general.beCatch();
        }

        if (!iszhihui)
        {
            iszhihui = true;
            if(cheng)
            cheng.iszhihuiguan = true;
            foreach (var c in bins)
            {
                c.ZhiHuiIn();
            }
            foreach (var c in teams)
            {
                c.ZhiHuiIn();
            }
       
            var a = binUIbag.GetComponent<BinBagUI>();
                if(cheng)
                FSM.instance.parameter.zhiyuan += (int)cheng.takeZhiYuan();
            var dd = binUIbag.GetComponent<BinBagUI>();
            dd.zhiHui.SetActive(true);
            if(cheng)
            {
                a.chengText.text = cheng.zhiyuan.ToString();
                a.chengPass.text = "+" + cheng.addzhiyuan + "/t";
            }
            
        }
        EventCenter.GetInstance().EventTrigger<List<BinICONPager>>("creatBinICON", getBinICONPager());
    }
    public void overNext()
    {
        if(isclick)
        {
            isclick = false;
            @object.SetActive(false);
        }
    }

    public void notYourTurn()
    {
        if(birdIn)
        {
            if(ZhiHui.Count==0)
            {
                Debug.Log(1111);
                iszhihui = false;
                cheng.iszhihuiguan = false;
                
                foreach (var a in bins)
                {
                    a.ZhiHuiOut();
                }
            }
            birdIn = false;
        }
    }

    public FirePackge fireDisplay()
    {
        List<Bin> bi = new List<Bin>();
        List<Bin> en = new List<Bin>();
        
        foreach (var a in bins)
        {
            bi.Add(a.bin);
        }
        foreach (var a in teams)
        {
            foreach (var b in a.team)
                bi.Add(b);
        }
        foreach (var a in evemy)
        {
            en.Add(a.bin);
        }
        fireID = 0;
        return FireEnd(bi, en);
        
    }
    public void tanchushengli()
    {
        tanchu("胜利");
    }
    public void tanchushibai()
    {
        tanchu("战败");
    }
    public void binWin(int Hp)
    {
        
        eHpCost = Hp;
        isYouShi = true;
        Invoke("tanchushengli", 1f);
        if (Hp>=eHpMax)
        {
            foreach (var a in evemy)
            {
                a.dead();
            }
            foreach(var a in bins)
            {
                a.isFire = false;
            }
            foreach (var a in teams)
            {
                a.isFire = false;
            }
           
            evemy.Clear();
            isFire = false;
            bHpCost = 0;
            eHpCost = 0;
            binUIbag.GetComponent<BinBagUI>().noFire();
            songManager.instance.AKFireWin();
        }
        else
        songManager.instance.AKFireing();
        clickMan();
    }

    public void enemyWin(int Hp)
    {
        bHpCost = Hp;
        isYouShi = false;

        Invoke("tanchushibai", 0.3f);

        if (Hp >= bHpMax)
        {
            foreach (var a in bins)
            {
                a.dead();
            }
            bins.Clear();
            foreach (var a in ZhiHui)
            {
                a.dead();
            }
            ZhiHui.Clear();
            foreach (var a in teams)
            {
                a.dead();
            }
            foreach (var a in evemy)
            {
                a.isFire = false;
            }
            teams.Clear();
            isFire = false;
            bHpCost = 0;
            eHpCost = 0;
            binUIbag.GetComponent<BinBagUI>().noFire();
            songManager.instance.AKFireLose();
        }
        else
        songManager.instance.AKFireing();
        Lost();
        clickMan();
    }
    public FirePackge FireEnd(List<Bin> bi, List<Bin> en)
    {
        FirePackge pack = new FirePackge();
        int bHP = 0, eHP = 0, bYouShi = 0, eYouShi = 0;
        
        foreach (var a in bi)
        {
            bYouShi += a.youshi;
        }
        bYouShi += chilkYouSHi(bi,pack.bYouShi);
        bHP = bHpMax-bHpCost;
        pack.bHpMax = bHpMax;
        pack.eHpMax = eHpMax;
        pack.bYouShi.Add("总兵力优势", bYouShi);
        foreach (var a in en)
        {
            eYouShi += a.youshi;
        }
        eYouShi += chilkYouSHi(en,pack.eYouShi);
        eHP =eHpMax- eHpCost;
        pack.eHp = eHP;
        pack.bHp = bHP;
        pack.eYouShi.Add("总兵力优势", eYouShi);
        pack.bYou = bYouShi;
        pack.eYou = eYouShi;
        if (eYouShi < bYouShi)
        {
            int c = bYouShi - eYouShi;
            binWin(eHpMax - eHP + c);
            pack.eHp = eHpMax - eHP - c;
            pack.cost = c;
            pack.iswin = true;
            tanchu("造成" + c + "伤害");
        }
        else if (eYouShi == bYouShi)
        {
            jiangchi();
            pack.iswin = false;
        }
        else
        {
            int c = eYouShi - bYouShi;
            enemyWin(bHpMax - bHP + c);
            pack.bHp = bHpMax - bHP - c;
            pack.cost = c;
            tanchu("受到" + c + "伤害");
            pack.iswin = false;
        }
        return pack;
    }

    public int chilkYouSHi(List<Bin> bins, Dictionary<string, int> YouShi)
    {
        int k = 0;
        if (bins.Count == 0)
            return k;
        bool isbin = !bins[0].emeny;
        if (isbin&&ischeng)
        {
            k += 1;
        }
        return k;
    }
    public void jiangchi()
    {
        isYouShi = false;
        tanchu("僵持");
        songManager.instance.AKFireing();
    }


    public void Lost()
    {
        if(ischeng&&bins.Count==0&& teams.Count == 0)
        {
            Debug.Log("lose");
        }
    }
}
[Serializable]
public class Road
{
    public int juli;//距离
    
    public RoadPoint roadPoint;
    
}