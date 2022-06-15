using Model.Interfaces;

namespace Model
{
    public class Group : IValidatable
    {
        private List<Cell> _cells { get; }

        public Group()
        {
            _cells = new List<Cell>();
        }
        
        public void AddCell(Cell cell)
        {
            _cells.Add(cell);
        }
        
        

        public bool Validate()
        {
            //validate cells and validate if the whole group contains integers multiple times

            throw new NotImplementedException();
        }
    }
}
