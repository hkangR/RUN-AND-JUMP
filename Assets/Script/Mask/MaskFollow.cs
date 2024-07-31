using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MaskFollow : MonoBehaviour
{
    [SerializeField] private Vector3 originalpos;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform camera1;
    [SerializeField] private Vector3 distance;

    [SerializeField] private GameObject parent;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player1").transform;
        camera1 = GameObject.Find("Camera1").transform;
        mainCamera = GameObject.Find("Main Camera").transform; 
        
        parent = GameObject.Find("MaskParent");
    }

    // Start is called before the first frame update
    void Start()
    {
        originalpos = transform.position;
        transform.Rotate(90,90,-90);
        
        distance = mainCamera.position - camera1.position;
        transform.position = distance + originalpos;
        
        transform.SetParent(parent.transform);
    }




}
