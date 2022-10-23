using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class birdObj : MonoBehaviour
{

    public RoadPoint endPoint;
    public int y;

     public void fly()
    {
        Vector3 pos = (this.transform.position + (endPoint.transform.position - this.transform.position)/2);
        pos.y += y;
        transform.DOMove(pos, 1).OnComplete(()=> { transform.DOMove(endPoint.transform.position,1).OnComplete(()=> { EventCenter.GetInstance().EventTrigger("birdIN");endPoint.BirdIn();Destroy(gameObject); }); });
    }
}
