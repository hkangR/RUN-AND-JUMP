using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager instance;
    
    public Dictionary<string, KeyCode> keyMappings;
    public Dictionary<string, string> axisMappings;
    public Player player;
    public Transform spawnPoint;
    public List<Collider2D> jumpPlanes;
    public DeadmenuController deadmenuController;
    public VictorymenuController victorymenuController;

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
        jumpPlanes = new List<Collider2D>(GameObject.FindGameObjectsWithTag("jumpPlane").Select(go => go.GetComponent<Collider2D>()));

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
    
    //暂时先放在这
    public void PlayerRespawn()
    {
        player.transform.position = spawnPoint.position;
        player.GetComponent<PlayerProperty>().relife();
        player.stateMachine.ChangeState(player.idleState);
    }




    #region KeyChange
    //-----------------按键更改接口-------------------------------------
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
    #endregion
}
