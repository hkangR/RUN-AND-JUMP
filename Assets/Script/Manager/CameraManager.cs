using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Camera mainCamera => GameObject.Find("Main Camera").GetComponent<Camera>();
    public Camera camera1;
    public VirtualCamera virtualCamera;

    public GameObject virtualCamera1;
    public GameObject virtualCamera2;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    private void LateUpdate()//防止相机震动修改rotation
    {
        camera1.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    
    public void SwitchToCamera1()
    {
        virtualCamera1.SetActive(true);   // 激活目标相机
        virtualCamera2.SetActive(false);  // 禁用其他相机
        virtualCamera = virtualCamera1.GetComponent<VirtualCamera>();
    }

    public void SwitchToCamera2()
    {
        virtualCamera1.SetActive(false);  // 禁用其他相机
        virtualCamera2.SetActive(true);   // 激活目标相机
        virtualCamera = virtualCamera2.GetComponent<VirtualCamera>();
    }
}
