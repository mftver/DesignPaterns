using ConsoleApp1.Models.Interfaces;
using ConsoleApp1.Logic.Interfaces;

namespace ConsoleApp1.Models
{
    internal abstract class BaseSudoku : IValidatable
    {
        private Cell[][] MySudoku { get; }
        private List<IValidatable> groups { get; }

        private Isolver solver;

        public BaseSudoku(Cell[][] mySudoku, List<IValidatable> groups, Isolver solver)
        {
            MySudoku = mySudoku;
            this.groups = groups;
            this.solver = solver;
        }

        public bool validate()
        {
            throw new NotImplementedException();
        }
    }
}
