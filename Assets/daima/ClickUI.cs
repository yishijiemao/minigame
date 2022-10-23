using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickUI : MonoBehaviour
{

    public static ClickUI instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance = null)
            {
                Destroy(gameObject);
            }
        }
    }


    private void Start()
    {
        EventCenter.GetInstance().AddEventListener("ICCreartbin", ICCreartbin);
        EventCenter.GetInstance().AddEventListener("ICCreartbin0", ICCreartbin0);
        EventCenter.GetInstance().AddEventListener("ICCreartbin1", ICCreartbin1);
        EventCenter.GetInstance().AddEventListener("ICCreartbin2", ICCreartbin2);
        EventCenter.GetInstance().AddEventListener("ICCreartbin3", ICCreartbin3);
        EventCenter.GetInstance().AddEventListener("ICinitZhiKui", ICinitZhiKui);
        EventCenter.GetInstance().AddEventListener("ClickUIClean", cleanUI);
        EventCenter.GetInstance().AddEventListener("ICOrder", ICOrder);
        EventCenter.GetInstance().AddEventListener("ICZhiKuiOrder", ICZhiKuiOrder);
        EventCenter.GetInstance().AddEventListener("orderOver", orderOver);
        EventCenter.GetInstance().AddEventListener("ZhiKuiOrderOver", orderOver);
        EventCenter.GetInstance().AddEventListener("ICTeam", ICTeam);
        EventCenter.GetInstance().AddEventListener("ICBack", ICBack);
        EventCenter.GetInstance().AddEventListener("GouHuo", GouHuo);
        EventCenter.GetInstance().AddEventListener("GouHuoEnd", GouHuoEnd);
        EventCenter.GetInstance().AddEventListener("ICTeamOut", ICTeamOut);
        EventCenter.GetInstance().AddEventListener("ICLevelUP", ICLevelUP);
        EventCenter.GetInstance().AddEventListener("isYourTurn", isYourTurn);
        EventCenter.GetInstance().AddEventListener("notYourTurn", notYourTurn);
        EventCenter.GetInstance().AddEventListener("birdIN", birdIn);
        EventCenter.GetInstance().AddEventListener("isBird", isBird);
        EventCenter.GetInstance().AddEventListener("ICHelp", ICHelp);
    }
    public Text text;
    public GameObject clickICON;
    public Transform ICONbag;
    public GameObject nowOnClick;
    public ClickPagck nowPageck;
    public Bin zhikui;
    public bool isGouHuo;
    public List<Bin> bins;
    public bool isorder;
    public bool isyourturn;
    public bool isbird;
    public void display(ClickPagck pagck)
    {
        if (isorder||pagck==null||isbird)
            return;


        cleanICON();
        nowPageck = pagck;
        text.text = pagck.str;
        nowOnClick = pagck.Object;

        switch (pagck.type)
        {
           
            case TypeClick.bin:
               
                
                if (pagck.Object.GetComponent<TeamOBJ>() != null)
                {
                    songManager.instance.AKClickJuntuan();
                }
                else
                {
                   switch(pagck.Object.GetComponent<BinOBJ>().bin.type)
                    {
                        case TypeOfBin.jingwei:
                            songManager.instance.AKClickjian();
                            break;
                        case TypeOfBin.盾:
                            songManager.instance.AKClickdun();
                            break;
                        case TypeOfBin.yuancheng:
                            songManager.instance.AKClickgong();
                            break;
                        case TypeOfBin.qibing:
                            songManager.instance.AKClickma();
                            break;
                    }
                }
                break;
            case TypeClick.cheng:
                if (!pagck.Object.GetComponent<RoadPoint>().isFire)
                {
                    songManager.instance.AKClickNoFirecheng();
                }
                else
                    songManager.instance.AKClickInFirePoint();

                break;
            case TypeClick.zhihui:
                songManager.instance.AKClickzhihui();
                break;
            case TypeClick.noActivation:
                break;
        }
        if (nowOnClick.GetComponent<CamImage>())
        {
            EventCenter.GetInstance().EventTrigger<Transform, Transform>("CamTo", nowOnClick.GetComponent<CamImage>().tra, nowOnClick.GetComponent<CamImage>().look);
        }
        if(isGouHuo)
        {
            if(pagck.type==TypeClick.bin)
            {
                if (nowOnClick.GetComponent<BinOBJ>())
                if (nowOnClick.GetComponent<BinOBJ>().isGouHuo)
                {
                    createICON("支援", "ICHelp");
                    var a = nowOnClick.GetComponent<BinOBJ>().nowcheng.GetComponent<RoadPoint>();
                    RoadManager.instance.disRoad(RoadManager.instance.GouHuoPoint.findRoad(a));
                }
                if (nowOnClick.GetComponent<TeamOBJ>())
                if (nowOnClick.GetComponent<TeamOBJ>().isGuoHuo)
                {
                    createICON("支援", "ICHelp");
                    var a = nowOnClick.GetComponent<TeamOBJ>().nowcheng.GetComponent<RoadPoint>();
                    RoadManager.instance.disRoad(RoadManager.instance.GouHuoPoint.findRoad(a));
                }
                    
            }
            return;
        }
        switch (pagck.type)
        {
            case TypeClick.initZhiKui:
                createICON("kongming", "ICinitZhiKui");
                isyourturn = true;
                break;
            case TypeClick.bin:
                createICON("命令", "ICOrder");
                if (pagck.Object.GetComponent<TeamOBJ>() == null)
                {
                    createICON("组队", "ICTeam");
                    if (pagck.Object.GetComponent<BinOBJ>().isCheng&& pagck.Object.GetComponent<BinOBJ>().bin.nextBins.Count>0)
                        createICON("升级", "ICLevelUP");
                }
                else
                    createICON("解散", "ICTeamOut");
                break;
            case TypeClick.cheng:
                if(isyourturn)
                {
                    createICON("升级", "ICchengLevelUP");
                    createICON("造兵", "ICCreartbin"); 
                    createICON("建造", "ICBuild");

                }
                
                break;
            case TypeClick.zhihui:
                if (isyourturn)
                {
                    createICON("命令", "ICZhiKuiOrder");
                }
                break;
            case TypeClick.noActivation:
                break;
        }
    }
    public void ICHelp()
    {
        if (nowOnClick.GetComponent<BinOBJ>())
        {
            nowOnClick.GetComponent<BinOBJ>().getRoadsToZhiYuan(RoadManager.instance.GouHuoPoint.findRoad(nowOnClick.GetComponent<BinOBJ>().nowcheng.GetComponent<RoadPoint>()));
        }
        if (nowOnClick.GetComponent<TeamOBJ>())
        {
            nowOnClick.GetComponent<TeamOBJ>().getRoadsToZhiYuan(RoadManager.instance.GouHuoPoint.findRoad(nowOnClick.GetComponent<TeamOBJ>().nowcheng.GetComponent<RoadPoint>()));
        }
    }
    public void cleanICON()
    {
        int i = 0;
        while (i < ICONbag.childCount)
        {
            Destroy(ICONbag.GetChild(i++).gameObject);
        }
    }
    public void GouHuoEnd()
    {
        isGouHuo = false;
    }
    public void GouHuo()
    {
        isGouHuo = true;
    }
    public void ICTeam()
    {
        nowOnClick.GetComponent<BinOBJ>().nowcheng.GetComponent<RoadPoint>().teamUI();
        songManager.instance.AKClickzujianTeam();
    }
    public void cleanUI()
    {
        text.text = "名字";
        nowPageck = null;
        nowOnClick = null;
        int i = 0;
        while (i < ICONbag.childCount)
        {
            Destroy(ICONbag.GetChild(i++).gameObject);
        }
    }
    public void ICOrder()
    {
        songManager.instance.AKClickOrder();
        if (nowOnClick.GetComponent<BinOBJ>() != null)
        {
            var a = nowOnClick.GetComponent<BinOBJ>();
            a.nowcheng.GetComponent<RoadPoint>().next();
            EventCenter.GetInstance().EventTrigger("LockOrder");
            isorder = true;
            cleanICON();
            createICON("返回", "ICOrderCallOff");
        }
       if(nowOnClick.GetComponent<TeamOBJ>()!=null)
        {
            var a = nowOnClick.GetComponent<TeamOBJ>();
            a.nowcheng.GetComponent<RoadPoint>().next();
            EventCenter.GetInstance().EventTrigger("LockOrder");
            isorder = true;
            cleanICON();
            createICON("返回", "ICOrderCallOff");
        }
    }
    public void ICLevelUP()
    {
        if(FSM.instance.costZiYuan(nowOnClick.GetComponent<BinOBJ>().bin.nextBins[0].cost))
        {
            songManager.instance.AKBinLevelUp();
            nowOnClick.GetComponent<BinOBJ>().LevelUp();
            cleanUI();
        }
        
    }
    public void isYourTurn()
    {
        isyourturn = true;
    }
    public void notYourTurn()
    {
        isyourturn = false;
    }
    public void ICTeamOut()
    {
        nowOnClick.GetComponent<TeamOBJ>().TeamBreak();
        cleanUI();
    }
    public void birdIn()
    {
        isbird = false;

    }
    public void orderOver()
    {
        if (nowPageck.type==TypeClick.zhihui)
        {
           var a = nowOnClick.GetComponent<General>();
            a.getRoads();
        }
        else
        {
            if (nowOnClick.GetComponent<BinOBJ>()!=null)
            {
                var a = nowOnClick.GetComponent<BinOBJ>();
                a.getRoads();
            }
            else if(nowOnClick.GetComponent<TeamOBJ>() != null)
            {
                var a = nowOnClick.GetComponent<TeamOBJ>();
                a.getRoads();
            }

        }
        cleanUI();
        isorder = false;
        EventCenter.GetInstance().EventTrigger("orderNextOver");
    }
    public void ICCreartbin()
    {


        cleanICON();
        createICON("剑", "ICCreartbin0");
        createICON("盾", "ICCreartbin1");
        createICON("弓", "ICCreartbin2");
        createICON("马", "ICCreartbin3");
        createICON("返回", "ICBack");
    }
    public void ICBack()
    {

        var pagck = nowPageck;
        cleanICON();
        switch (pagck.type)
        {
            case TypeClick.bin:
                createICON("命令", "ICOrder");
                if (pagck.Object.GetComponent<TeamOBJ>() == null)
                {
                    createICON("组队", "ICTeam");
                    if (pagck.Object.GetComponent<BinOBJ>().isCheng)
                        createICON("升级", "ICLevelUP");
                }
                else
                    createICON("解散", "ICTeamOut");
                break;
            case TypeClick.cheng:
                createICON("升级", "ICchengLevelUP");
                createICON("造兵", "ICCreartbin");
                createICON("建造", "ICBuild");
                break;
            case TypeClick.zhihui:
                createICON("命令", "ICZhiKuiOrder");
                break;
            case TypeClick.noActivation:
                break;
        }
    }

    public void isBird()
    {
        if (nowOnClick==null)
            return;
        isbird = true;
        
        if(nowOnClick.GetComponent<RoadPoint>()!=null)
        {
            nowOnClick.GetComponent<RoadPoint>().isBird();
        }
        else if(nowOnClick.GetComponent<BinOBJ>() != null)
        {
            nowOnClick.GetComponent<BinOBJ>().nowcheng.GetComponent<RoadPoint>().isBird();
        }
        else if (nowOnClick.GetComponent<General>() != null)
        {
            nowOnClick.GetComponent<General>().nowcheng.GetComponent<RoadPoint>().isBird();
        }
        else if (nowOnClick.GetComponent<TeamOBJ>() != null)
        {
            nowOnClick.GetComponent<TeamOBJ>().nowcheng.GetComponent<RoadPoint>().isBird();
        }
        else
        {
            isbird = false;
            
        }
    }

    public void ICCreartbin0()
    {
        
        if (FSM.instance.costZiYuan(bins[0].cost))
        {
            songManager.instance.AKCreateBin();
            nowOnClick.GetComponent<chengbao>().creartBin(bins[0]);
            nowOnClick.GetComponent<RoadPoint>().tanchu("资源-" + bins[0].cost);
        }
    }
    public void ICCreartbin1()
    {
        
        if (FSM.instance.costZiYuan(bins[1].cost))
        {
            songManager.instance.AKCreateBin();
            nowOnClick.GetComponent<chengbao>().creartBin(bins[1]);
            nowOnClick.GetComponent<RoadPoint>().tanchu("资源-" + bins[1].cost);
        }
    }
    public void ICCreartbin2()
    {
        
        if (FSM.instance.costZiYuan(bins[2].cost))
        {
            songManager.instance.AKCreateBin();
            nowOnClick.GetComponent<chengbao>().creartBin(bins[2]);
            nowOnClick.GetComponent<RoadPoint>().tanchu("资源-" + bins[2].cost);
        }
    }
    public void ICCreartbin3()
    {
        if (FSM.instance.costZiYuan(bins[3].cost))
        {
            songManager.instance.AKCreateBin();

            nowOnClick.GetComponent<chengbao>().creartBin(bins[3]);
            nowOnClick.GetComponent<RoadPoint>().tanchu("资源-" + bins[3].cost);
        }
    }

    public void ICZhiKuiOrder()
    {
        songManager.instance.AKClickOrder();
        var a = nowOnClick.GetComponent<General>();
        a.nowcheng.GetComponent<RoadPoint>().next();
        EventCenter.GetInstance().EventTrigger("LockOrder");
        isorder = true;
        cleanICON();
        createICON("返回", "ICOrderCallOff");
    }
    public void ICinitZhiKui()
    {
        nowOnClick.GetComponent<chengbao>().creartBin(zhikui);
        EventCenter.GetInstance().EventTrigger("initZhiKuiClone");
        EventCenter.GetInstance().EventTrigger<string>("disLockStr", "选择玩家出生城池");
        nowPageck.type = TypeClick.cheng;
        display(nowPageck);
    }

    public void createICON(string str,string fun)
    {
        Sprite sprite = (Sprite)Resources.Load($"image/ui/{str}", typeof(Sprite));
       
        GameObject @object = Instantiate(clickICON, ICONbag);
        @object.GetComponent<ClickICONUI>().fun = fun;
        @object.GetComponent<ClickICONUI>().image.sprite = sprite;
    }


}

    public class ClickPagck
    {
        public string str;
        public TypeClick type;
        public GameObject Object;
    }
    public enum TypeClick
    {
        bin, zhihui, cheng, rondPoint, eneny, noActivation,initZhiKui
    } 