﻿using Model.Interfaces;
using Model.Interfaces.Observer;

namespace Model;

public class Cell : IValidatable, IDpObservable<NumberSwitch>, IDpObserver<NumberSwitch>
{
    public readonly int MaxValue;
    private readonly List<IDpObserver<NumberSwitch>> _observers = new();
    public SubGroup SubGroup { get; }
    private bool _isCurrentNumberValid = true;
    public int Number { get; private set; }
    public int HelperNumber { get; private set; }
    public bool IsFixed { get; private set; }
    public List<Group> Groups { get; }
    public List<int> PossibleNumbers { get; }

    public Cell(int number, List<Group> groups, SubGroup subGroup, int maxValue)
    {
        if (number < 0 || number > maxValue) throw new ArgumentException("Number must be between 0 and " + maxValue);

        Groups = groups;
        SubGroup = subGroup;
        MaxValue = maxValue;

        PossibleNumbers = new List<int>();
        for (var i = 0; i <= maxValue; i++)
            PossibleNumbers.Add(i);
        
        // Add the cell to the groups
        foreach (var group in groups) group.AddValidatable(this);

        if (!TrySetNumber(number)) throw new ArgumentException("Can't create new cell with number " + number);
        HelperNumber = number;
        IsFixed = number != 0;
    }

    public void Subscribe(IDpObserver<NumberSwitch> observer)
    {
        // Do not allow duplicate subscriptions
        if (_observers.Contains(observer)) return;
        _observers.Add(observer);
    }
    
    public void addGroups(List<Group> groups)
    {
        foreach (var group in groups) group.AddValidatable(this);
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
        var isValid = true;
        
        // check if the cells number is in any linked group
        foreach (var group in Groups.Where(group => group.Cells().Where(cell => cell != this).Any(cell => cell.Number == Number)))
        {
            isValid = false;
        }
        
        return isValid;
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
        if (newNumber > MaxValue) return;
        UpdatePossibleNumbers(new NumberSwitch(Number, newNumber));
        Number = newNumber;
    }
    
    public void setHelperNumber(int newNumber)
    {
        if (IsFixed) return;
        if (newNumber > MaxValue) return;
        HelperNumber = newNumber;
    }

    public int GetSubGroupId()
    {
        return SubGroup.Id;
    }

    /***
     * Subscribes to all cells in every validation group to track if their value changes
     */
    public void TriggerSubscription()
    {
        foreach (var cell in Groups.SelectMany(group => group.Cells())) cell.Subscribe(this);
    }
}