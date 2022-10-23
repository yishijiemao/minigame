using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu][Serializable]
public class Bin :ScriptableObject
{
    public string namE;
    public int youshi;
    public int Hp;
    public float jiaoli;
    public TypeOfBin type;
    public GameObject obj;
    public Sprite Ui;
    public bool emeny;
    public List<Bin> nextBins;
    public int cost;
}

public enum TypeOfBin
{
    jingwei,yuancheng,qibing,general,¾üÍÅ,¶Ü
}