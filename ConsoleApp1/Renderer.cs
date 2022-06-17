using Model;
using Model.Interfaces;

namespace Frontend;

public class Renderer
{
    public bool highlightInvalidCells = false;
    public void Draw(Sudoku sudoku, Coordinate cursorPosition,IState state)
    {
        DrawSudoku(sudoku, cursorPosition,state);
        if (sudoku.Validate())
            DrawVictory();
    }

    private void DrawVictory()
    {
        Console.WriteLine("You win!");
    }

    private void DrawSudoku(Sudoku sudoku, Coordinate cursorPosition,IState state)
    {
        Console.Clear();
        if (state.GetType() == typeof(HelperState))
        {      
            Console.WriteLine("HELPER STATE!");
        }

        var field = sudoku.Grid;
        for (var y = 0; y < field.GetLength(0); y++)
        {
            for (var x = 0; x < field.GetLength(1); x++)
            {
                var cell = field[y, x];

                if (cell == null)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    continue;
                }
                
                var number = cell.Number;
                
                if (state.GetType() == typeof(HelperState))
                {
                    number = cell.HelperNumber;
                }

                if (cursorPosition.IsEqual(new Coordinate(x, y)))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else if (highlightInvalidCells && !cell.IsFixed && number != 0 && !cell.Validate())
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = GetConsoleColor(cell.GetSubGroupId());
                    Console.ForegroundColor = GetConsoleColor(cell.GetSubGroupId() + 2);
                }

                Console.Write(number);
            }
            // Reset console colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }

    private ConsoleColor GetConsoleColor(int seed)
    {
        // Get number of ConsoleColor
        var length = Enum.GetNames(typeof(ConsoleColor)).Length;
        
        Math.DivRem(seed, length, out var remainder);
        
        return (ConsoleColor)remainder;
    }
}