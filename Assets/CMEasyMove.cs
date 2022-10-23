using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMEasyMove : MonoBehaviour
{
    public Transform taget;
    public float radius;
    public float AngularSpeed;

    private float angled;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 POS = taget.rotation * Vector3.forward * radius;
        transform.position = new Vector3(POS.x, taget.position.y, POS.z);
    }

    // Update is called once per frame
    void Update()
    {
        angled += (AngularSpeed * Time.deltaTime) % 360;
        float posX = radius * Mathf.Sin(angled * Mathf.Deg2Rad);
        float posZ = radius * Mathf.Sin(angled * Mathf.Deg2Rad);

        transform.position = new Vector3(posX, posZ) + taget.position;
    }
}
