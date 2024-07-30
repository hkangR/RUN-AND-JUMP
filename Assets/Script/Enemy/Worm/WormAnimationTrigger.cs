using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormAnimationTrigger : MonoBehaviour
{
    private Enemy_Worm enemy => GetComponentInParent<Enemy_Worm>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        
        foreach(var hit in collider)
        {
            if(hit.GetComponent<Player>() != null)
            {
                /*PlayerStats target = hit.GetComponent<PlayerStats>();
                //enemy.stats.DoDamage(target);
                enemy.stats.DoDamage(target);*/
                Debug.Log("Attacking Player");
            }
        }
    }

    //private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
    //private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();
}
