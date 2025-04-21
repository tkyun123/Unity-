using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    public static GameSettingsUI Instance;
    public Slider sliderSoundVolume;
    public Slider sliderMusicVolume;

    public TMP_Dropdown dropdownDifficulty;
    public TMP_Dropdown dropdownFirstMove;
    public AudioClip testSoundClip;
    public GameObject settingsPanel;
    public float fadeDuration = 0.3f;
    void Awake() => Instance = this;

    public void SetttingShow()
    {
        LoadSettingsToUI();
    }

    public void OnSoundSliderChanged(float value)
    {
        Debug.Log("surrces");
        AudioManager.Instance.SetSFXVolume(value);
        OnTrySoundClick();
    }

    public void OnMusicSliderChanged(float value)
    {
        AudioManager.Instance.SetBGMVolume(value);
    }

    public void OnTrySoundClick()
    {
        AudioManager.Instance.PlaySFX(testSoundClip);
    }

    public void OnConfirmClick()
    {
        PlayerPrefs.SetFloat("SoundVolume", sliderSoundVolume.value);
        PlayerPrefs.SetFloat("MusicVolume", sliderMusicVolume.value);
        PlayerPrefs.SetInt("Difficulty", dropdownDifficulty.value);
        PlayerPrefs.SetInt("FirstMove", dropdownFirstMove.value);
        PlayerPrefs.Save();

        settingsPanel.SetActive(false);
        GameManager.Instance.RestartGame();
    }

    public void OnCancelClick()
    {
        float sound = PlayerPrefs.GetFloat("SoundVolume", 1f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        AudioManager.Instance.SetSFXVolume(sound);
        AudioManager.Instance.SetBGMVolume(music);
        settingsPanel.SetActive(false);
        Timer.Instance.Continue();
    }
    void LoadSettingsToUI()
    {
        float sound = PlayerPrefs.GetFloat("SoundVolume", 1f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sliderSoundVolume.value = sound;
        sliderMusicVolume.value = music;
        dropdownDifficulty.value = PlayerPrefs.GetInt("Difficulty", 0);
        dropdownFirstMove.value = PlayerPrefs.GetInt("FirstMove", 0);
    }



}
