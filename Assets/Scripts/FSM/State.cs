using UnityEngine;

/// <summary>
/// 狀態
/// </summary>
public class State
{
    // protected 受保護的 允許子類別存取
    protected string name;                  // 狀態名稱
    protected StateMachine stateMachine;    // 狀態機器

    // virtual 虛擬 : 允許子類別覆寫此方法
    /// <summary>
    /// 進入狀態
    /// </summary>
    public virtual void Enter()
    {

    }

    /// <summary>
    /// 更新狀態
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// 離開狀態
    /// </summary>
    public virtual void Exit()
    {

    }
}
