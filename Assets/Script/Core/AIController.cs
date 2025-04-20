using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameManager;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    public static AIController Instance;
    public string mark;
    public string playerMark;
    public int difficulty = 0;
    private Cell[,] board = new Cell[3, 3];
    void Awake() => Instance = this;

    void Start()
    {
        mark = GameManager.Instance.ai.ToString();
        playerMark = GameManager.Instance.player.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstMove()
    {
        Invoke("AIAction", 1);
    }

    public void AIAction()
    {
        board = BoardManager.Instance.grid;
        Vector2Int pos = new Vector2Int(0, 0);
        if(difficulty == 1)
        {
            pos = FindBestMove(board);
        }else
        {
            pos = FindRandomMove(board);
        }
        BoardManager.Instance.OperatorChess(pos.x, pos.y, this.mark);
    }

    public Vector2Int FindRandomMove(Cell[,] board)
    {
        int randomInt = Random.Range(0, 8);
        int i = 0;
        Cell target = new Cell();
        while (i <= randomInt)
        {
            foreach(Cell cell in board)
            {
                if (cell.Empty())
                {
                    i++;
                    target = cell;
                }
                if (i > randomInt) break;
            }
        }
        return new Vector2Int(target.x, target.y);
    }
    public Vector2Int FindBestMove(Cell[,] board)
    {
        int bestScore = int.MinValue;
        Vector2Int bestMove = new Vector2Int(-1, -1);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j].Empty())
                {
                    board[i, j].SetMark(mark);
                    int score = Minimax(board, 0, false);
                    board[i, j].Reset();

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new Vector2Int(i, j);
                    }
                }
            }
        }
        return bestMove;
    }

    public int Minimax(Cell[,] board, int depth, bool isMaximizing, int alpha = int.MinValue, int beta = int.MaxValue)
    {
        // 终局状态判断
        if (BoardManager.Instance.CheckForWin(board, mark)) return 20 - depth;  // 胜利值随深度衰减[7]
        if (BoardManager.Instance.CheckForWin(board, playerMark)) return -20 + depth;
        if (BoardManager.Instance.BoardFull(board)) return 0;

        // 最大化玩家（AI）回合
        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j].Empty())
                    {
                        board[i, j].SetMark(mark); // 模拟落子
                        int score = Minimax(board, depth + 1, false, alpha, beta);
                        board[i, j].Reset(); // 回溯棋盘状态

                        bestScore = Math.Max(score, bestScore);
                        alpha = Math.Max(alpha, bestScore);

                        if (beta <= alpha) // Alpha-Beta剪枝
                            break;
                    }
                }
            }
            return bestScore;
        }
        // 最小化玩家（人类）回合
        else
        {
            int worstScore = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j].Empty())
                    {
                        board[i, j].SetMark(playerMark);
                        int score = Minimax(board, depth + 1, true, alpha, beta);
                        board[i, j].Reset();

                        worstScore = Math.Min(score, worstScore);
                        beta = Math.Min(beta, worstScore);

                        if (beta <= alpha)
                            break;
                    }
                }
            }
            return worstScore;
        }
    }

}
