using Model.Interfaces;
using Model.Interfaces.Observer;

namespace Model
{
    public class Cell : IValidatable, IDpObservable<NumberSwitch>, IDpObserver<NumberSwitch>
    {
        public int Number { get; private set; }
        private bool IsFixed { get; set; }
        private List<Group> Groups { get; }
        private List<int> PossibleNumbers { get; }

        private readonly SubGroup _subGroup;
        
        private readonly List<IDpObserver<NumberSwitch>> _observers = new();
        
        public Cell(int number, bool isFixed, List<Group> groups, SubGroup subGroup, int maxValue)
        {
            if (number < 0 || number > maxValue)
            {
                throw new ArgumentException("Number must be between 0 and " + maxValue);
            }

            Groups = groups;
            _subGroup = subGroup;
            
            PossibleNumbers = new List<int>();
            for (var i = 0; i <= maxValue; i++)
                PossibleNumbers.Add(i);
            
            
            // Add the cell to the groups
            foreach (var group in groups)
            {
                group.AddCell(this);
            }

            if(!TrySetNumber(number)) throw new ArgumentException("Can't create new cell with number " + number);
            IsFixed = isFixed;
        }

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

        public int GetSubGroupId() => _subGroup.Id;

        public bool Validate()
        {
            return PossibleNumbers.Count > 0;
        }

        public void Subscribe(IDpObserver<NumberSwitch> observer)
        {
            // Do not allow duplicate subscriptions
            if (_observers.Contains(observer)) return;
            _observers.Add(observer);
        }

        public void OnNext(NumberSwitch data)
        {
            foreach (var observer in _observers)
            {
                observer.UpdatePossibleNumbers(data);
            }
        }
        
        public void UpdatePossibleNumbers(NumberSwitch data)
        {
            PossibleNumbers.Add(data.OldNumber);
            PossibleNumbers.Remove(data.NewNumber);
        }

        /***
         * Subscribes to all cells in every validation group to track if their value changes
         */
        public void TriggerSubscription()
        {
            foreach (var cell in Groups.SelectMany(group => group.Cells))
                cell.Subscribe(this);
        }
    }
}
