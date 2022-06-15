using Model.Interfaces;

namespace Model;

public class Sudoku
{
    public readonly Cell[][] Grid;

    public Sudoku(Cell[][] grid, List<IValidatable> groups)
    {
        Grid = grid;
        Groups = groups;
    }

    private List<IValidatable> Groups { get; }

    public bool validate()
    {
        return Groups.All(validatable => validatable.Validate());
    }
}