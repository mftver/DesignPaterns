using Model.Interfaces;
using Model.Interfaces.Observer;

namespace Model;

public class Cell : IValidatable, IDpObservable<NumberSwitch>, IDpObserver<NumberSwitch>
{
    private readonly int _maxValue;
    private readonly List<IDpObserver<NumberSwitch>> _observers = new();
    private readonly SubGroup _subGroup;
    private bool _isCurrentNumberValid = true;

    public Cell(int number, bool isFixed, List<Group> groups, SubGroup subGroup, int maxValue)
    {
        if (number < 0 || number > maxValue) throw new ArgumentException("Number must be between 0 and " + maxValue);

        Groups = groups;
        _subGroup = subGroup;
        _maxValue = maxValue;

        PossibleNumbers = new List<int>();
        for (var i = 0; i <= maxValue; i++)
            PossibleNumbers.Add(i);
        
        // Add the cell to the groups
        foreach (var group in groups) group.AddValidatable(this);

        if (!TrySetNumber(number)) throw new ArgumentException("Can't create new cell with number " + number);
        IsFixed = isFixed;
    }

    public int Number { get; private set; }
    public bool IsFixed { get; private set; }
    private List<Group> Groups { get; }
    private List<int> PossibleNumbers { get; }

    public void Subscribe(IDpObserver<NumberSwitch> observer)
    {
        // Do not allow duplicate subscriptions
        if (_observers.Contains(observer)) return;
        _observers.Add(observer);
    }

    public void OnNext(NumberSwitch data)
    {
        foreach (var observer in _observers) observer.UpdatePossibleNumbers(data);
    }

    public void UpdatePossibleNumbers(NumberSwitch data)
    {
        // Do change numbers if their are not valid
        var isNewNumberValid = PossibleNumbers.Contains(data.NewNumber);

        if (_isCurrentNumberValid)
            PossibleNumbers.Add(data.OldNumber);
        if (isNewNumberValid)
            PossibleNumbers.Remove(data.NewNumber);

        _isCurrentNumberValid = isNewNumberValid;
    }

    public bool Validate()
    {
        return PossibleNumbers.All(val => val == 0);
    }

    /**
         * Tries to set the number the cell, but will not do so if it's validation fails
         */
    public bool TrySetNumber(int newNumber)
    {
        if (IsFixed) return false;
        if (!PossibleNumbers.Contains(newNumber)) return false;

        var update = new NumberSwitch(Number, newNumber);
        OnNext(update);
        UpdatePossibleNumbers(update);
        Number = newNumber;

        return true;
    }

    /**
         * Sets the number of the cell regardless of whether it is a valid number
         */
    public void SetNumber(int newNumber)
    {
        if (IsFixed) return;
        if (newNumber > _maxValue) return;
        UpdatePossibleNumbers(new NumberSwitch(Number, newNumber));
        Number = newNumber;

        // Print all possible numbers to the console
        Console.WriteLine("Possible numbers for cell " + Number + ": ");
        foreach (var number in PossibleNumbers) Console.Write(number + " ");
    }

    public int GetSubGroupId()
    {
        return _subGroup.Id;
    }

    /***
     * Subscribes to all cells in every validation group to track if their value changes
     */
    public void TriggerSubscription()
    {
        foreach (var cell in Groups.SelectMany(group => group.Cells())) cell.Subscribe(this);
    }
}