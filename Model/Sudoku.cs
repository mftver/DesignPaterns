using Model.Interfaces;

namespace Model;

internal class Sudoku : BaseSudoku
{
    public Sudoku(Cell[][] mySudoku, List<IValidatable> groups) : base(mySudoku, groups)
    {
    }
}