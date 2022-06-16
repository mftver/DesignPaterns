using Model.Interfaces;

namespace Model
{
    public class Group : IValidatable
    {
        public List<Cell> Cells { get; private set; }

        public Group()
        {
            Cells = new List<Cell>();
        }
        
        public void AddCell(Cell cell)
        {
            Cells.Add(cell);
        }
        public bool Validate() => Cells.All(c => c.Validate());
    }
}
