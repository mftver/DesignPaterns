using Factory.Interfaces;
using Model;
using Model.Interfaces;
using File = Factory.models.File;

namespace Factory.Factories;

internal class SamuraiFactory : BaseSudokuFactory
{
    public override Sudoku Create(File file)
    {
        var lines = file.Contents();
        var subSudokus = new List<IValidatable>();

        foreach (var line in lines)
        {
            var numbers = line.ToCharArray().Select( c => c.ToString()).ToArray();
        
            subSudokus.Add(CreateSudoku(numbers));
        }

        //convert IValidatableList to SudokuList
        var sudokuList = new List<Sudoku>();
        foreach (var subSudoku in subSudokus)
        {
            sudokuList.Add(subSudoku as Sudoku);
        }
        
        var grid = mergeSudokus(sudokuList);

        return new Sudoku(grid, subSudokus);
    }

    private Cell[][] mergeSudokus(List<Sudoku> subSudokus)
    {
        var grid = new Cell[21][];

        for (var i = 0; i < 21; i++) grid[i] = new Cell[21];

        // put top left sudoku in the top left quadrant
        grid = putSudokuOnGrid(grid, subSudokus.ElementAt(0), 0, 0);
        
        // put top right sudoku in the top right quadrant
        grid = putSudokuOnGrid(grid, subSudokus.ElementAt(1), 12, 0);

        // put bottom left sudoku in the bottom left quadrant
        grid = putSudokuOnGrid(grid, subSudokus.ElementAt(3), 0, 12);
        
        // put bottom right sudoku in the bottom right quadrant
        grid = putSudokuOnGrid(grid, subSudokus.ElementAt(4), 12, 12);
        
        grid = putMiddleSudokuOnGrid(grid, subSudokus.ElementAt(2));
        
        return grid;
    }

    private Cell[][] putMiddleSudokuOnGrid(Cell[][] grid, Sudoku sudoku)
    {
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                if (i < 3 && j < 3)
                {
                    grid[i][j] = mergeCell(grid[i + 6][j + 6], sudoku.Grid[i][j]);
                }
                else if (i > 5 && j < 3)
                {
                    grid[i][j] = mergeCell(grid[i + 6][j + 6], sudoku.Grid[i][j]);
                }
                else if (i < 3 && j > 5)
                {
                    grid[i][j] = mergeCell(grid[i + 6][j + 6], sudoku.Grid[i][j]);
                }
                else if (i > 5 && j > 5)
                {
                    grid[i][j] = mergeCell(grid[i + 6][j + 6], sudoku.Grid[i][j]);
                }
                else
                {
                    grid[i + 6][j + 6] = sudoku.Grid[i][j];
                }
            }
        }

        return grid;
    }

    private Cell mergeCell(Cell mergeInto, Cell cellToMerge)
    {
        mergeInto.addGroups(cellToMerge.Groups);
        return mergeInto;
    }

    private Cell[][] putSudokuOnGrid(Cell[][] grid, Sudoku sudoku, int xOffset, int yOffset)
    {
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                grid[i + xOffset][j + yOffset] = sudoku.Grid[i][j];
            }
        }

        return grid;
    }

    public override bool Supports(File file)
    {
        var fileExtensions = new List<string>
        {
            ".samurai"
        };

        return fileExtensions.Contains(file.Extension);
    }
}