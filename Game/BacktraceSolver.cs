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
        var startCoordinate = new Coordinate(0, 0);

        if (_sudoku.FindCell(startCoordinate)!.IsFixed)
        {
            startCoordinate = MoveCoordinate(startCoordinate);
        }

        SolveCell(startCoordinate);
    }

    private bool SolveCell(Coordinate coordinate)
    {
        var cell = _sudoku.FindCell(coordinate);

        for (var i = 1; i <= cell.MaxValue; i++)
        {
            //if (isSolved) return true;
            if (!cell.TrySetNumber(7)) continue;
        
            var nextCoordinate = MoveCoordinate(coordinate);
        
            if (nextCoordinate == null)
            {
                return _sudoku.Validate();
            }
            return SolveCell(nextCoordinate);
        }

        return false;
    }

    private bool isLastCoordinate(Coordinate coordinate)
    {
        return coordinate.X == _gridSize - 1 && coordinate.Y == _gridSize - 1;
    }

    private Coordinate? MoveCoordinate(Coordinate currentCoordinate)
    {
        var newCoordinate = new Coordinate(currentCoordinate.X, currentCoordinate.Y);

        if (newCoordinate.X == _gridSize - 1)
        {
            newCoordinate.X = 0;
            newCoordinate.Y++;
        }
        else
        {
            newCoordinate.X++;
        }

        if (isLastCoordinate(newCoordinate)) return null;
        var cell = _sudoku.FindCell(newCoordinate);

        if (cell == null || cell.IsFixed) return MoveCoordinate(newCoordinate);

        return newCoordinate;
    }
}