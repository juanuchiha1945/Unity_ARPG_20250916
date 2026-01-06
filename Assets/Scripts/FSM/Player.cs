using System;
using UnityEngine;

/// <summary>
/// 玩家類別 : 紀錄玩家資料與相關功能
/// </summary>
public class Player : Character
{
    private static Player _instance;

    public static Player instance
    {
        get
        {
            if(_instance == null) _instance = FindAnyObjectByType<Player>();
            return _instance;
        }
    }

    public event Action onDead;

    #region 玩家屬性
    // 唯讀屬性 : 讓外部取得此資料窗口但不能修改
    // 序列化 : 讓私有欄位可以在編輯器中顯示與修改
    [field: Header("玩家資料")]
    //[field: SerializeField, Range(0, 10)]
    //public float walkSpeed { get; private set; } = 2.5f;
    [field: SerializeField, Range(3, 15)]
    public float runSpeed { get; private set; } = 5;
    [field: SerializeField, Range(0, 20)]
    public float jumpHeight { get; private set; } = 7.5f;
    [field: SerializeField, Range(0, 30)]
    public float turnSpeed { get; private set; } = 15f;
    [field: SerializeField, Range(0, 3), Tooltip("中斷攻擊連段的時間")]
    public float breakComboTime { get; private set; } = 1f;

    //  public Animator ani { get; private set; }
    //  public Rigidbody rig { get; private set; }

    //  public string parHorizontal { get; private set; } = "水平";
    //  public string parVertical { get; private set; } = "垂直";
    public string parGravity { get; private set; } = "重力";
    public string parJump { get; private set; } = "跳躍開關";
    public string parAttackCombo { get; private set; } = "攻擊段數";
    //public string parTriggerAttack { get; private set; } = "觸發攻擊";
    //public string parTriggerDead { get; private set; } = "觸發死亡";

    private Transform mainCam;
    #endregion

    #region 狀態資料
    
    public PlayerIdle idle { get; private set; }
    public PlayerWalk walk { get; private set; }
    public PlayerRun run { get; private set; }
    public PlayerJump jump { get; private set; }
    public PlayerFall fall { get; private set; }
    public PlayerAttack attack { get; private set; }
    public PlayerDead dead { get; private set; }
    #endregion

    #region 檢查資料
    [Header("檢查資料")]
    [SerializeField, Range(0, 1)]
    private float checkGroundRadius = 0.2f;             // 檢查地面半徑
    [SerializeField, Range(-2, 2)]
    private float checkGroundOffsetY;                   // 檢查地面 Y 軸位移
    [SerializeField]
    private LayerMask layerCanJump;                     // 可跳躍的圖層
    #endregion

    // ODGS 選取後繪製圖示
    private void OnDrawGizmosSelected()
    {
        // 決定顏色
        Gizmos.color = new Color(0.5f, 1, 0.5f, 0.5f);
        // 繪製球體
        Gizmos.DrawSphere(
            transform.position + new Vector3(0, checkGroundOffsetY, 0),
            checkGroundRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果碰到物件 嘗試取得敵人攻擊物件 有資料 就造成傷害
        if (other.TryGetComponent(out AttackObjectEnemy attackObject))
        {
            Damage(attackObject.attackPower);
        }
    }

    protected override void Awake()
    {
        base.Awake();                       // 繼承父類別的 Awake 方法
        HideMouse();                        // 隱藏滑鼠    

        //  ani = GetComponent<Animator>();     // 取得動畫元件
        //  rig = GetComponent<Rigidbody>();    // 取得剛體元件
        mainCam = Camera.main.transform;    // 取得主攝影機的變形元件 (貼 MainCamera 標籤)

        #region 狀態實例化
        // 實例化 new 該類別 : 讓此類別不用掛在物件上也可以在場景內執行
        stateMachine = new StateMachine();
        idle = new PlayerIdle(stateMachine, this, $"{name} 待機");
        walk = new PlayerWalk(stateMachine, this, $"{name} 走路");
        run = new PlayerRun(stateMachine, this, $"{name} 跑步");
        jump = new PlayerJump(stateMachine, this, $"{name} 跳躍");
        fall = new PlayerFall(stateMachine, this, $"{name} 落下");
        attack = new PlayerAttack(stateMachine, this, $"{name} 攻擊");
        dead = new PlayerDead(stateMachine, this, $"{name} 死亡");
        #endregion

        // 初始化狀態機 為 待機狀態
        stateMachine.Initialize(idle);
    }

    private void Update()
    {
        // 狀態機更新
        stateMachine.Update();
        // 呼叫輸入攻擊方式
        InputAttack();
    }

    /// <summary>
    /// 設定加速度
    /// </summary>
    /// <param name="direction">加速度方向</param>
    public void SetVelocity(Vector3 direction)
    {
        //剛體 的 線性加速度 = 方向
        rig.linearVelocity = direction;
    }

    /// <summary>
    /// 面相攝影機
    /// </summary>
    public void LookAtCamera()
    {
        // 建立一個新的四元數 只使用攝影機的 Y 軸角度
        Quaternion camAngle = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
        // 使用插值方式 讓玩家角度慢慢轉向攝影機角度
        transform.rotation = Quaternion.Lerp(transform.rotation, camAngle, turnSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 隱藏滑鼠
    /// </summary>
    public void HideMouse()
    {
        // 隱藏滑鼠並鎖定在遊戲視窗中心
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 能否跳躍 : 檢查是否碰到可跳躍圖層物件
    /// </summary>
    public bool CanJump()
    {
        // 檢查是否在地面上
        return Physics.CheckSphere(
            transform.position + new Vector3(0, checkGroundOffsetY, 0),
            checkGroundRadius, layerCanJump);
            
    }

    /// <summary>
    /// 輸入攻擊按鍵並進入攻擊狀態
    /// </summary>
    private void InputAttack()
    {
        // 如果在攻擊中就跳出
        if (attack.isAttacking) return;

        // 如果 按下滑鼠左鍵 並且 可以跳躍 (在地面上)
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanJump())
        {
            // 狀態機 切換到 攻擊狀態
            stateMachine.SwitchState(attack);
        }
    }

    protected override void Damage(float damage)
    {
        base.Damage(damage);
        CameraShake.instance.ShakeCamera(0.2f, 5, 10f);
        StartCoroutine(DamageEffect(0.5f, 0.2f));
    }

    protected override void Dead()
    {
        base.Dead();
        gameObject.layer = 0;
        onDead?.Invoke();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameFlowManager.instance.ShowFinish("你死了！");
    }

    

}
