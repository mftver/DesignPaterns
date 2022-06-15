using System.Text;
using Factory;
using Factory.models;
using File = Factory.models.File;

namespace Frontend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WindowWidth = 200;
            Console.WindowHeight = 50;
            Console.CursorVisible = false;

            var factory = new OmniFactory();

            var testFile = new File(@"../../../../Sudokus/puzzle.9x9");

            var sudoku = factory.Create(testFile);

            var renderer = new Renderer();

            var inputReader = new InputHandler(renderer, sudoku);
            inputReader.Run();
        }
    }
}

