using UnityEngine;
using System.Collections;

public class FadeSystem : MonoBehaviour
{
    /// <summary>
    /// 控制 Canvas Group 淡入或淡出
    /// </summary>
    public static IEnumerator Fade(CanvasGroup group, bool fadeIn = true, float interval = 0.03f)
    {
        // 如果 fadeIn 為 true，就讓透明度每次加 0.1f；否則每次減 0.1f
        var increase = fadeIn ? +0.1f : -0.1f;

        // 進行 10 次的淡入或淡出
        for (int i = 0; i < 10; i++)
        {
            group.alpha += increase;                     // 調整透明度
            yield return new WaitForSeconds(interval);   // 等待指定的時間間隔
        }

        // 最後設定互動與遮擋射線
        group.interactable = fadeIn;
        group.blocksRaycasts = fadeIn;
    }
}
