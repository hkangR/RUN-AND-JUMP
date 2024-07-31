using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Camera mainCamera;
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
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
}
