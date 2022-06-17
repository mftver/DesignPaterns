using Game.Interfaces;
using Model;

namespace Game;

public class BacktraceSolver : ISolver
{
    private readonly int _gridSize;
    private readonly Sudoku _sudoku;
    private bool _isSolved = false;
    public BacktraceSolver(Sudoku sudoku)
    {
        _gridSize = sudoku.Grid.GetLength(0);
        _sudoku = sudoku;
    }

    public void Solve()
    {
        SolveCell();
    }

    private bool SolveCell()
    {
        var cell = NextCell();
        if (cell == null) return true;
        foreach (var cellPossibleNumber in cell.PossibleNumbers.Where(x => x != 0).ToList())
        {
            if (!cell.TrySetNumber(cellPossibleNumber)) continue;
            if (SolveCell()) return true;
        }

        return false;
    }

    private bool isLastCoordinate(Coordinate coordinate)
    {
        return coordinate.X == _gridSize - 1 && coordinate.Y == _gridSize - 1;
    }

    private Cell? NextCell()
    {
        return _sudoku.Grid.OfType<Cell>().FirstOrDefault(cell => !cell.IsFixed && cell.Number == 0);
    }
}