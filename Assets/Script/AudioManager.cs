using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;    // ±≥æ∞“Ù¿÷
    public AudioSource sfxSource;    // “Ù–ß
    public AudioClip move;
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

    // ≤•∑≈±≥æ∞“Ù¿÷
    public void PlayBGM(AudioClip clip, float volume = 1f, bool loop = true)
    {
        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.loop = loop;
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

}
