using System.Collections;
using UnityEngine;

public class BossHandHitState : EnemyState
{
    private BossHand enemy;
    private Player player;
    private int shakeDir = 1; // 向右震动
    private Coroutine moveCoroutine; // 存储当前正在运行的协程

    public BossHandHitState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, BossHand enemy) 
        : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        // 播放动画
        base.Enter();
        stateTimer = enemy.floatTime;
        player = PlayerManager.instance.player;
        enemy.transform.position += enemy.hitOffset; // 手掌悬浮位置

        if (moveCoroutine == null) // 确保每次只启动一个协程
        {
            moveCoroutine = enemy.StartCoroutine(MoveAbovePlayerAndHit(enemy.transform, player.transform, enemy.floatSpeed));
        }
    }

    public override void Exit()
    {
        base.Exit();

        // 如果有正在运行的协程，退出时停止它
        if (moveCoroutine != null)
        {
            enemy.StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
    }

    public override void Update()
    {
        base.Update();
        // Debug.Log("Hit State");

        if (!enemy.isHitting && moveCoroutine == null)
        {
            moveCoroutine = enemy.StartCoroutine(MoveAbovePlayerAndHit(enemy.transform, player.transform, enemy.floatSpeed));
        }

        // Debug.Log("idle");
        /*if (stateTimer < 0)
        { 
            enemy.StartCoroutine(enemy.Hit());
        }*/
    }

    private IEnumerator MoveAbovePlayerAndHit(Transform enemyTransform, Transform playerTransform, float floatSpeed)
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x, enemyTransform.position.y, enemyTransform.position.z);
        float elapsedTime = 0f;
        float journeyLength = Vector3.Distance(enemyTransform.position, targetPosition);

        while (Vector3.Distance(enemyTransform.position, targetPosition) > 0.01f)
        {
            // 计算路程完成的比例
            elapsedTime += Time.deltaTime;
            float fractionOfJourney = elapsedTime * floatSpeed / journeyLength;

            // 插值到目标位置
            enemyTransform.position = Vector3.Lerp(enemyTransform.position, targetPosition, fractionOfJourney);

            yield return null; // 等待下一帧
        }

        // 确保敌人在循环结束后精确地位于目标位置
        enemyTransform.position = targetPosition;

        // 在到达目标位置后启动 Hit() 协程
        yield return enemy.StartCoroutine(enemy.Hit());

        // 协程结束后将 moveCoroutine 置为空
        moveCoroutine = null;
    }
}