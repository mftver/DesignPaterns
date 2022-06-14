using Model.Interfaces;

namespace Model
{
    public class Cell : IValidatable
    {
        public int Number { get; private set; }
        private bool IsFixed { get; }
        private List<IValidatable> Groups { get; }
        private List<int> PossibleNumbers { get; }

        public readonly int SubGroup;

        public Cell(int number, List<IValidatable> groups, int subGroup)
        {
            if (number is < 0 or > 9)
            {
                throw new ArgumentOutOfRangeException();
            }

            Number = number;
            this.IsFixed = true;
            this.Groups = groups;
            SubGroup = subGroup;
            PossibleNumbers = new List<int>
            {
                1,2,3,4,5,6,7,8,9
            };
            PossibleNumbers.Remove(number);
        }

        public Cell(List<IValidatable> groups, int subGroup)
        {
            this.IsFixed = false;
            this.Groups = groups;
            SubGroup = subGroup;
            PossibleNumbers = new List<int>
            {
                1,2,3,4,5,6,7,8,9
            };
        }

        public bool removePossibleNumber(int number)
        {
            return number is > 0 and <= 9 && PossibleNumbers.Remove(number);
        }

        public bool validate()
        {
            return PossibleNumbers.Count > 0;
        }
    }
}
