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
        //Debug.Log("x:" + x.ToString() + "y:" + y.ToString()+mask);
        if(!grid[x, y].Play(mask))
        {
            Debug.LogError("Can not Play Chess");
        }
    }
    public bool CheckForWin(Cell[,] board, string player, bool show = false)
    {
        for(int i = 0; i < 3; i++)
        {
            if (board[i, 0].currentMark == player && board[i, 1].currentMark == player && board[i, 2].currentMark == player)
            {
                if (show)
                {
                    board[i, 0].highlight();
                    board[i, 1].highlight();
                    board[i, 2].highlight();
                }
                return true;
            }
            if (board[0, i].currentMark == player && board[1, i].currentMark == player && board[2, i].currentMark == player)
            {
                if (show)
                {
                    board[0, i].highlight();
                    board[1, i].highlight();
                    board[2, i].highlight();
                }
                return true;
            }
        }
        if (board[0, 0].currentMark == player && board[1, 1].currentMark == player && board[2,2].currentMark == player)
        {
            if (show)
            {
                board[0, 0].highlight();
                board[1, 1].highlight();
                board[2, 2].highlight();
            }
            return true;
        }
        if (board[2, 0].currentMark == player && board[1, 1].currentMark == player && board[0, 2].currentMark == player)
        {
            if (show)
            {
                board[0, 2].highlight();
                board[1, 1].highlight();
                board[2, 0].highlight();
            }
            return true;
        }
        return false;
    }

    public bool CheckForWin(string player)
    {
        return CheckForWin(grid, player,true);
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
    public void CurrentInfo()
    {
        string out_text = "";
        for(int i = 0;i< 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                out_text += grid[i, j].currentMark + ";";
            }
        }
        Debug.Log(out_text);
    }
}

