using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMRotation : MonoBehaviour
{
    public Transform target;
    public Transform targetTO;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 20;//����
    public float minDistance = .6f;//����
    public float xSpeed = 200.0f;//�ٶ�
    public float ySpeed = 200.0f;
    public int yMinLimit = -80;//�޶��Ƕ�
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float zoomDampening = 5.0f;
    public float xDeg = 0.0f;//����ĽǶȼ�¼
    public float yDeg = 0.0f;//����ĽǶ�
    public float currentDistance;//���ż�¼
    public float desiredDistance;//����
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    public Quaternion rotation;
    private Vector3 position;

    public float X;//��ʼ���Ƕ�X��
    public float Y;//��ʼ���Ƕ�Y��
    /// <summary>
    /// ��ʼ������
    /// </summary>
    public float CameDistance;
    void Start()
    {
        Init();
        desiredDistance = CameDistance;
        yDeg = X;
        xDeg = Y;
    }
    void OnEnable()
    {
        Init();
        desiredDistance = CameDistance;
        yDeg = X;
        xDeg = Y;

    }

    public void Init()
    {

        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;


        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }


    void LateUpdate()
    {

        if (Input.GetMouseButton(1))
        {
            xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }
        yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
        // ���������ת

        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
        currentRotation = transform.rotation;


        rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
        transform.rotation = rotation;



        // Ӱ��scrollwheel�佹����
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        //�佹��С/���
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        //ƽ���佹
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);


        target.position = Vector3.Lerp(target.position, targetTO.position, Time.deltaTime * 5);


        position = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
        transform.position = position;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
