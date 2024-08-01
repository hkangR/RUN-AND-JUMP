using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Player player;
    public Transform spawnPoint;

    public void Start() {
        player = PlayerManager.instance.player;
    }

    public void Respawn() {
        player.transform.position = spawnPoint.position;
        player.GetComponent<PlayerProperty>().relife();
        player.stateMachine.ChangeState(player.idleState);
    }
}
