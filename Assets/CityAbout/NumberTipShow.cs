using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumberTipShow : MonoBehaviour
{

    public Text tipNumber;
    public float Timer;
    public float UpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Timer);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, UpSpeed*Time.deltaTime, 0);
    }

    public void showUINumberTip(float num)
    {
        tipNumber.text = num.ToString();
    }
}
