using ConsoleApp1.Models.Interfaces;

namespace ConsoleApp1.Models
{
    internal class Group : IValidatable
    {
        private List<Cell> _cells { get; }

        public Group(List<Cell> cells)
        {
            _cells = cells;
        }

        public bool validate()
        {
            throw new NotImplementedException();
        }
    }
}
