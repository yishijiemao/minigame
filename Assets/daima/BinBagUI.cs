using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BinBagUI : MonoBehaviour
{
    public GameObject Fire;
    public Text text;
    public Text text1;
    public GameObject man;
    public GameObject cheng;
    public Text chengText;
    public Text chengPass;
    public GameObject zhiHui;
    public GameObject fengHuo;
    public void isFire()
    {
        Fire.SetActive(true);
    }

    public void noFire()
    {
        Fire.SetActive(false);
    }
}
