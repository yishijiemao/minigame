using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class chengbao : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public List<Bin> bins;
    public RoadPoint roadPoint;
    public bool iszhihuiguan;
    public GameObject chengbaoUI;
    public float zhiyuan;
    public float addzhiyuan;
    public CHENG cheng;
    public bool isInit;
    public bool fengHuoTai;
    private  void Start()
    {
       
        EventCenter.GetInstance().EventTrigger<chengbao>("chengbaoInit", this);
    }

    private void Update()
    {
        
    }

    public  void OnPointerClick(PointerEventData eventData)
    {
        ClickUI.instance.display(getGagck());
        EventCenter.GetInstance().EventTrigger<Vector3>("CamMoveToPoint", transform.position);
        
    }

    public ClickPagck getGagck()
    {
        
        

        ClickPagck pagck = new ClickPagck();
        pagck.str = "城堡";
        pagck.Object = this.gameObject;
        if (iszhihuiguan)
        {
            pagck.type = TypeClick.cheng;
        }
        else
        {
            pagck.type = TypeClick.noActivation;
        }
        if(isInit)
        {
            pagck.type = TypeClick.initZhiKui;
        }
        
        return pagck;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        chengbaoUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        chengbaoUI.SetActive(false);
    }

    public void creartBin(Bin bin)
    {
        GameObject @object = Instantiate(bin.obj, transform.position,Quaternion.identity);
        roadPoint.tanchu("资源-" + bin.cost);
        
    }

    public void addBin(Bin bin)
    {
        Bin bbb = bin;
        bins.Add(bbb);
        
    }
    public void checkZhuiHui()
    {
        if (roadPoint.ZhiHui.Count > 0)
            iszhihuiguan = true;
        else
            iszhihuiguan = false;
    }
    public void upChenChi()
    {
        zhiyuan += addzhiyuan;
    }

    public float takeZhiYuan()
    {
        float a= Mathf.Floor(zhiyuan);
        roadPoint.tanchu("资源+" + a);
        zhiyuan -= a;
        return a;
    }
}
