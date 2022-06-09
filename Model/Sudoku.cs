using Model.Interfaces;

namespace Model;

public class Sudoku : BaseSudoku
{
    public Sudoku(Cell[][] mySudoku, List<IValidatable> groups) : base(mySudoku, groups)
    {
    }
}