using Model;

namespace Factory.Interfaces
{
    internal interface IFactory
    {
        Sudoku Create(string[] input);
    }
}
