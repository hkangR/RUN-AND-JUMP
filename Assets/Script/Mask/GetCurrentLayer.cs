using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetCurrentLayer : MonoBehaviour
{
    public Vector3 startPoint; // �߶����
    public Vector3 endPoint; // �߶��յ�
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform camera1;
    [SerializeField] private Vector3 distanceVec3;
    public int intersectionCount;

    void Start()
    {
        camera1 = GameObject.Find("Camera1").transform;
        mainCamera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        CheckIntersections();
    }

    void CheckIntersections()
    {
        distanceVec3 = mainCamera.position - camera1.position;
        startPoint = transform.position + distanceVec3;
        endPoint = transform.position + distanceVec3;
        //д����ԭ��������mesh����ײ��ֲ������λ�ã�������������ⲿ����(����mesh���꣩
        startPoint.z = -47;
        endPoint.z = -50;

        Vector3 direction = endPoint - startPoint;
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(startPoint, direction.normalized, distance);
        intersectionCount = hits.Length;
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint, endPoint);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPoint, 0.1f);
        Gizmos.DrawSphere(endPoint, 0.1f);
        
    }
}