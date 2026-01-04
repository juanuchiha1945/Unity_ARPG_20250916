using UnityEngine;
using Unity.Cinemachine; // ⚠️ 注意：這是新版的命名空間
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private static CameraShake _instance;

    public static CameraShake instance
    {
        get
        {
            if (_instance == null) _instance = FindAnyObjectByType<CameraShake>();
            return _instance;
        }
    }

    // 定義 Cinemachine 的虛擬攝影機柏林函數
    private CinemachineBasicMultiChannelPerlin perlin;

    private float amplitudeDefault, frequencyDefault;

    private void Awake()
    {
        // 【新增這行】確保腳本醒來時，直接告訴大家 "我就是 instance"
        if (_instance == null) _instance = this;

        // 獲取 Cinemachine 虛擬攝影機組件
        perlin = GetComponent<CinemachineBasicMultiChannelPerlin>();

        // 防呆檢查：如果找不到 perlin 組件，報錯提醒你
        if (perlin == null)
        {
            Debug.LogError($"❌ 在 {name} 物件上找不到 'CinemachineBasicMultiChannelPerlin'！請檢查是否有掛載 Cinemachine Camera 並且開啟了 Noise 設定。");
        }
        else
        {
            amplitudeDefault = perlin.AmplitudeGain;
            frequencyDefault = perlin.FrequencyGain;
        }
    }

    // 啟動攝影機晃動效果的方法
    public void ShakeCamera(float duration, float amplitude, float frequency)
    {
        StartCoroutine(ShakeCoroutine(duration, amplitude, frequency));
    }

    // 協同程序，用於執行攝影機晃動效果
    private IEnumerator ShakeCoroutine(float duration, float amplitude, float frequency)
    {
        // 如果該組件存在，設定晃動參數
        if (perlin != null)
        {
            perlin.AmplitudeGain = amplitude;   // 設定振幅
            perlin.FrequencyGain = frequency;   // 設定頻率
        }

        // 等待晃動持續的時間
        yield return new WaitForSeconds(duration);

        // 恢復初始值，停止晃動
        if (perlin != null)
        {
            perlin.AmplitudeGain = amplitudeDefault;
            perlin.FrequencyGain = frequencyDefault;
        }
    }
}