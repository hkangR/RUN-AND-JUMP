using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            
            //Debug.Log("Attacking Something");
        }
    }
}
