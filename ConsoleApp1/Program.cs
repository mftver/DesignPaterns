using System.Text;
using Factory;

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

            var sudoku = factory.CreateSudoku(@"../../../../Sudokus/puzzle.4x4");

            var renderer = new Renderer();

            var inputReader = new InputHandler(renderer, sudoku);
            inputReader.Run();
        }
    }
}

