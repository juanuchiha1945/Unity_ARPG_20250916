using UnityEngine;

/// <summary>
/// 狀態機
/// </summary>
public class StateMachine
{
    // 紀錄當前狀態
    private State currentstate;

    // 功能 = 方法 = 函式 = 函數 | method, function
    /// <summary>
    /// 初始化狀態機
    /// </summary>
    /// <param name="firstState">第一個狀態</param>
    public void Initialize(State firstState)
    {
        // 指定當前狀態
        currentstate = firstState;
        // 進入當前狀態
        currentstate.Enter();
    }

    /// <summary>
    /// 切換狀態 : 先退出原本狀態進入新的狀態
    /// </summary>
    /// <param name="newState">新的狀態</param>
    public void SwitchState(State newState)
    {
        // 離開當前狀態
        currentstate.Exit();
        // 指定新狀態為當前狀態
        currentstate = newState;
        // 進入當前狀態
        currentstate.Enter();
    }

    /// <summary>
    /// 更新狀態 : 持續執行當前的狀態
    /// </summary>
    public void Update()
    {
        // 更新當前狀態
        currentstate.Update();
    }
}
