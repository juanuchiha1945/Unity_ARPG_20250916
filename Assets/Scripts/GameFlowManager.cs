using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 如果你要直接用 SceneManager 需引用此行

/// <summary>
/// 遊戲流程管理器：管理遊戲的主要流程與狀態
/// 遊戲勝利與失敗，重新與退出處理
/// </summary>
public class GameFlowManager : MonoBehaviour
{
    private static GameFlowManager _instance;
    public static GameFlowManager instance
    {
        get
        {
            if (_instance == null) _instance = FindAnyObjectByType<GameFlowManager>();
            return _instance;
        }
    }

    private CanvasGroup groupFinish;
    private TMP_Text textFinish;
    private Button btnReplay, btnQuit;
    [Header("敵人擊殺設定")]
    public TMP_Text textEnemyCount;    // 顯示數量的文字 UI (記得拖曳進去)
    public int enemyCountMax;      // 最大擊殺目標 (可在 Inspector 修改)
    private int enemyCountKill;    // 目前擊殺數

    private void Awake()
    {
        groupFinish = GameObject.Find("群組_結束畫面").GetComponent<CanvasGroup>();
        textFinish = GameObject.Find("文字_結束標題").GetComponentInChildren<TMP_Text>();
        btnReplay = GameObject.Find("按鈕_重新挑戰").GetComponent<Button>();
        btnQuit = GameObject.Find("按鈕_退出").GetComponent<Button>();
        btnReplay.onClick.AddListener(() => SceneManager.LoadSceneAsync("遊戲場景"));
        btnQuit.onClick.AddListener(() => Application.Quit());
        textEnemyCount = GameObject.Find("文字_清除所有敵人").GetComponentInChildren<TMP_Text>();
        enemyCountMax = GameObject.FindGameObjectsWithTag("敵人").Length;
        textEnemyCount.text = $"清除所有敵人：0 / {enemyCountMax}";

        // --- 以下是第二張圖片的內容 ---

        btnReplay.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("遊戲場景");
        });

        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    /// <summary>
    /// 擊殺敵人
    /// </summary>
    public void KillEnemy()
    {
        enemyCountKill++;

        // $ 符號代表字串插值，可以直接把變數放進 {} 裡面
        textEnemyCount.text = $"清除所有敵人：{enemyCountKill} / {enemyCountMax}";

        if (enemyCountKill >= enemyCountMax)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ShowFinish("任務完成！");
        }
    }

    /// <summary>
    /// 顯示結束畫面
    /// </summary>
    /// <param name="title">結束標題</param>
    public void ShowFinish(string title)
    {
        textFinish.text = title;
        // 呼叫淡入效果 (需確認有 FadeSystem 腳本)
        StartCoroutine(FadeSystem.Fade(groupFinish));
    }
}