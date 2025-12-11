using UnityEngine;

/// <summary>
/// 玩家地面狀態 : 在地面上可以執行的行為，待機、走路與跑步都屬於此狀態
/// </summary>
public class PlayerGround : PlayerState
{
    public PlayerGround(StateMachine stateMachine, Player player, string name) : base(stateMachine, player, name)
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

        #region 條件區域
        // 如果 可以跳躍 並且 按下空白鍵 就產生向上的加速度
        if (player.CanJump() && Input.GetKeyDown(KeyCode.Space))
            stateMachine.SwitchState(player.jump);
        #endregion
    }
}