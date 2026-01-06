using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 角色：角色基本資料與功能
/// abstract 抽象類別：不會有實體
/// </summary>
public abstract class Character : MonoBehaviour
{
    [field: Header("角色資料")]
    [field: SerializeField, Range(0, 10)]
    public float walkSpeed { get; private set; } = 2.5f;
    [SerializeField, Range(0, 500)]
    protected float maxHp = 100;

    protected float hp;

    [SerializeField]
    protected Image imgHp;
    [SerializeField]
    protected TMP_Text texHp;

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
        hp = maxHp;                         // 血量等於最大血量
        if (texHp != null)
        {
            texHp.text = hp.ToString();
        }
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收到的傷害值</param>
    protected virtual void Damage(float damage)
    {
        hp -= damage;

        // 限制血量不會低於 0 (為了美觀)
        if (hp < 0) hp = 0;

        if (hp <= 0) Dead();

        // --- 更新 UI 的部分 ---

        // 1. 更新文字
        if (texHp != null)
        {
            texHp.text = hp.ToString();
        }

        // 2. 更新血條圖片 (這是關鍵！)
        if (imgHp != null && maxHp > 0)
        {
            // 目前血量 / 最大血量 = 剩餘百分比 (0 ~ 1)
            imgHp.fillAmount = hp / maxHp;
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    protected virtual void Dead()
    {
        ani.SetTrigger(parTriggerDead);
        rig.isKinematic = true;             // 死亡後剛體改為運動學 (不受物理影響)
        enabled = false;                    // 停用此腳本
    }

    /// <summary>
    /// 受傷效果：卡肉感
    /// </summary>
    protected IEnumerator DamageEffect(float timeScale, float duration)
    {
        Time.timeScale = timeScale;                 // 時間變慢
        yield return new WaitForSeconds(duration);  // 等待指定時間
        Time.timeScale = 1f;                        // 恢復正常速度
    }

}