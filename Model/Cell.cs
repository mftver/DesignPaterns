using Model.Interfaces;

namespace Model
{
    internal class Cell : IValidatable
    {
        private int Number { get; set; }
        private bool IsFixed { get; }
        private List<IValidatable> Groups { get; }
        private List<int> PossibleNumbers { get; }

        public Cell(int number, List<IValidatable> groups)
        {
            if (number is < 0 or > 9)
            {
                throw new ArgumentOutOfRangeException();
            }

            Number = number;
            this.IsFixed = true;
            this.Groups = groups;
            PossibleNumbers = new List<int>
            {
                1,2,3,4,5,6,7,8,9
            };
            PossibleNumbers.Remove(number);
        }

        public Cell(List<IValidatable> groups)
        {
            this.IsFixed = false;
            this.Groups = groups;
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
