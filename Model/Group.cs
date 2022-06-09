using Model.Interfaces;

namespace Model
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
            //validate cells and validate if the whole group contains integers multiple times

            throw new NotImplementedException();
        }
    }
}
