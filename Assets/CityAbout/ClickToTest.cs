using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToTest : MonoBehaviour
{
    public GameObject PrefabShow;
    public Canvas cccc;
    // Start is called before the first frame update
   
    public void CreateAshowTip(float num)
    {
        NumberTipShow ret = Instantiate(PrefabShow, this.transform.position, Quaternion.identity, cccc.transform).GetComponent<NumberTipShow>();
        Debug.Log("1111");
        ret.showUINumberTip(num);
    }
}
