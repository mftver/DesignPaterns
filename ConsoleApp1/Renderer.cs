﻿using Model;

namespace Frontend;

public class Renderer
{
    public void Draw(Sudoku sudoku, Coordinate cursorPosition)
    {
        DrawSudoku(sudoku, cursorPosition);
        if (sudoku.Validate())
            DrawVictory();
    }

    private void DrawVictory()
    {
        Console.WriteLine("You win!");
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
                if (cell == null)
                {
                    Console.Write(' ');
                    continue;
                }

                if (cursorPosition.IsEqual(new Coordinate(x, y)))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
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