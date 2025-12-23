/// <summary>
/// 敵人追蹤
/// </summary>
public class EnemyTrack : EnemyState
{
    public EnemyTrack(Enemy enemy, StateMachine stateMachine, string name) : base(enemy, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 更新代理器速度 = 追蹤速度
        enemy.agent.speed = enemy.trackSpeed;
        // 更新代理器停止距離 = 攻擊範圍
        enemy.agent.stoppingDistance = enemy.attackRadius;
        // 讓代理器前往玩家位置
        enemy.agent.SetDestination(enemy.traPlayer.position);
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

        // 讓代理器前往玩家位置
        enemy.agent.SetDestination(enemy.traPlayer.position);
        // 更新移動動畫
        enemy.ani.SetFloat(enemy.parVertical,
            enemy.agent.velocity.magnitude / enemy.trackSpeed * 2);

        #region 條件區域
        // 如果玩家超出追蹤範圍就切換到待機狀態
        if (!enemy.CheckPlayerInTrackRange()) stateMachine.SwitchState(enemy.wander);
        
        // 如果剩餘距離 <= 攻擊範圍就切換到攻擊狀態
        if (enemy.agent.remainingDistance <= enemy.attackRadius)
            stateMachine.SwitchState(enemy.attack);
        #endregion
    }
}