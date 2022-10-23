using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMSystem : MonoBehaviour
{
    Transform CMtransform;
    float Speed=50.0f;
    // Start is called before the first frame update
    void Start()
    {
        CMtransform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 InputDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) InputDir.z = -1.0f;
        if (Input.GetKey(KeyCode.S)) InputDir.z = +1.0f;
        if (Input.GetKey(KeyCode.A)) InputDir.x = +1.0f;
        if (Input.GetKey(KeyCode.D)) InputDir.x = -1.0f;
        Vector3 moveDir = transform.forward * InputDir.z + transform.right*InputDir.x;
        CMtransform.position += moveDir * Speed * Time.deltaTime;
    }
}
