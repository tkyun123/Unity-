using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndPanel : MonoBehaviour
{
    public static GameEndPanel Instance;
    public TMP_Text EndText;
    public float fadeDuration = 0.3f;
    public CanvasGroup canvasGroup;
    void Awake() => Instance = this;

    public void win(string reason="")
    {
        EndText.text = "YOU WIN" + reason;
        FadeIn();
    }

    public void lose(string reason = "")
    {
        EndText.text = "YOU LOSE" + reason;
        FadeIn();
    }

    public void draw(string reason = "")
    {
        EndText.text = "DRAW" + reason;
        FadeIn();
    }
    public void Continue()
    {
        GameManager.Instance.RestartGame();
        gameObject.SetActive(false);
    }
    public void OnQuit()
    {
        GameManager.Instance.Quit();
    }

    public void FadeIn()
    {
        StartCoroutine(FadePanel(0, 1f)); // 从透明到不透明
    }

    IEnumerator FadePanel(float startAlpha, float targetAlpha)
    {
        canvasGroup = GetComponent<CanvasGroup>();
        float elapsedTime = -0.5f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, (elapsedTime > 0 ? elapsedTime : 0)/ fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetAlpha; // 确保最终值准确
    }
}
