using Game.Interfaces;
using Model;

namespace Game;

public class BacktraceSolver : ISolver
{
    public void Solve(Sudoku sudoku)
    {
        var gridSize = sudoku.Grid.GetLength(0);
        var currentCoordinate = new Coordinate(0, 0);
        var lastTriedValue = 1;
        
        var solved = false;
        while (!solved)
        {
            // Check if current coordinate is null or fixed
            if (sudoku.FindCell(currentCoordinate) == null || sudoku.FindCell(currentCoordinate).IsFixed)
            {
                currentCoordinate = MoveCoordinate(true, currentCoordinate, gridSize);
                continue;
            }
            
            var moveForward = sudoku.TryEnter(currentCoordinate, lastTriedValue);
            if (moveForward) { // Correct value was entered at current coordinate
                currentCoordinate = MoveCoordinate(moveForward, currentCoordinate, gridSize);
                lastTriedValue = 1;
            } else if (lastTriedValue == gridSize) // we tried all values and failed, backtrace
            {
                sudoku.TryEnter(currentCoordinate, 0);
                currentCoordinate = MoveCoordinate(false, currentCoordinate, gridSize);
                lastTriedValue = 1;
            } else { // Not correct value, try next value
                lastTriedValue++;
            }

            solved = sudoku.Validate();
        }
    }

    private Coordinate MoveCoordinate(bool forward, Coordinate currentCoordinate, int gridSize)
    {
        var newCoordinate = new Coordinate(currentCoordinate.X, currentCoordinate.Y);
        if (forward)
        {
            if (newCoordinate.X == gridSize - 1)
            {
                newCoordinate.X = 0;
                newCoordinate.Y++;
            }
            else
            {
                newCoordinate.X++;
            }
        }
        else
        {
            if (newCoordinate.X == 0)
            {
                newCoordinate.X = gridSize - 1;
                newCoordinate.Y--;
            }
            else
            {
                newCoordinate.X--;
            }
        }
        
        return newCoordinate;
    }
}