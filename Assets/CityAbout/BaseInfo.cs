using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum Basestate
{
    Normal,beAttack
}
[CreateAssetMenu(menuName = "Scriptable Objects/Base")]
public class BaseInfo : ScriptableObject
{
    public int Level;
    Basestate currentState;
    public float BIC;

}
