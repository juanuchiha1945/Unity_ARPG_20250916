using UnityEngine;

/// <summary>
///  玩家走路
/// </summary>
public class PlayerWalk : PlayerGround
{
    public PlayerWalk(StateMachine stateMachine, Player player, string name) : base(stateMachine, player, name)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.ani.SetFloat(player.parHorizontal, inputH);   // 設定動畫參數 水平 為 水平輸入
        player.ani.SetFloat(player.parVertical, inputV);     // 設定動畫參數 垂直 為 垂直輸入

        // 設定剛體速度 為 右向 * 水平輸入 * 走路速度 + 前向 * 垂直輸入 * 走路速度
        player.SetVelocity(
            player.transform.right * inputH * player.walkSpeed +
            player.transform.forward * inputV * player.walkSpeed +
            player.transform.up * player.rig.linearVelocity.y);

        // 面相攝影機
        player.LookAtCamera();

        #region 條件區域
        // 如果玩家的水平輸入 等於 零 並且 垂直輸入 等於 零 就切換到 待機狀態
        if (inputH == 0 && inputV == 0) stateMachine.SwitchState(player.idle);

        // 如果玩家按左邊 Shift 鍵 就切換到 跑步狀態
        if (Input.GetKey(KeyCode.LeftShift)) stateMachine.SwitchState(player.run);
        #endregion
    }
}
