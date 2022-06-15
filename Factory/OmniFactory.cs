using Factory.Factories;
using Model;

namespace Factory
{
    public class OmniFactory
    {
        public Sudoku CreateSudoku(string filePath) {
            var sudokuString = ReadFile(filePath);

            var sudokuFactory = new SudokuFactory();

            return sudokuFactory.Create(sudokuString);
        }
        
        private string[] ReadFile(string filePath) {
            var lines = File.ReadAllLines(filePath);
            return lines;
        }
    }
}
