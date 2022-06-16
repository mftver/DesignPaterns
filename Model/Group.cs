using Model.Interfaces;

namespace Model;

public class Group : IValidatable
{
    public Group()
    {
        _validatables = new List<IValidatable>();
    }

    private List<IValidatable> _validatables { get; }

    public bool Validate()
    {
        return _validatables.All(c => c.Validate());
    }

    public void AddValidatable(IValidatable validatable)
    {
        _validatables.Add(validatable);
    }

    public void RemoveValidatable(IValidatable validatable)
    {
        _validatables.Remove(validatable);
    }

    public List<IValidatable> GetChildren()
    {
        return _validatables;
    }

    public List<Cell> Cells()
    {
        var cells = new List<Cell>();

        foreach (var validatable in _validatables)
            if (validatable.GetType() == typeof(Cell))
            {
                cells.Add(validatable as Cell);
            }
            else
            {
                var group = validatable as Group;
                cells.AddRange(group.Cells());
            }

        return cells;
    }
}