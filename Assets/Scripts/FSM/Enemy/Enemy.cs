using UnityEngine;

/// <summary>
/// 敵人
/// </summary>
public class Enemy : Character
{
    #region 資料
    [field: Header("敵人資料")]
    [field: SerializeField]
    public Vector2 idleTimeRange { get; private set; } = new Vector2(1f, 3f);
    [field: SerializeField]
    public Vector2 wanderTimeRange { get; private set; } = new Vector2(3f, 5f);
    [SerializeField, Tooltip("遊走的中心點")]
    private Vector3 wanderCenter;
    [SerializeField, Tooltip("遊走的半徑"), Range(0, 7)]
    private float wanderRadius = 5;

    /// <summary>
    /// 遊走的目標點
    /// </summary>
    private Vector3 wanderTarget;
    #endregion

    #region 狀態機
    public EnemyIdle idle { get; private set; }
    public EnemyWander wander { get; private set; }
    public EnemyTrack track { get; private set; }
    public EnemyAttack attack { get; private set; }
    public EnemyDead dead { get; private set; }
    #endregion

    /// <summary>
    /// 選取物件時繪製圖示
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0.5f, 0.3f);
        Gizmos.DrawSphere(wanderCenter, wanderRadius);

        Gizmos.color = new Color(0.5f, 0.3f, 1, 0.8f);
        Gizmos.DrawSphere(wanderTarget, 0.7f);
    }

    protected override void Awake()
    {
        base.Awake();

        // 狀態機初始化
        stateMachine = new StateMachine();
        // 實例化狀態
        idle = new EnemyIdle(this, stateMachine, $"{name} 待機");
        wander = new EnemyWander(this, stateMachine, $"{name} 巡邏");
        track = new EnemyTrack(this, stateMachine, $"{name} 追蹤");
        attack = new EnemyAttack(this, stateMachine, $"{name} 攻擊");
        dead = new EnemyDead(this, stateMachine, $"{name} 死亡");
        // 狀態機啟動
        stateMachine.Initialize(idle);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    /// <summary>
    /// 獲得遊走目標點
    /// </summary>
    public void SetWanderTarget()
    {
        // 在圓形範圍內隨機選取一個點作為遊走目標點
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        wanderTarget = wanderCenter + new Vector3(randomPoint.x, 0, randomPoint.y);
    }
}