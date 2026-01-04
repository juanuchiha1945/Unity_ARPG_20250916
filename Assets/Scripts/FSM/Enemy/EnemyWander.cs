using UnityEngine;

/// <summary>
/// 敵人遊走
/// </summary>
public class EnemyWander : EnemyState
{
    private float wanderTime;

    public EnemyWander(Enemy enemy, StateMachine stateMachine, string name) : base(enemy, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 設定遊走目標點
        enemy.SetWanderTarget();

        // 在遊走時間範圍內隨機一個時間
        wanderTime = Random.Range(enemy.wanderTimeRange.x, enemy.wanderTimeRange.y);

        // Debug.Log($"<color=#f7f>遊走時間：{wanderTime}</color>");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        #region 條件區域
        // 如果計時器大於遊走時間就切換到待機狀態
        if (timer >= wanderTime) stateMachine.SwitchState(enemy.idle);
        
        // 如果玩家進入追蹤範圍就切換到追蹤模式
        if (enemy.CheckPlayerInTrackRange()) stateMachine.SwitchState(enemy.track);
        else enemy.StartCoroutine(FadeSystem.Fade(enemy.groupHp, false));
        #endregion
    }
}