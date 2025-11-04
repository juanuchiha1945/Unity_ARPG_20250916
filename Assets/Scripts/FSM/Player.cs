using UnityEngine;

/// <summary>
/// 玩家類別 : 紀錄玩家資料與相關功能
/// </summary>
public class Player : MonoBehaviour
{
    #region 玩家屬性
    // 唯讀屬性 : 讓外部取得此資料窗口但不能修改
    // 序列化 : 讓私有欄位可以在編輯器中顯示與修改
    [field: Header("玩家資料")]
    [field: SerializeField, Range(0, 10)]
    public float walkSoeed { get; private set; } = 2.5f;
    [field: SerializeField, Range(3, 15)]
    public float runSoeed { get; private set; } = 5;
    [field: SerializeField, Range(0, 20)]
    public float jumpHeight { get; private set; } = 7.5f;

    public Animator ani { get; private set; }
    public Rigidbody rig { get; private set; }
    public string parHorizontal { get; private set; } = "水平";
    public string parVertical { get; private set; } = "垂直";
    public string parGravity { get; private set; } = "重力";
    public string parJump { get; private set; } = "跳躍開關";
    public string parAttackCombo { get; private set; } = "攻擊段數";
    public string parTriggerAttack { get; private set; } = "觸發攻擊";
    public string parTriggerDead { get; private set; } = "觸發死亡";
    #endregion

    private void Awake()
    {
        ani = GetComponent<Animator>();     // 取得動畫元件
        rig = GetComponent<Rigidbody>();    // 取得剛體元件
    }
}
