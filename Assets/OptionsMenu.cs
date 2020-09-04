using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System;
public class OptionsMenu : MonoBehaviour
{
    [Header("Video")]
    // UNIVERSAL
    public Volume postProcessingVolume;
    // LWRP
    //public PostProcessVolume postProcessingVolume;
    public Slider gammaSlider;
    public TextMeshProUGUI gammaDisplay;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullScreenModeDropdown;
    public int vSyncCount
    {
        get => vSyncCount;

        set
        {
            vSyncCount = value;
            QualitySettings.vSyncCount = vSyncCount;
        }
    }

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public TextMeshProUGUI masterDisplay;
    public TextMeshProUGUI musicDisplay;
    public TextMeshProUGUI sfxDisplay;

    [Header("Gameplay")]
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityDisplay;
    public Toggle damageNumberToggle;

    private Resolution[] m_Resolutions;
    private string[] m_Qualities;
    private string[] m_FullScreenModes;

    private void ShowPrefs()
    {
        qualityDropdown.value = PlayerPrefs.GetInt("qualityLevel");
        qualityDropdown.RefreshShownValue();

        resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
        resolutionDropdown.RefreshShownValue();

        fullScreenModeDropdown.value = PlayerPrefs.GetInt("fullScreenMode");
        fullScreenModeDropdown.RefreshShownValue();

        float gamma = PlayerPrefs.GetFloat("brightness", 0f);
        gammaDisplay.text = System.Math.Round(gamma, 2).ToString();
        gammaSlider.value = gamma;

        float masterVolume = PlayerPrefs.GetFloat("masterVolume", 0.5f);
        masterDisplay.text = Mathf.RoundToInt(masterVolume * 100) + "%";
        masterSlider.value = masterVolume;

        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        musicDisplay.text = Mathf.RoundToInt(musicVolume * 100) + "%";
        musicSlider.value = musicVolume;

        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
        sfxDisplay.text = Mathf.RoundToInt(sfxVolume * 100) + "%";
        sfxSlider.value = sfxVolume;

        float cameraSens = PlayerPrefs.GetFloat("camSensitivity", 5f);
        sensitivityDisplay.text = System.Math.Round(cameraSens, 2).ToString();
        sensitivitySlider.value = cameraSens;

        damageNumberToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("damageNumbersEnabled", 1));
    }

    private void Start()
    {
        // Populate resolution selection
        m_Resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < m_Resolutions.Length; i++)
        {
            string option = m_Resolutions[i].width + " x " + m_Resolutions[i].height;
            resolutionOptions.Add(option);

            if (m_Resolutions[i].width == Screen.currentResolution.width &&
               m_Resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Remove non-16:9 ratios
        for (int i = 0; i < m_Resolutions.Length; i++)
        {
            if (m_Resolutions[i].width / m_Resolutions[i].height != (16 / 9))
            {
                resolutionOptions.RemoveAt(i);
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Populate quality selection
        m_Qualities = QualitySettings.names;
        qualityDropdown.ClearOptions();
        List<string> qualityOptions = new List<string>();
        int currentQualityIndex = 0;
        for (int i = 0; i < m_Qualities.Length; i++)
        {
            string option = m_Qualities[i];
            qualityOptions.Add(option);

            if (QualitySettings.GetQualityLevel() == i)
            {
                currentQualityIndex = i;
            }
        }
        qualityDropdown.AddOptions(qualityOptions);
        qualityDropdown.value = currentQualityIndex;
        qualityDropdown.RefreshShownValue();

        // Populate quality selection
        m_FullScreenModes = new string[] { "Fullscreen", "Fullscreen Window", "Maximized Window", "Windowed" };
        fullScreenModeDropdown.ClearOptions();
        List<string> fullScreenModeOptions = new List<string>();
        int fullScreenModeIndex = 0;
        for (int i = 0; i < m_FullScreenModes.Length; i++)
        {
            string option = m_FullScreenModes[i];
            fullScreenModeOptions.Add(option);

            if (QualitySettings.GetQualityLevel() == i)
            {
                fullScreenModeIndex = i;
            }
        }
        fullScreenModeDropdown.AddOptions(fullScreenModeOptions);
        fullScreenModeDropdown.value = fullScreenModeIndex;
        fullScreenModeDropdown.RefreshShownValue();

        // Store ref to gamma volume
        postProcessingVolume = GameOptions.instance.m_GammaVolume;

        ShowPrefs();
    }

    #region VIDEO
    public void SetQualityLevel(int qualityIndex)
    {
        qualityDropdown.value = qualityIndex;
        qualityDropdown.RefreshShownValue();

        QualitySettings.SetQualityLevel(qualityIndex, true);
        PlayerPrefs.SetInt("qualityLevel", qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionIndex)
    {
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Resolution resolution = m_Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        PlayerPrefs.Save();
    }

    public void SetFullscreenMode(int fullScreenModeIndex)
    {
        fullScreenModeDropdown.value = fullScreenModeIndex;
        fullScreenModeDropdown.RefreshShownValue();

        Screen.fullScreenMode = (FullScreenMode)fullScreenModeIndex;
        PlayerPrefs.SetInt("fullScreenMode", fullScreenModeIndex);
        PlayerPrefs.Save();
    }

    public void SetBrightness(float brightness)
    {
        if (!postProcessingVolume)
            return;

        gammaSlider.value = brightness;

        // LWRP
        //////////////////////////////////////////////////////////////////////////
        //ColorGrading colorGradingLayer = null;

        //var fx = postProcessingVolume.profile.TryGetSettings(out colorGradingLayer);
        //colorGradingLayer.gamma.value = Vector4.one * brightness;
        //////////////////////////////////////////////////////////////////////////

        // UNIVERSAL
        //////////////////////////////////////////////////////////////////////////
        LiftGammaGain liftGammaGainLayer = null;

        if (GameOptions.instance.m_GammaVolume.profile != null)
        {
            bool fx = GameOptions.instance.m_GammaVolume.profile.TryGet(out liftGammaGainLayer);
        }
        liftGammaGainLayer.gamma.value = Vector4.one * brightness;

        //////////////////////////////////////////////////////////////////////////

        gammaDisplay.text = System.Math.Round(brightness, 2).ToString();

        PlayerPrefs.SetFloat("brightness", brightness);
        PlayerPrefs.Save();
    }
    #endregion

    #region AUDIO
    public void SetMasterVolume(float volume)
    {
        masterSlider.value = volume;

        float finalVolume = Mathf.Log10(volume) * 20;
        if (Mathf.Approximately(finalVolume, -60))
        {
            finalVolume = -80f;
        }

        audioMixer.SetFloat("masterVolume", finalVolume);
        masterDisplay.text = Mathf.RoundToInt(volume * 100) + "%";

        PlayerPrefs.SetFloat("masterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        musicSlider.value = volume;

        float finalVolume = Mathf.Log10(volume) * 20;
        if (Mathf.Approximately(finalVolume, -60))
        {
            finalVolume = -80f;
        }

        audioMixer.SetFloat("musicVolume", finalVolume);
        musicDisplay.text = Mathf.RoundToInt(volume * 100) + "%";

        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxSlider.value = volume;

        float finalVolume = Mathf.Log10(volume) * 20;
        if (Mathf.Approximately(finalVolume, -60))
        {
            finalVolume = -80f;
        }

        audioMixer.SetFloat("sfxVolume", finalVolume);
        sfxDisplay.text = Mathf.RoundToInt(volume * 100) + "%";

        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();
    }
    #endregion

    #region GAMEPLAY
    public void SetCameraSensitivty(float sensitivity)
    {
        sensitivitySlider.value = sensitivity;

        // Apply new Camera sensitivity!
        GameOptions.instance.m_CameraSensitivity = sensitivity;
        sensitivityDisplay.text = System.Math.Round(sensitivity, 2).ToString();

        PlayerPrefs.SetFloat("camSensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public void ToggleDamageNumbers(bool isEnabled)
    {
        damageNumberToggle.isOn = isEnabled;

        // Toggle damage number displaying in game.
        GameOptions.instance.m_DamageNumbersEnabled = isEnabled;

        PlayerPrefs.SetInt("damageNumbersEnabled", Convert.ToInt32(isEnabled));
        PlayerPrefs.Save();
    }
    #endregion
}
