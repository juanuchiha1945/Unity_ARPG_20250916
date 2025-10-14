using UnityEngine;
using UnityEngine.UI;

// 繼承 MonoBehaviour 允許此類別掛在遊戲物件上
/// <summary>
/// 主選單管理器
/// 繼續遊戲、開始遊戲、選項、製作團隊與退出按鈕
/// <summary>
public class MainMenuManager : MonoBehaviour 
{
    //private 代表只能在此類別內存取並隱藏
    // 允許在 Unity 編輯器中設置私有變數
    [SerializeField] private Button btnLord;     //繼續遊戲按鈕 
    [SerializeField] private Button btnNew;      //開始遊戲按鈕 
    [SerializeField] private Button btnOption;   //選項按鈕 
    [SerializeField] private Button btnCredit;   //製作團隊按鈕
    [SerializeField] private Button btnQuit;     //退出按鈕
    [SerializeField] private Button btnBackOption;
    [SerializeField] private Button btnBackCredit;
    [SerializeField] private CanvasGroup groupOption;
    [SerializeField] private CanvasGroup groupCredit;

    private void Awake()
    {
        //Debug.Log("Hello world");
        //為按鈕添加點擊事件監聽器
        //使用 Lambda 表達式來調用方法
        //StartCoroutine 用於啟動協程
        //控制介面的淡入與淡出
        btnOption.onClick.AddListener(() => StartCoroutine(FadeSystem.Fade(groupOption, interval: 0.05f)));
        btnBackOption.onClick.AddListener(() => StartCoroutine(FadeSystem.Fade(groupOption, false)));
        btnCredit.onClick.AddListener(() => StartCoroutine(FadeSystem.Fade(groupCredit, interval: 0.05f)));
        btnBackCredit.onClick.AddListener(() => StartCoroutine(FadeSystem.Fade(groupCredit, false)));
        // 點擊退出按鈕時，退出應用程式
        // Application.Quit() 會在編輯器中停止播放模式，在打包後的應用程式中退出應用程式
        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
            Debug.Log("<color=#ff3>退出遊戲</color>");
        });
        // 點即開始遊戲按鈕時，載入遊戲場景
        btnNew.onClick.AddListener(() => SceneLoader.instacne.LoadSceneAsync("遊戲場景"));
    }
}
