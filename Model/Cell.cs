using Model.Interfaces;

namespace Model;

public class Cell : IValidatable
{
    private readonly SubGroup _subGroup;

    public Cell(int number, List<IValidatable> groups, SubGroup subGroup)
    {
        if (number is < 0 or > 9) throw new ArgumentOutOfRangeException();

        Number = number;
        IsFixed = true;
        Groups = groups;
        _subGroup = subGroup;
        PossibleNumbers = new List<int>
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9
        };
        RemovePossibleNumber(number);

        // Add the cell to the groups
        foreach (var group in groups.Where(group => group.GetType() == typeof(Group))) ((Group)group).AddValidateable(this);
    }

    public int Number { get; }
    private bool IsFixed { get; }
    private List<IValidatable> Groups { get; }
    private List<int> PossibleNumbers { get; }

    public bool Validate()
    {
        return PossibleNumbers.Count > 0;
    }

    public bool RemovePossibleNumber(int number)
    {
        return number is > 0 and <= 9 && PossibleNumbers.Remove(number);
    }

    public int GetSubGroupId()
    {
        return _subGroup.Id;
    }
}