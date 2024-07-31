using UnityEngine;
public class CameraFollower : MonoBehaviour {
    // 目标相机
    public Transform targetCamera;
    // 初始相对位置
    private Vector3 initialOffset;
    // 初始相对旋转
    private Quaternion initialRotationOffset;
    // 是否追踪绝对位置
    public bool followAbsolutePosition = false;

    void Start() 
    {
        // 计算初始相对位置和旋转
        initialOffset = transform.position - targetCamera.position;
        initialRotationOffset = Quaternion.Inverse(targetCamera.rotation) * transform.rotation;
    }

    void LateUpdate()
    {
        if (followAbsolutePosition) 
        {
            // 更新为目标相机的绝对位置和旋转
            transform.position = targetCamera.position;
            transform.rotation = targetCamera.rotation;
        }
        else 
        {
            // 更新为相对位置和旋转
            transform.position = targetCamera.position + initialOffset;
            transform.rotation = targetCamera.rotation * initialRotationOffset;
        }
    }
}