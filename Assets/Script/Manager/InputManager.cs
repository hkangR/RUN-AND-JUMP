using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    
    public Dictionary<string, KeyCode> keyMappings;
    public Dictionary<string, string> axisMappings;

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
    
    private void Start()
    {
        keyMappings = new Dictionary<string, KeyCode>
        {
            { "Jump", KeyCode.Space },
            { "Attack", KeyCode.Mouse0 },
            { "Dash", KeyCode.LeftShift },
        };
        
        axisMappings = new Dictionary<string, string>
        {
            { "Horizontal", "Horizontal" },
            { "Vertical", "Vertical" }
        };
    }
    
    // 更新键绑定方法
    public void UpdateKeyBinding(string action, KeyCode newKey)
    {
        if (keyMappings.ContainsKey(action))
        {
            keyMappings[action] = newKey;
        }
    }

    // 更新轴绑定方法
    public void UpdateAxisBinding(string action, string newAxis)
    {
        if (axisMappings.ContainsKey(action))
        {
            axisMappings[action] = newAxis;
        }
    }
    
}
