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

            var reader = new SudokuReader();
            var sudoku = reader.Read(@"./Levels/TempleOfDoom.json");

            var renderer = new Renderer();

            var inputReader = new InputHandler(renderer, sudoku);
            inputReader.Run();
        }
    }
}

