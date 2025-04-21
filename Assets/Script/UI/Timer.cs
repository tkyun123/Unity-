using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public int countdownTime;           // 初始倒计时时间
    public TMP_Text countdownText;     // UI 显示用

    private Coroutine countdownCoroutine;     // 协程实例，方便停止
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
        // 如果已有倒计时在跑，就先停止
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
        StartCountdown(); // 重新开始
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
                // 如果暂停了，每帧等待直到继续
                yield return null;
            }
        }
        countdownText.text = "0.00";
        GameManager.Instance.Timeout();
    }
}

