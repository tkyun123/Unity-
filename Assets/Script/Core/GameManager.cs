using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum Player { X, O }
    public Player player;
    public Player ai;
    public Player currentPlayer;
    public bool gameEnded;
    public Sprite chessXImg;
    public Sprite chessOImg;
    public Image playerAvatar;
    public Image aiAvatar;
    public TMP_Text playerText;
    public TMP_Text aiText;
    public int playerScore;
    public int AiScore;
    public int difficulty;
    public GameObject gameSettingsUI;
    public GameObject gameEndUI;
    [SerializeField] private string gameSceneName = "StartMenu";
    private Stack<Vector2Int> historyOperation = new Stack<Vector2Int>();
    void Awake() => Instance = this;

    public void SwitchTurn()
    {
        if (BoardManager.Instance.BoardFull())
        {
            EndGame("None");
            return;
        }
        currentPlayer = (currentPlayer == Player.X) ? Player.O : Player.X;
        if(!IsPlayerTurn())
        {
            AIController.Instance.AIAction();
        }
    }
    public void Start()
    {
        if(player.ToString() == "X")
        {
            playerAvatar.sprite = chessXImg;
            aiAvatar.sprite = chessOImg;
        }
        else
        {
            playerAvatar.sprite = chessOImg;
            aiAvatar.sprite = chessXImg;
        }
        playerScore = 0;
        AiScore = 0;
        if (!IsPlayerTurn())
        {
            AIController.Instance.AIAction();
        }
    }

    public bool IsPlayerTurn()
    {
        return currentPlayer == player;
    }

    public void EndGame(string winner)
    {
        gameEnded = true;
        //UIManager.Instance.ShowGameOver(winner);
        if(winner == ai.ToString())
        {
            AiScore++;
            gameEndUI.SetActive(true);
            GameEndPanel.Instance.lose();
        }
        if(winner == player.ToString())
        {
            playerScore++;
            gameEndUI.SetActive(true);
            GameEndPanel.Instance.win();
        }
        playerText.text = playerScore.ToString();
        aiText.text = AiScore.ToString();

        Debug.Log(winner + " WIN ");
    }

    public void RestartGame()
    {
        gameEnded = false;
        currentPlayer = Player.X;
        BoardManager.Instance.ResetBoard();
        int difficulty = PlayerPrefs.GetInt("Difficulty", 0);
        AIController.Instance.difficulty = difficulty;
        int firstMove = PlayerPrefs.GetInt("FirstMove", 0);
        if(firstMove == 0)
        {
            currentPlayer = player;
        }
        else
        {
            currentPlayer = ai;

            AIController.Instance.FirstMove();
        }
        //UIManager.Instance.HideGameOver();
        historyOperation.Clear();
    }

    public void recordOperation(int x,int y)
    {
        this.historyOperation.Push(new Vector2Int(x, y));
    }
    public void Repentance()
    {
        if(historyOperation.Count <= 2)
        {
            Debug.Log("Can not repentance operation");
            return;
        }
        Vector2Int first =  historyOperation.Pop();
        BoardManager.Instance.grid[first.x, first.y].Repentance();
        Vector2Int second = historyOperation.Pop();
        BoardManager.Instance.grid[second.x, second.y].Repentance();
    }
    
    public void Options()
    {
        gameSettingsUI.SetActive(true);
        GameSettingsUI.Instance.SetttingShow();
    }

    public void Quit()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}

