using Unity.Cinemachine.Editor;
using UnityEngine;

/// <summary>
///  玩家攻擊
/// </summary>
public class PlayerAttack : PlayerState
{
    private int comboMax = 4;       // 最大攻擊段數
    private int comboIndex;         // 目前攻擊段數
    private float attackFinishTime; // 攻擊結束時間

    public bool isAttacking { get; private set; }   // 是否正在攻擊

    public PlayerAttack(StateMachine stateMachine, Player player, string name) : base(stateMachine, player, name)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 如果 距離上次攻擊結束時間 超過 連擊中段時間 就歸零攻擊段數
        if (Time.time - attackFinishTime > player.breakComboTime)
            comboIndex = 0;

        // 設定正在攻擊
        isAttacking = true;
        // 套用動畫位移
        player.ani.applyRootMotion = true;
        // 觸發攻擊動畫
        player.ani.SetTrigger(player.parTriggerAttack);
        // 更新攻擊段數
        player.ani.SetFloat(player.parAttackCombo, comboIndex);

        // 段數遞增 (加一)
        comboIndex++;
        // 如果段數大於等於最大段數 就重置為0
        if (comboIndex >= comboMax) comboIndex = 0;
    }

    public override void Exit()
    {
        base.Exit();
        // 設定不在攻擊
        isAttacking = false;
        // 關閉動畫的位移
        player.ani.applyRootMotion = false;
        // 紀錄攻擊結束時間
        attackFinishTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        //Debug.Log($"<color=#6f6>計時器:{timer}</color>");

        // 如果攻擊動畫播完就切回待機
        // if (playter.ani.GetCurrenTanimatorStateInfo(0).normalizedTime >= 1f)
        // 如果 計時器 >= 當前動畫時間 就 切回待機
        // player.aniGetCurrentAnimatorStateInfo(0).length 取得當前動畫的長度
        if (player.ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)        
            stateMachine.SwitchState(player.idle);    
    }
}
