using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameOptions : MonoBehaviour
{
    public static GameOptions instance;

    public int m_TargetFrameRate = 120;
    public int m_VsyncCount = 1;
    public float m_CameraSensitivity = 5.0f;
    public bool m_DamageNumbersEnabled = true;
    public Volume m_GammaVolume;
    public AudioMixer m_AudioMixer;

    private void Awake()
    {
        // Singleton check
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // DDOL
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = m_TargetFrameRate;
        QualitySettings.vSyncCount = m_VsyncCount;
        LoadPrefs();
    }

    private void LoadPrefs()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"), true);

        Screen.fullScreenMode = (FullScreenMode)PlayerPrefs.GetInt("fullScreenMode");

        LiftGammaGain liftGammaGainLayer = null;
        if (GameOptions.instance.m_GammaVolume.profile != null)
        {
            bool fx = GameOptions.instance.m_GammaVolume.profile.TryGet(out liftGammaGainLayer);
        }
        liftGammaGainLayer.gamma.value = Vector4.one * PlayerPrefs.GetFloat("brightness", 0f);


        float finalVolume = Mathf.Log10(PlayerPrefs.GetFloat("masterVolume", 0.5f)) * 20;
        if (Mathf.Approximately(finalVolume, -60))
        {
            finalVolume = -80f;
        }

        m_AudioMixer.SetFloat("masterVolume", finalVolume);

        float finalSfxVolume = Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume", 0.5f)) * 20;
        if (Mathf.Approximately(finalVolume, -60))
        {
            finalSfxVolume = -80f;
        }

        m_AudioMixer.SetFloat("sfxVolume", finalSfxVolume);

        float finalMusicVolume = Mathf.Log10(PlayerPrefs.GetFloat("musicVolume", 0.5f)) * 20;
        if (Mathf.Approximately(finalVolume, -60))
        {
            finalMusicVolume = -80f;
        }

        m_AudioMixer.SetFloat("musicVolume", finalMusicVolume);

        m_CameraSensitivity = PlayerPrefs.GetFloat("camSensitivity", 5f);

        m_DamageNumbersEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("damageNumbersEnabled", 1));
    }
}