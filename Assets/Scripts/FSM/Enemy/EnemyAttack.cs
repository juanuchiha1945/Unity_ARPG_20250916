using UnityEngine;

/// <summary>
/// 敵人攻擊
/// </summary>
public class EnemyAttack : EnemyState
{
    public EnemyAttack(Enemy enemy, StateMachine stateMachine, string name) : base(enemy, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 代理器加速度歸零
        enemy.agent.velocity = Vector3.zero;
        // 更新代理器停止距離 = 攻擊範圍
        enemy.agent.stoppingDistance = enemy.attackRadius;
        // 更新攻擊動畫
        enemy.ani.SetTrigger(enemy.parTriggerAttack);
        // 更新走路動畫為零
        enemy.ani.SetFloat(enemy.parVertical, 0);
    }

    public override void Exit()
    {
        base.Exit();

        // 還原停止距離
        enemy.agent.stoppingDistance = 0;
    }

    public override void Update()
    {
        base.Update();

        // 面向玩家
        enemy.LookAtPlayer();

        #region 條件區域
        // 如果計時器大於攻擊動畫播放時間 就回到追蹤狀態
        if (timer >= enemy.ani.GetCurrentAnimatorStateInfo(0).length + enemy.attackCD)
            stateMachine.SwitchState(enemy.track);
        #endregion
    }
}