using Factory.Interfaces;
using Model;
using File = Factory.models.File;

namespace Factory.Factories
{
    internal class SamuraiFactory : IFactory
    {
        public Sudoku Create(File file)
        {
            throw new NotImplementedException();
        }

        public bool Supports(File file)
        {
            var fileExtensions = new List<string>()
            {
                ".samurai"
            };

            return fileExtensions.Contains(file.Extension);
        }
    }
}
