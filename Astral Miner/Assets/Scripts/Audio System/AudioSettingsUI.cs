using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MasterKey = "MasterVolume";
    private const string AmbienceKey = "AmbienceVolume";
    private const string SFXKey = "SFXVolume";

    private void Start()
    {
        LoadVolumes();

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        ambienceSlider.onValueChanged.AddListener(SetAmbienceVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float value)
    {
        SetMixerVolume("MasterVolume", value);
        PlayerPrefs.SetFloat(MasterKey, value);
    }

    public void SetAmbienceVolume(float value)
    {
        SetMixerVolume("AmbienceVolume", value);
        PlayerPrefs.SetFloat(AmbienceKey, value);
    }

    public void SetSFXVolume(float value)
    {
        SetMixerVolume("SFXVolume", value);
        PlayerPrefs.SetFloat(SFXKey, value);
    }


    private void SetMixerVolume(string parameter, float value)
    {
        float decibels = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;

        audioMixer.SetFloat(parameter, decibels);
    }

    private void LoadVolumes()
    {
        float master = PlayerPrefs.GetFloat(MasterKey, 1f);
        float ambience = PlayerPrefs.GetFloat(AmbienceKey, 1f);
        float sfx = PlayerPrefs.GetFloat(SFXKey, 1f);

        masterSlider.value = master;
        ambienceSlider.value = ambience;
        sfxSlider.value = sfx;

        SetMixerVolume("MasterVolume", master);
        SetMixerVolume("AmbienceVolume", ambience);
        SetMixerVolume("SFXVolume", sfx);
    }
}