using ConsoleApp1.Logic.Interfaces;
using ConsoleApp1.Models;
using ConsoleApp1.Models.Interfaces;

namespace ConsoleApp1.Sudoku
{
    internal class Sudoku : BaseSudoku
    {
        public Sudoku(Cell[][] mySudoku, List<IValidatable> groups, Isolver solver) : base(mySudoku, groups, solver)
        {
        }
    }
}
