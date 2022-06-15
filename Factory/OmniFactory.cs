using Model;

namespace Factory
{
    internal class OmniFactory
    {
        public Sudoku CreateSudoku(string filePath) {
            var sudokuString = ReadFile(filePath);
        }
        
        private string[] ReadFile(string filePath) {
            var lines = File.ReadAllLines(filePath);
            return lines;
        }
    }
}
