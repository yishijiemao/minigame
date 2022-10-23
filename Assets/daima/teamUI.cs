using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class teamUI : MonoBehaviour
{
    public GameObject @object;
    public Transform pag;
    public GameObject UI;
    public List<BinOBJ> bins=new List<BinOBJ>();
    public TeamPacge pac;
    private void Start()
    {
        EventCenter.GetInstance().AddEventListener<TeamPacge>("teamUIdisplay", display);
    }
    public void display(TeamPacge pacge)
    {
        bins.Clear();
        UI.SetActive(true);
        pac = pacge;
        int i = 0;
        while (i < pag.childCount)
        {
            Destroy(pag.GetChild(i++).gameObject);
        }
        foreach(var a in pacge.bins)
        {
            var c= Instantiate(@object, pag).GetComponent<TeamIcon>();
            c.text.text = a.bin.namE;
            c.text1.text = a.bin.type.ToString();
            c.uI = this;
            c.oBJ = a;
            c.image.sprite = a.bin.Ui;

        }
    }
    public void add(BinOBJ bin)
    {
        bins.Add(bin);
    }
    public void dis(BinOBJ bin)
    {
        bins.Remove(bin);
    }
    public void end()
    {
        pac.point.creartTeam(bins);
        UI.SetActive(false);
    }

}
public class TeamPacge
{
    public List<BinOBJ> bins=new List<BinOBJ>();
    public RoadPoint point;
}