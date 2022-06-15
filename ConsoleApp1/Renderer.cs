using Model;

namespace Frontend;

public class Renderer
{
    public void Draw(Sudoku sudoku, Coordinate cursorPosition)
    {
        DrawSudoku(sudoku, cursorPosition);
    }

    private void DrawSudoku(Sudoku sudoku, Coordinate cursorPosition)
    {
        Console.Clear();

        var field = sudoku.Field;
        var y = 0;
        foreach (var row in field)
        {
            var x = 0;
            foreach (var cell in row)
            {
                if (cursorPosition.IsEqual(new Coordinate(x, y)))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = (ConsoleColor)cell.SubGroup;
                    Console.ForegroundColor = (ConsoleColor)cell.SubGroup + 2;
                }
                Console.Write(cell.Number);
                x++;
            }
            Console.WriteLine();
            y++;
        }
    }
}