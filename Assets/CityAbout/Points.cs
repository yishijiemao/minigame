using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {

        collision.GetComponent<SimpleAIMovement>().times--;
        Debug.Log(collision.name);
    }

}
