using Factory.Factories;
using File = Factory.models.File;

namespace Tests.Factories;

public class SudokuFactoryTest
{
    private readonly SudokuFactory _factory = new();


    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test4X4HasCorrectWidth()
    {
        var sudoku = _factory.Create(new File(@"../../../../Sudokus/puzzle.4x4"));

        Assert.That(sudoku.Grid.GetLength(0), Is.EqualTo(4), "Grid size is 4 wide");
    }

    [Test]
    public void CorrectNumbersShownInGrid()
    {
        var files = new List<File>
        {
            new(@"../../../../Sudokus/puzzle.4x4"),
            new(@"../../../../Sudokus/puzzle2.4x4"),
            new(@"../../../../Sudokus/puzzle3.4x4"),
            new(@"../../../../Sudokus/puzzle.6x6"),
            new(@"../../../../Sudokus/puzzle2.6x6"),
            new(@"../../../../Sudokus/puzzle3.6x6"),
            new(@"../../../../Sudokus/puzzle.9x9"),
            new(@"../../../../Sudokus/puzzle2.9x9"),
            new(@"../../../../Sudokus/puzzle3.9x9")
        };

        foreach (var file in files) ValidateSudokuGridRender(file);
    }


    private void ValidateSudokuGridRender(File file)
    {
        var sudoku = _factory.Create(file);

        // Convert grid back to one dimensional list
        var grid = new List<int>();
        for (var i = 0; i < sudoku.Grid.GetLength(0); i++)
        for (var j = 0; j < sudoku.Grid[i].GetLength(0); j++)
            grid.Add(sudoku.Grid[i][j].Number);

        // Convert file contents to list of integers
        var fileContentNumbers = new List<int>();
        var contents = file.Contents();
        foreach (var row in contents)
        {
            // Split string into individual items
            var charArray = row.ToCharArray();
            // Convert each item to integer
            fileContentNumbers.AddRange(charArray.Select(c => c - '0'));
        }

        Assert.That(grid, Is.EqualTo(fileContentNumbers), "Grid contains correct values");
    }
}