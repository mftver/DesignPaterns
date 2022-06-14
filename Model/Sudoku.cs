using Model.Interfaces;

namespace Model;

public class Sudoku : BaseSudoku
{
    public readonly Cell[][] Field;
    public Sudoku(Cell[][] mySudoku, List<IValidatable> groups) : base(mySudoku, groups)
    {
        Field = mySudoku;
    }
}