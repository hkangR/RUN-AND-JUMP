using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskControl : MonoBehaviour
{
    [SerializeField] private Vector3 originalpos;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform camera1;
    [SerializeField] private Vector3 distance;

    private void Awake()
    {
    }

    private void Start()
    {
        camera1 = CameraManager.instance.camera1.transform;
        mainCamera = CameraManager.instance.mainCamera.transform; 
        originalpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = mainCamera.position - camera1.position;
        transform.position = distance + originalpos;
    }
}
