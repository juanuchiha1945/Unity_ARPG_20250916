using UnityEngine;

/// <summary>
///  玩家跑步
/// </summary>
public class PlayerRun : PlayerGround
{
    public PlayerRun(StateMachine stateMachine, Player player, string name) : base(stateMachine, player, name)
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

        // 設定動畫參數 水平、垂直 為 玩家輸入值 
        player.ani.SetFloat(player.parHorizontal, inputH * 2);    
        player.ani.SetFloat(player.parVertical, inputV * 2);

        // 設定剛體速度 為 右向 * 水平輸入 * 走路速度 + 前向 * 垂直輸入 * 走路速度
        player.SetVelocity(
            player.transform.right * inputH * player.runSpeed +
            player.transform.forward * inputV * player.runSpeed +
            player.transform.up * player.rig.linearVelocity.y);

        // 面相攝影機
        player.LookAtCamera();

        #region 條件區域
        // 如果玩家的水平輸入 等於 零 並且 垂直輸入 等於 零 就切換到 待機狀態
        if (inputH == 0 && inputV == 0) stateMachine.SwitchState(player.idle);

        // 如果玩家沒有按左邊 Shift 鍵 就切換到 走路狀態
        if (Input.GetKeyUp(KeyCode.LeftShift)) stateMachine.SwitchState(player.walk);
        #endregion
    }
}
