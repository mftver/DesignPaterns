using Model.Interfaces;

namespace Model
{
    internal abstract class BaseSudoku : IValidatable
    {
        private Cell[][] MySudoku { get; }
        private List<IValidatable> Groups { get; }

        public BaseSudoku(Cell[][] mySudoku, List<IValidatable> groups)
        {
            MySudoku = mySudoku;
            this.Groups = groups;
        }

        public bool validate()
        {
            return Groups.All(validatable => validatable.validate());
        }
    }
}
