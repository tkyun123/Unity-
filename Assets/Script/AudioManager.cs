using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;    // ±≥æ∞“Ù¿÷
    public AudioSource sfxSource;    // “Ù–ß
    public AudioClip move;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip draw;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // øÁ≥°æ∞±£¡Ù
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        float sound = PlayerPrefs.GetFloat("SoundVolume", 1f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SetBGMVolume(music);
        SetSFXVolume(sound);
    }
    // ≤•∑≈±≥æ∞“Ù¿÷
    public void PlayBGM()
    {
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // ≤•∑≈“Ù–ß
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    // …Ë÷√“Ù¿÷“Ù¡ø
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // …Ë÷√“Ù–ß“Ù¡ø
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // æ≤“Ù
    public void MuteAll(bool mute)
    {
        bgmSource.mute = mute;
        sfxSource.mute = mute;
    }

    // ‘›Õ£“Ù¿÷
    public void PauseMusic()
    {
        bgmSource.Pause();
    }

}
