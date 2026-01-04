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
    protected float hpMax = 100;

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
        hp = hpMax;                         // 血量等於最大血量
        texHp.text = $"{hp} / {hpMax}";     // 更新血量文字
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">傷害值</param>
    protected virtual void Damage(float damage)
    {
        hp -= damage;                       // 扣血
        hp = Mathf.Clamp(hp, 0, hpMax);     // 將血量夾在 0 ~ 最大值 之間
        imgHp.fillAmount = hp / hpMax;      // 更新血條
        texHp.text = $"{hp} / {hpMax}";     // 更新血量文字  
        if (hp <= 0) Dead();                // 死亡

        Debug.Log($"<color=#66f>{gameObject.name} 剩餘血量：{hp}</color>");

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