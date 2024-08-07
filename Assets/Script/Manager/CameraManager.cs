using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Camera mainCamera;
    public Camera camera1;
    public VirtualCamera virtualCamera; 

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
}
