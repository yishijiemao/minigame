using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binManager : MonoBehaviour
{
    public List<Bin> bin;
    public static binManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void addBin(Bin point)
    {
        bin.Add(point);

    }
    public void dead(Bin point)
    {
        bin.Remove(point);
    }
}
