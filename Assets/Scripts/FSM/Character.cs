using UnityEngine;

/// <summary>
/// 角色：角色基本資料與功能
/// abstract 抽象類別：不會有實體
/// </summary>
public abstract class Character : MonoBehaviour
{
    [field: Header("角色資料")]
    [field: SerializeField, Range(0, 10)]
    public float walkSpeed { get; private set; } = 2.5f;

    public Animator ani { get; private set; }
    public Rigidbody rig { get; private set; }
    public string parHorizontal { get; private set; } = "水平";
    public string parVertical { get; private set; } = "垂直";
    public string parTriggerAttack { get; private set; } = "觸發攻擊";
    public string parTriggerDead { get; private set; } = "觸發死亡";

    public StateMachine stateMachine { get; protected set; }

    protected virtual void Awake()
    {
        ani = GetComponent<Animator>();     // 取得動畫元件
        rig = GetComponent<Rigidbody>();    // 取得剛體元件
    }
}