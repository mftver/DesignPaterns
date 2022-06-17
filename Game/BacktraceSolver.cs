using Game.Interfaces;
using Model;

namespace Game;

public class BacktraceSolver : ISolver
{
    private readonly Sudoku _sudoku;
    public BacktraceSolver(Sudoku sudoku)
    {
        _sudoku = sudoku;
    }

    public void Solve()
    {
        SolveCell();
    }

    private bool SolveCell()
    {
        var cell = NextCell();
        if (cell == null)
        {
            return true;
        }

        // var possibleNumbers = cell.PossibleNumbers.Where(val => val != 0).ToList();
        // foreach (var possibleNumber in possibleNumbers)
        for (int i = 0; i < cell.MaxValue; i++)
        {
            if (!cell.TrySetNumber(i)) continue;
            if (SolveCell()) return true;
            cell.TrySetNumber(0);
        }

        return false;
    }

    private Cell? NextCell()
    {
        return _sudoku.Grid.OfType<Cell>().FirstOrDefault(cell => !cell.IsFixed && cell.Number == 0);
    }
}