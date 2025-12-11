using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [Header("Audio Mixer 設定")]
    public AudioMixer audioMixer;

    [Header("UI Sliders (拉桿)")]
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    // 注意：這些名稱必須跟你 AudioMixer 裡 Expose 出來的參數名稱完全一樣
    private const string MasterVolumeParam = "VolumeMaster";
    private const string BGMVolumeParam = "VolumeBGM";
    private const string SFXVolumeParam = "VolumeSFX";

    void Start()
    {
        InitializeSliders();
        AddSliderListeners();
    }

    // 1. 初始化：從 Mixer 抓取目前的分貝數，轉換成 0~1 顯示在 Slider 上
    private void InitializeSliders()
    {
        if (audioMixer != null)
        {
            float masterdB, bgmdB, sfxdB;

            // 取得目前的 dB 數值
            audioMixer.GetFloat(MasterVolumeParam, out masterdB);
            audioMixer.GetFloat(BGMVolumeParam, out bgmdB);
            audioMixer.GetFloat(SFXVolumeParam, out sfxdB);

            // 【關鍵修正】將 dB (-80 ~ 0) 轉換為 線性數值 (0 ~ 1)
            if (masterVolumeSlider != null)
                masterVolumeSlider.value = Mathf.Pow(10, masterdB / 20);

            if (bgmVolumeSlider != null)
                bgmVolumeSlider.value = Mathf.Pow(10, bgmdB / 20);

            if (sfxVolumeSlider != null)
                sfxVolumeSlider.value = Mathf.Pow(10, sfxdB / 20);
        }
    }

    // 2. 監聽：當玩家拖動 Slider 時，去改變 Mixer
    private void AddSliderListeners()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.onValueChanged.AddListener((value) => SetVolume(MasterVolumeParam, value));
        }

        if (bgmVolumeSlider != null)
        {
            bgmVolumeSlider.onValueChanged.AddListener((value) => SetVolume(BGMVolumeParam, value));
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.AddListener((value) => SetVolume(SFXVolumeParam, value));
        }
    }

    // 3. 設定音量：將 Slider 的 0~1 轉換回 dB 寫入 Mixer
    private void SetVolume(string parameter, float sliderValue)
    {
        if (audioMixer != null)
        {
            // 【關鍵修正】將 線性數值 (0 ~ 1) 轉換為 dB (-80 ~ 0)
            // 如果拉到 0，直接設為 -80dB (靜音)，避免 Log(0) 數學錯誤
            float dbValue = sliderValue <= 0.001f ? -80f : Mathf.Log10(sliderValue) * 20;

            audioMixer.SetFloat(parameter, dbValue);
        }
    }
}