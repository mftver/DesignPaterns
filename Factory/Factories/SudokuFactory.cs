using Model;
using File = Factory.models.File;

namespace Factory.Factories;

internal class SudokuFactory : BaseSudokuFactory
{
    public override Sudoku Create(File file)
    {
        var numbers = file.Contents()[0].ToCharArray().Select( c => c.ToString()).ToArray();
        
        return CreateSudoku(numbers);
    }

    public override bool Supports(File file)
    {
        var fileExtensions = new List<string>
        {
            ".4x4",
            ".6x6",
            ".9x9"
        };

        return fileExtensions.Contains(file.Extension);
    }
}