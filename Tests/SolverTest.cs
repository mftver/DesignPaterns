using Factory.Factories;
using File = Factory.models.File;
using Game;

namespace Tests;

public class SolverTest
{
    [Test]
    public void CanSolveSudoku()
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
        
        var factory = new SudokuFactory();

        foreach (var file in files)
        {
            Assert.That(new BacktraceSolver(factory.Create(file)).Solve(), Is.True);
        }
    }
}