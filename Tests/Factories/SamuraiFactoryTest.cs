using Factory.Factories;
using Model;
using File = Factory.models.File;

namespace Tests.Factories;

public class SamuraiFactoryTest
{
    private readonly SudokuFactory _factory = new();


    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CorrectNumbersShownInGrid()
    {
        var files = new List<File>
        {
            new(@"../../../../Sudokus/puzzle.samurai"),
            new(@"../../../../Sudokus/puzzle2.samurai"),
            new(@"../../../../Sudokus/puzzle3.samurai"),
        };

        foreach (var file in files) ValidateSudokuGridRender(file);
    }


    private void ValidateSudokuGridRender(File file)
    {
        var sudoku = _factory.Create(file);

        // Convert grid back to one dimensional list
        var grid = sudoku.Grid.Cast<Cell>().Select(c => c.Number).ToList();

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

        // Loop through array and compare each item to the file contents
        for (var i = 0; i < grid.Count; i++)
        {
            Assert.That(fileContentNumbers[i], Is.EqualTo(grid[i]));
        }
    }
}