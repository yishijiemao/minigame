using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FengHuoUI : MonoBehaviour
{

    public CanvasGroup group;
    
    private void Start()
    {
        EventCenter.GetInstance().AddEventListener("GouHuo", display);
    }
    public void display()
    {
        group.alpha = 1;
        StartCoroutine(colorfade());
    }

    IEnumerator colorfade()
    {
        yield return new WaitForSeconds(2f);
        float dura = 2f;
        float i = 1;
        while (i > 0.01f)
        {
            i = i - 1 / dura * Time.deltaTime;
            group.alpha = i;
            Debug.Log(i);
            yield return 1;
        }
        
    }
    


}
