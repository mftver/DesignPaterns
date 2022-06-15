using Model.Interfaces;

namespace Model;

public class Sudoku
{
    public readonly Cell[][] Grid;
    private List<IValidatable> Groups { get; }

    public Sudoku(Cell[][] grid, List<IValidatable> groups)
    {
        Grid = grid;
        Groups = groups;
    }

    public bool validate()
    {
        return Groups.All(validatable => validatable.Validate());
    }
}