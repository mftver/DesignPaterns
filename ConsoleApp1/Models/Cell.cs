using ConsoleApp1.Models.Interfaces;

namespace ConsoleApp1.Models
{
    internal class Cell : IValidatable
    {
        private int Number { get; set; }
        private bool isFixed { get; }
        private List<IValidatable> cells { get; }

        public Cell(int number, List<IValidatable> cells)
        {
            Number = number;
            this.isFixed = true;
            this.cells = cells;
        }

        public Cell(List<IValidatable> cells)
        {
            this.isFixed = false;
            this.cells = cells;
        }

        public bool validate()
        {
            throw new NotImplementedException();
        }
    }
}
