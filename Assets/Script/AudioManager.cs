using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;    // ��������
    public AudioSource sfxSource;    // ��Ч
    public AudioClip move;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �糡������
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���ű�������
    public void PlayBGM(AudioClip clip, float volume = 1f, bool loop = true)
    {
        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    // ������Ч
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    // ������������
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // ������Ч����
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // ����
    public void MuteAll(bool mute)
    {
        bgmSource.mute = mute;
        sfxSource.mute = mute;
    }

}
