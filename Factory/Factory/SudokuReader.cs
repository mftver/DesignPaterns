using Model;
using Model.Interfaces;

namespace Factory;

public class SudokuReader
{
    // TODO: Implement actual reader
    public Sudoku Read(string filePath)
    {
        var board = new Cell[9][];
        var groups = new List<IValidatable>();
        for (int i = 0; i < 9; i++)
        {
            board[i] = new Cell[9];
            groups.Append(new Group(new List<Cell>()));
            for (int j = 0; j < 9; j++)
            {
                board[i][j] = new Cell(0, new List<IValidatable>(), i);
            }
        }
        
        return new Sudoku(board, groups);
    }
}