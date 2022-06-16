using Model.Interfaces;
using Model.Interfaces.Observer;

namespace Model
{
    public class Cell : IValidatable, IDpObservable<NumberSwitch>, IDpObserver<NumberSwitch>
    {
        public int Number { get; private set; }
        public bool IsFixed { get; private set; }
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
            Update(update);
            Number = newNumber;
            
            return true;
        }

        public int GetSubGroupId()
        {
            return _subGroup.Id;
        }

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
                observer.Update(data);
            }
        }

        public void Update(NumberSwitch data)
        {
            PossibleNumbers.Add(data.OldNumber);
            PossibleNumbers.Remove(data.NewNumber);
        }

        public void TriggerSubscription()
        {
            // Subscribe to all cells in every group
            foreach (var cell in Groups.SelectMany(group => group.Cells))
                cell.Subscribe(this);
        }
    }
}
