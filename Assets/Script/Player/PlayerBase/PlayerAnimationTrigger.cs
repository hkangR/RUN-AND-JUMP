using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//在动画序列帧中触发
public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
        //Debug.Log("Attack Finish");
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        if(player.canMakeMask)
            Instantiate(player.mask,player.attackCheck.position,Quaternion.identity);
        
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackCheckRadius);

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                enemy.StartCoroutine("FlashFX");
                player.CauseDamage(enemy);
            }
            
        }
    }
}
