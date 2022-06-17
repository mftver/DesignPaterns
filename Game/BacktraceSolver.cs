using Game.Interfaces;
using Model;

namespace Game;

public class BacktraceSolver : ISolver
{
    private readonly int _n;
    private readonly int _maxNumber;
    private readonly Sudoku _sudoku;

    public BacktraceSolver(Sudoku sudoku)
    {
        _sudoku = sudoku;
        _n = sudoku.Grid.GetLength(0);
        _maxNumber = _sudoku.Grid[0,0].MaxValue;
    }

    public void Solve()
    {
        solveSudoku(_sudoku.Grid,0,0);
    }

    private bool solveSudoku(Cell[,] grid, int row, int col)
    {
        /* if we have reached the 8th row and 9th column (0 indexed matrix), we are returning true to avoid further backtracking */
        if (row == _n - 1 && col == _n)
            return true;

        /* Check if column value  becomes 9, we move to next row and column start from 0 */
        if (col == _n)
        {
            row++;
            col = 0;
        }

        /* Check if the current position of the grid already contains value >0, we iterate for next column */
        if (grid[row, col].Number != 0)
            return solveSudoku(grid, row, col + 1);

        for (var num = 1; num < _maxNumber + 1; num++)
        {
            /* Check if it is safe to place the num (1-9)  in the given row ,col ->we move to next column */
            if (isSafe(grid, row, col, num))
            {
                /*  assigning the num in the current (row,col)  position of the grid and assuming our assigned num in the position is correct */
                grid[row, col].SetNumber(num);

                /* Checking for next possibility with next column */
                if (solveSudoku(grid, row, col + 1))
                    return true;
            }

            /* removing the assigned num , since our assumption was wrong , and we go for next assumption with diff num value */
            grid[row, col].SetNumber(0);
        }

        return false;
    }

    private bool isSafe(Cell[,] grid, int row, int col, int num)
    {
        /* Check if we find the same num in the similar row, we return false */
        for (var x = 0; x < _n; x++)
        {
            if (grid[row, x] == null) continue;
            
            if (grid[row, x].Number == num)
            {
                return false;
            }
        }
           

        /* Check if we find the same num in the similar column, we return false */
        for (var x = 0; x < _n; x++)
        {
            if (grid[x, col] == null) continue;
            
            if (grid[x, col].Number == num)
            {
                return false;
            }
        }
        
        if (grid[row,col] == null) return false;
        
        /* Check if we find the same num in the particular 3*3 matrix, we return false else true*/
        var subgroup = grid[row,col].SubGroup.GetChildren().Cast<Cell>().ToList();
        return subgroup.All(cell => cell.Number != num);
    }
}