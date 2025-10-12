// 2025/10/12 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    private const string MasterVolumeParam = "VolumeMaster";
    private const string BGMVolumeParam = "VolumeBGM";
    private const string SFXVolumeParam = "VolumeSFX";

    void Start()
    {
        InitializeSliders();
        AddSliderListeners();
    }

    // Initialize the slider values based on the current mixer settings
    private void InitializeSliders()
    {
        if (audioMixer != null)
        {
            float masterVolume, bgmVolume, sfxVolume;

            // Retrieve current volume values from the Audio Mixer
            audioMixer.GetFloat(MasterVolumeParam, out masterVolume);
            audioMixer.GetFloat(BGMVolumeParam, out bgmVolume);
            audioMixer.GetFloat(SFXVolumeParam, out sfxVolume);

            // Set slider values
            masterVolumeSlider.value = masterVolume;
            bgmVolumeSlider.value = bgmVolume;
            sfxVolumeSlider.value = sfxVolume;
        }
    }

    // Add listeners to sliders to update mixer values when changed
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

    // Update the mixer parameter with the slider value
    private void SetVolume(string parameter, float value)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat(parameter, value);
        }
    }
}