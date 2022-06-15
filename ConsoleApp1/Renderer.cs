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

        var field = sudoku.Grid;
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
                    Console.BackgroundColor = (ConsoleColor)cell.GetSubGroupId();
                    Console.ForegroundColor = (ConsoleColor)cell.GetSubGroupId() + 2;
                }

                Console.Write(cell.Number);
                x++;
            }

            // Reset console colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            y++;
        }
    }
}