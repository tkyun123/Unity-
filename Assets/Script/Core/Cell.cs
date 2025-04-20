using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int x, y;
    public Button button;
    public Image currentImg;
    public string currentMark = "";
    private float fadeDuration = 0.5f;
    public AudioClip move;
    void Start()
    {
        BoardManager.Instance.RegisterCell(x, y, this);
        currentImg = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        currentMark = "";
        move = AudioManager.Instance.move;
    }

    void OnClick()
    {
        if (!this.Empty() || GameManager.Instance.gameEnded || !GameManager.Instance.IsPlayerTurn()) return;
        Play(GameManager.Instance.currentPlayer.ToString());
    }

    public bool Play(string mark)
    {
        currentMark = mark;

        if (currentMark == "X") currentImg.sprite = GameManager.Instance.chessXImg;
        if (currentMark == "O") currentImg.sprite = GameManager.Instance.chessOImg;

        StartCoroutine(FadeInChessPiece());
        AudioManager.Instance.PlaySFX(move);
        button.interactable = false;
        GameManager.Instance.recordOperation(this.x, this.y);
        if (BoardManager.Instance.CheckForWin(currentMark))
        {
            GameManager.Instance.EndGame(mark);
        }
        else
        {
            GameManager.Instance.SwitchTurn();
        }

        return true;
    }

    public bool Equal(Cell other)
    {
        return other.currentMark == this.currentMark;
    }

    public bool Empty()
    {
        return this.currentMark == "";
    }

    public void SetMark(string mark)
    {
        currentMark = mark;
    }

    public void Reset()
    {
        currentMark = "";
        button.interactable = true;

        Color color = currentImg.color;
        color.a = 0f;
        currentImg.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.Empty() && !GameManager.Instance.gameEnded && GameManager.Instance.IsPlayerTurn())
        {
            this.currentImg.sprite = GameManager.Instance.player.ToString() == "X"
                    ? GameManager.Instance.chessXImg
                    : GameManager.Instance.chessOImg;
            this.currentImg.color = new Color(1f, 1f, 1f, 0.3f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.Empty() && !GameManager.Instance.gameEnded && GameManager.Instance.IsPlayerTurn())
        {
            this.currentImg.color = new Color(1f, 1f, 1f, 0f);
        }    
    }

    private IEnumerator FadeInChessPiece()
    {
        float duration = 0.3f; // Ω•œ‘ ±º‰
        float elapsed = 0f;
        Color color = currentImg.color;
        color.a = 0f;
        currentImg.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            currentImg.color = color;
            yield return null;
        }

        color.a = 1f;
        currentImg.color = color;
    }

    public void Repentance()
    {
        StartCoroutine(FadeAndRemove());
    }

    private IEnumerator<WaitForEndOfFrame> FadeAndRemove()
    {
        
        if (currentImg == null) yield break;

        Color color = currentImg.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            currentImg.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
        this.Reset();
    }
}
