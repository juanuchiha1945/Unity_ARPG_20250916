using UnityEngine;

/// <summary>
///  玩家落下
/// </summary>
public class PlayerFall : PlayerState
{
    public PlayerFall(StateMachine stateMachine, Player player, string name) : base(stateMachine, player, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // 動畫參數 重力 浮點數 -1
        player.ani.SetFloat(player.parGravity, -1);
    }

    public override void Exit()
    {
        base.Exit();
        // 離開跳躍狀態時將跳躍開關 關閉
        player.ani.SetBool(player.parJump, false);
    }

    public override void Update()
    {
        base.Update();

        // 設定剛體速度 為 右向 * 水平輸入 * 走路速度 + 前向 * 垂直輸入 * 走路速度
        player.SetVelocity(
            player.transform.right * inputH * player.walkSpeed +
            player.transform.forward * inputV * player.walkSpeed +
            player.transform.up * player.rig.linearVelocity.y);

        // 面相攝影機
        player.LookAtCamera();

        #region 條件區域
        // 如果 玩家可以跳躍 (回到地面上) 就切換到 待機狀態
        if (player.CanJump())
            stateMachine.SwitchState(player.idle);
        #endregion
    }
}
