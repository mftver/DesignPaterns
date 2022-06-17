using Model.Interfaces;

namespace Model;

public class Sudoku
{
    public readonly Cell?[,] Grid;

    public Sudoku(Cell[,] grid, List<IValidatable> groups)
    {
        Grid = grid;
        Groups = groups;
        CreateCellSubscriptions();
    }

    private List<IValidatable> Groups { get; }

    public bool Validate()
    {
        return Groups.All(validatable => validatable.Validate());
    }

    private void CreateCellSubscriptions()
    {
        // Create subscriptions
        foreach (var cell in Grid)
        {
            cell?.TriggerSubscription();
        }

        // Broadcast their initial values
        foreach (var cell in Grid)
        {
            if (cell == null) continue;
            var nextVal = new NumberSwitch(0, cell.Number);
            cell.OnNext(nextVal);
        }
    }

    public bool TryEnter(Coordinate coordinate, int number)
    {
        var cell = FindCell(coordinate);
        if (cell == null) throw new Exception("Can't find cell");
        
        return cell.TrySetNumber(number);
    }

    public void Enter(Coordinate coordinate, int number)
    {
        var cell = FindCell(coordinate);
        if (cell == null) throw new Exception("Can't find cell");
        cell.SetNumber(number);
    }

    public Cell? FindCell(Coordinate coordinate)
    {
        return Grid[coordinate.X, coordinate.Y];
    }
}