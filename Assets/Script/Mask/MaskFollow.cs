using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MaskFollow : MonoBehaviour
{
    private Renderer renderer;
    public Material materialInstance;
    
    [SerializeField] private Vector3 originalpos;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform camera1;
    [SerializeField] private Vector3 distance;

    [SerializeField] private GameObject parent;
    private float creationTime;

    private void Awake()
    {
        playerTransform = GlobalManager.instance.transform;
        camera1 = CameraManager.instance.camera1.transform;
        mainCamera = CameraManager.instance.mainCamera.transform; 
        
        parent = GameObject.Find("MaskParent");
        renderer = gameObject.GetComponent<Renderer>();
        materialInstance = renderer.material;

        creationTime = Time.time;
        materialInstance.SetFloat("_CreationTime", creationTime);
    }
    
    void Start()
    {
        originalpos = transform.position;
        transform.Rotate(90,90,-90);
        
        distance = mainCamera.position - camera1.position;
        transform.position = distance + originalpos;
        
        transform.SetParent(parent.transform);
    }

    void Update()
    {
        float currentTime = Time.time;
        materialInstance.SetFloat("_CurrentTime", currentTime);
    }
}