using UnityEngine;

/// <summary>
/// 攻擊物件：掛載在武器、子彈或傷害判定框上
/// </summary>
public class AttackObject : MonoBehaviour
{
    [field: Header("攻擊數值設定")]

    // 1. 使用 Range 限制攻擊力在 10 ~ 100 之間
    // 2. 使用 { get; private set; } 確保外部腳本只能讀取，不能修改
    [field: SerializeField, Range(10, 100)]
    public float attackPower { get; private set; } = 10f;

    /// <summary>
    /// 當此物件碰觸到其他 Collider 時觸發
    /// </summary>
    /// <param name="other">碰到的物件</param>
    private void OnTriggerEnter(Collider other)
    {
        // 範例：如果碰到的是玩家 (假設玩家物件名稱叫 "獵人" 或有 Tag)
        if (other.name == "獵人" || other.CompareTag("Player"))
        {
            Debug.Log($"<color=red>擊中 {other.name}！造成 {attackPower} 點傷害。</color>");

            // 在這裡可以呼叫玩家的受傷方法，例如：
            // other.GetComponent<Player>().TakeDamage(attackPower);
        }
    }
}