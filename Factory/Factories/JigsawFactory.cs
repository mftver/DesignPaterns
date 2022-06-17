using Factory.Interfaces;
using Model;
using Model.Interfaces;
using File = Factory.models.File;

namespace Factory.Factories;

internal class JigsawFactory : BaseSudokuFactory
{
    public override Sudoku Create(File file)
    {
        //remove first 10 characters of file content
        var content = file.Contents()[0][10..];
        var numbers = content.Split('=');
        
        // Tell cells to subscribe to each other
        return CreateSudoku(numbers);
    }

    public override bool Supports(File file)
    {
        var fileExtensions = new List<string>
        {
            ".jigsaw"
        };

        return fileExtensions.Contains(file.Extension);
    }
}