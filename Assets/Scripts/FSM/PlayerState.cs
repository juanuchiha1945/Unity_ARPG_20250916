using UnityEngine;

/// <summary>
/// 玩家狀態
/// </summary>
// PlayerState 繼承 State > PlayerState 是 State 的子類別
public class PlayerState : State
{
    protected Player player;    // 玩家
    protected float inputH;
    protected float inputV;
    // protected float timer;      // 計時器

    // 建構函式 : 類別被實例化 new 時會執行，資料初始化處理
    public PlayerState(StateMachine stateMachine, Player player, string name)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.name = name;
    }

    public override void Enter()
    {
        base.Enter();
        // 這裡使用了 Rich Text 語法改變顏色
        // Debug.Log($"<color=#ff3>進入狀態 : {name}</color>");
        // timer = 0f; // 進入狀態時重置計時器
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        inputH = Input.GetAxis("Horizontal");   // 獲得玩家水平輸入 A -1，D +1 沒按 0
        inputV = Input.GetAxis("Vertical");     // 獲得玩家垂直輸入 W +1，S -1 沒按 0
        // 累加時間到計時器內
        // Time.deltaTime 每個影格的時間，大約是0.02 秒 (1/60)
        // timer += Time.deltaTime;
    }
}