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
                //hit.GetComponent<Enemy>().Damage();
                //hit.GetComponent<CharacterStats>().TakeDamage(player.stats.damage.GetValue());
                //EnemyStats target = hit.GetComponent<EnemyStats>();
                //player.stats.DoDamage(target);
                //player.stats.DoDamage(target);
                //Instantiate(hit.GetComponent<Temp>().mask, hit.transform.transform.position, Quaternion.identity);
                if(enemy.beAttackNum >= 3) enemy.Die();
                else
                {
                    enemy.beAttackNum++;
                    enemy.StartCoroutine("FlashFX");
                    player.CauseDamage(enemy);
                }
            }
            
            //Debug.Log("Attacking Something");
        }
    }
}
