using Factory.Interfaces;
using Model;
using File = Factory.models.File;

namespace Factory.Factories
{
    internal class SudokuFactory : IFactory
    {
        public Sudoku Create(File file)
        {
            throw new NotImplementedException();
        }

        public bool Supports(File file)
        {
            var fileExtensions = new List<string>()
            {
                ".4x4",
                ".6x6",
                ".9x9",
            };

            return fileExtensions.Contains(file.Extension);
        }
    }
}
