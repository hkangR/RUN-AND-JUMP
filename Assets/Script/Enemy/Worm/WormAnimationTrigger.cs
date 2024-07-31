using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//虫子的动画触发事件
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
                Player player = hit.GetComponent<Player>();
                enemy.CauseDamage(player);//敌人对玩家造成伤害
            }
        }
    }

    //private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
    //private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();
}
