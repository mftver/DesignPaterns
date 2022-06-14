using Model;

namespace Frontend;

public class Renderer
{
    public void Draw(Sudoku sudoku)
    {
        DrawSudoku(sudoku);
    }

    private void DrawSudoku(Sudoku sudoku)
    {
        Console.Clear();

        var field = sudoku.Field;
        foreach (var row in field)
        {
            foreach (var cell in row)
            {
                Console.BackgroundColor = (ConsoleColor)cell.SubGroup;
                Console.ForegroundColor = (ConsoleColor)cell.SubGroup + 2;
                Console.Write(cell.Number);
            }
            Console.WriteLine();
        }
    }
}