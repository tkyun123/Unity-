using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public int countdownTime;           // ��ʼ����ʱʱ��
    public TMP_Text countdownText;     // UI ��ʾ��

    private Coroutine countdownCoroutine;     // Э��ʵ��������ֹͣ
    private int currentTime;
    public bool isPaused;

    void Awake() => Instance = this;
    void Start()
    {
        countdownText = GetComponent<TMP_Text>();
        StartCountdown();
    }

    public void StartCountdown()
    {
        // ������е���ʱ���ܣ�����ֹͣ
        StopCountdown();
        currentTime = countdownTime;
        countdownText.color = Color.black;
        countdownCoroutine = StartCoroutine(CountdownCoroutine());
        
    }

    public void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

    }

    public void ResetCountdown()
    {
        Continue();
        StartCountdown(); // ���¿�ʼ
    }

    public void Pause()
    {
        this.isPaused = true;
    }

    public void Continue()
    {
        this.isPaused = false;
    }

    private IEnumerator CountdownCoroutine()
    {
        while (currentTime > 0)
        {
            if (!isPaused)
            {
                countdownText.text = "0.0" + currentTime.ToString();
                if (currentTime <= 3)
                {
                    countdownText.color = Color.red;
                }
                yield return new WaitForSeconds(1f);
                currentTime--;
            }
            else
            {
                // �����ͣ�ˣ�ÿ֡�ȴ�ֱ������
                yield return null;
            }
        }
        countdownText.text = "0.00";
        GameManager.Instance.Timeout();
    }
}

