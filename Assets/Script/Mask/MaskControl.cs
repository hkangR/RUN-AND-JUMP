using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskControl : MonoBehaviour
{
    [SerializeField] private Vector3 originalpos;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform camera1;
    [SerializeField] private Vector3 distance;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player1").transform;
        camera1 = GameObject.Find("Camera1").transform;
        mainCamera = GameObject.Find("Main Camera").transform; 
    }

    private void Start()
    {
        originalpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = mainCamera.position - camera1.position;
        transform.position = distance + originalpos;
    }
}
