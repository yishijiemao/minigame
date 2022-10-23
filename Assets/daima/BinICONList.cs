using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinICONList : MonoBehaviour
{
    public GameObject @object;
    public Transform bag;
   

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener<List<BinICONPager>>("creatBinICON", creatBinICON);        
    }
    

    public void creatBinICON(List<BinICONPager> bins)
    {
        int j = 0;
        while (j < bag.childCount)
        {

            Destroy(bag.GetChild(j++).gameObject);

        }
        for (int i = 0; i < bins.Count; i++)
        {
            var ccc = Instantiate(@object, bag).GetComponent<BinICON>();
            ccc.text.text = bins[i].bin.namE;
            ccc.image.sprite = bins[i].bin.Ui;
            if (bins[i].isTeam)
            {
                string c="";
                foreach(var a in bins[i].team)
                {
                    c += a.namE + "  ";
                }
                ccc.text1.text = c;
            }
            else
            ccc.text1.text = bins[i].bin.type.ToString();
            ccc.@object = bins[i].@object;
            if(bins[i].bin.emeny)
            {
                ccc.image.color = Color.red;
            }


        }
        

    }

    
}
public class BinICONPager
{
    public GameObject @object;
    public Bin bin;
    public bool isTeam;
    public List<Bin> team;
    public BinICONPager(GameObject game, Bin bins)
    {
        @object = game;
        bin = bins;
    }
}