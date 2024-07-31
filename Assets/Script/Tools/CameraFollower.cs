using UnityEngine;
public class CameraFollower : MonoBehaviour {
    // Ŀ�����
    public Transform targetCamera;
    // ��ʼ���λ��
    private Vector3 initialOffset;
    // ��ʼ�����ת
    private Quaternion initialRotationOffset;
    // �Ƿ�׷�پ���λ��
    public bool followAbsolutePosition = false;

    void Start() 
    {
        // �����ʼ���λ�ú���ת
        initialOffset = transform.position - targetCamera.position;
        initialRotationOffset = Quaternion.Inverse(targetCamera.rotation) * transform.rotation;
    }

    void LateUpdate()
    {
        if (followAbsolutePosition) 
        {
            // ����ΪĿ������ľ���λ�ú���ת
            transform.position = targetCamera.position;
            transform.rotation = targetCamera.rotation;
        }
        else 
        {
            // ����Ϊ���λ�ú���ת
            transform.position = targetCamera.position + initialOffset;
            transform.rotation = targetCamera.rotation * initialRotationOffset;
        }
    }
}