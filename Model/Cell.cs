using Model.Interfaces;

namespace Model
{
    public class Cell : IValidatable
    {
        public int Number { get; private set; }
        private bool IsFixed { get; }
        private List<IValidatable> Groups { get; }
        private List<int> PossibleNumbers { get; }

        private readonly SubGroup _subGroup;

        public Cell(int number, List<IValidatable> groups, SubGroup subGroup)
        {
            if (number is < 0 or > 9)
            {
                throw new ArgumentOutOfRangeException();
            }

            Number = number;
            this.IsFixed = true;
            this.Groups = groups;
            _subGroup = subGroup;
            PossibleNumbers = new List<int>
            {
                1,2,3,4,5,6,7,8,9
            };
            PossibleNumbers.Remove(number);
            
            // Add the cell to the groups
            foreach (var group in groups.Where(group => group.GetType() == typeof(Group)))
            {
                ((Group)group).AddCell(this);
            }
        }

        public bool removePossibleNumber(int number)
        {
            return number is > 0 and <= 9 && PossibleNumbers.Remove(number);
        }

        public int GetSubGroupId()
        {
            return _subGroup.Id;
        }

        public bool Validate()
        {
            return PossibleNumbers.Count > 0;
        }
    }
}
