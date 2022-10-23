using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireUI : MonoBehaviour
{

    public GameObject UI;
    public Text binHp;
    public Text enemyHp;
    public Text binYouShi;
    public Text enemyYouShi;
    public Transform binBag;
    public Transform enemyBag;
    public GameObject youshi;
    public Text text;
    public Text text1;
    public Transform bag;
    public List<FirePackge> packges;
    public GameObject @object;
    public GameObject UI2;
    float cost;
    bool iswin;
    bool isplay;
    private void Start()
    {
        EventCenter.GetInstance().AddEventListener<List<FirePackge>>("getFireUI", getDisplay);
        EventCenter.GetInstance().AddEventListener<FirePackge>("FireUIDisplay", display);
    }
    public void getDisplay(List<FirePackge> pack)
    {

        packges = pack;
      
    }
    
    public void open()
    {
        if (packges == null)
            return;
        if (packges.Count == 0)
            return;
        UI.SetActive(true);
        UI2.SetActive(false);
        int b = 0;
        while (b < bag.childCount)
        {
            Destroy(bag.GetChild(b++).gameObject);
        }
        
       for(int i=0;i<packges.Count;i++)
       {
            var a = Instantiate(@object, bag).GetComponent<fireUIICON>();
            a.text.text = "战役" + (i + 1).ToString();
            a.packge = packges[i];
       }
    }


    public void display(FirePackge packge)
    {
        
        UI2.SetActive(true);
        int i = 0;
        while (i < binBag.childCount)
        {

            Destroy(binBag.GetChild(i++).gameObject);

        }
        i = 0;
        while (i < enemyBag.childCount)
        {

            Destroy(enemyBag.GetChild(i++).gameObject);

        }
        binHp.text = packge.bHp.ToString() + "/" + packge.bHpMax.ToString();
        enemyHp.text = packge.eHp.ToString() + "/" + packge.eHpMax.ToString();
        binYouShi.text = packge.bYou.ToString();
        enemyYouShi.text = packge.eYou.ToString();

        foreach (var a in packge.bYouShi)
        {
            creatYouShi(a.Key, a.Value, true);

        }
        foreach (var a in packge.eYouShi)
        {
            creatYouShi(a.Key, a.Value, false);

        }
        if (packge.iswin)
        {

            text.gameObject.SetActive(true);
            text1.gameObject.SetActive(true);
            text.text = "胜利";
            text1.text = "造成" + packge.cost + "点伤害";
            cost = (float)packge.cost / (float)packge.eHpMax * 100;
            Debug.Log(cost);
            iswin = true;
            isplay = true;
        }
        else
        {
            if (cost == 0)
            {
                text.gameObject.SetActive(true);
                text.text = "僵持";
            }
            else
            {
                text.gameObject.SetActive(true);
                text1.gameObject.SetActive(true);
                text.text = "失败";
                text1.text = "受到" + packge.cost + "点伤害";
                cost = (float)packge.cost / (float)packge.bHpMax * 100;
                Debug.Log(cost);
                iswin = false;
                isplay = true;
            }
        }
    }
   
    public void clone()
    {
        isplay = false;
        
        text.gameObject.SetActive(false);
        text1.gameObject.SetActive(false);
        UI.SetActive(false);
        int i = 0;
        while (i < binBag.childCount)
        {

            Destroy(binBag.GetChild(i++).gameObject);
           
        }
        i = 0;
        while (i < enemyBag.childCount)
        {

            Destroy(enemyBag.GetChild(i++).gameObject);

        }
        
    }

    public void creatYouShi(string str,int zhi,bool isBin)
    {

        Transform b;
        if (isBin)
            b = binBag;
        else
            b = enemyBag;
        GameObject @object = Instantiate(youshi, b);
        @object.GetComponent<YouShiShiUI>().text.text = str;
        @object.GetComponent<YouShiShiUI>().text1.text = zhi.ToString();
    }


}


public class FirePackge
{
    public int bHp, eHp, bHpMax, eHpMax, cost,bYou,eYou;
    public bool iswin;
    public Dictionary<string, int> bYouShi= new Dictionary<string, int>();
    public Dictionary<string, int> eYouShi=new Dictionary<string, int>();
}