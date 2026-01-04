using UnityEngine;

/// <summary>
/// 敵人待機
/// </summary>
public class EnemyIdle : EnemyState
{
    private float idleTime;

    public EnemyIdle(Enemy enemy, StateMachine stateMachine, string name) : base(enemy, stateMachine, name)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 在待機時間範圍內隨機一個時間
        // Random.Range(最小值, 最大值)
        // Random.Range(1f, 3f) 會回傳 1 ~ 3 之間的隨機浮點數
        idleTime = Random.Range(enemy.idleTimeRange.x, enemy.idleTimeRange.y);

        // Debug.Log($"<color=#f7f>待機時間：{idleTime}</color>");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        #region 條件區域
        // 如果計時器大於待機時間就切換到遊走狀態
        if (timer >= idleTime) stateMachine.SwitchState(enemy.wander);
        #endregion

        // 如果玩家進入追蹤範圍就切換到追蹤狀態
        if (enemy.CheckPlayerInTrackRange()) stateMachine.SwitchState(enemy.track);
        else enemy.StartCoroutine(FadeSystem.Fade(enemy.groupHp, false));
    }
}