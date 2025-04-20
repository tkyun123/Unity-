using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public Cell[,] grid = new Cell[3, 3];

    void Awake() => Instance = this;


    public void RegisterCell(int x, int y, Cell cell)
    {
        grid[x, y] = cell;
    }

    public void OperatorChess(int x, int y, string mask)
    {
        Debug.Log("x:" + x.ToString() + "y:" + y.ToString());
        if(!grid[x, y].Play(mask))
        {
            Debug.LogError("Can not Play Chess");
        }
    }
    public bool CheckForWin(Cell[,] board,string player)
    {
        // 检查行列对角线
        // 如果有胜者，调用 GameManager.Instance.EndGame()
        for(int i = 0; i < 3; i++)
        {
            if (board[i, 0].currentMark == player && board[i, 1].currentMark == player && board[i, 2].currentMark == player)
            {
                return true;
            }
            if (board[0, i].currentMark == player && board[1, i].currentMark == player && board[2, i].currentMark == player)
            {
                return true;
            }
        }
        if (board[0, 0].currentMark == player && board[1, 1].currentMark == player && board[2,2].currentMark == player)
        {
            return true;
        }
        if (board[2, 0].currentMark == player && board[1, 1].currentMark == player && board[0, 2].currentMark == player)
        {
            return true;
        }
        return false;
    }

    public bool CheckForWin(string player)
    {
        return CheckForWin(grid, player);
    }

    public bool BoardFull(Cell[,] board)
    {
        foreach(Cell cell in board)
        {
            if (cell.Empty())
            {
                return false;
            }
        }
        return true;
    }

    public bool BoardFull()
    {
        return BoardFull(grid);
    }
    public void ResetBoard()
    {
        foreach (Cell cell in grid)
        {
            cell.Repentance();
        }
    }
}

