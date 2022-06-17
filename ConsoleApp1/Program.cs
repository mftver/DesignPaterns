using System.Text;
using Factory;
using File = Factory.models.File;

namespace Frontend;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WindowWidth = 200;
        Console.WindowHeight = 50;
        Console.CursorVisible = false;

        var factory = new OmniFactory();

        var testFile = new File(@"../../../../Sudokus/puzzle.jigsaw");

        var sudoku = factory.Create(testFile);

        var renderer = new Renderer();

        var inputReader = new InputHandler(renderer, sudoku);
        inputReader.Run();
    }
}