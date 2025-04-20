using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndPanel : MonoBehaviour
{
    public static GameEndPanel Instance;
    public TMP_Text EndText;
    void Awake() => Instance = this;
    public void win()
    {
        EndText.text = "YOU WIN";
    }

    public void lose()
    {
        EndText.text = "YOU LOSE";
    }
    public void Continue()
    {
        gameObject.SetActive(false);
        GameManager.Instance.RestartGame();
    }
    public void OnQuit()
    {
        GameManager.Instance.Quit();
    }
}
