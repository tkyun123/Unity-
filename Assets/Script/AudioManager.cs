using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;    // ��������
    public AudioSource sfxSource;    // ��Ч
    public AudioClip move;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip draw;
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

    public void Start()
    {
        float sound = PlayerPrefs.GetFloat("SoundVolume", 1f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SetBGMVolume(music);
        SetSFXVolume(sound);
    }
    // ���ű�������
    public void PlayBGM()
    {
        bgmSource.loop = true;
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

    // ��ͣ����
    public void PauseMusic()
    {
        bgmSource.Pause();
    }

}
