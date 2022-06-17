using Factory.Interfaces;
using Model;
using Model.Interfaces;
using File = Factory.models.File;

namespace Factory.Factories;

internal class JigsawFactory : IFactory
{
    public Sudoku Create(File file)
    {
        //remove first 10 characters of file content
        var content = file.Contents()[0][10..];
        var characters = content.Split('=');
        
        var gridSize = Convert.ToInt32(Math.Sqrt(characters.Length));

        // Create a group for each column
        var columnGroups = createGroupArray(gridSize);
        var rowGroups = createGroupArray(gridSize);
        var subGroups = new List<SubGroup>();

        // Fill grid
        var grid = new Cell[gridSize][];
        for (var column = 0; column < gridSize; column++)
        {
            grid[column] = new Cell[gridSize];
            var rowGroup = rowGroups[column];

            for (var row = 0; row < gridSize; row++)
            {
                var cellValue = characters[column * gridSize + row][0] - '0';
                var subGroupIndex = characters[column * gridSize + row][2] - '0';

                var subGroup = subGroups.ElementAtOrDefault(subGroupIndex);
                if (subGroup == null)
                {
                    subGroup = new SubGroup(subGroupIndex);
                    subGroups.Insert(subGroupIndex, subGroup);
                }

                var groups = new List<Group>
                {
                    columnGroups[row],
                    rowGroup,
                    subGroup
                };

                var isFixed = cellValue != 0;

                grid[column][row] = new Cell(cellValue, isFixed, groups, subGroup, gridSize);
            }
        }

        var allGroups = new List<IValidatable>();
        allGroups.AddRange(columnGroups);
        allGroups.AddRange(rowGroups);
        allGroups.AddRange(subGroups);

        // Tell cells to subscribe to each other
        return new Sudoku(grid, allGroups);
    }
    
    private Group[] createGroupArray(int size)
    {
        var groupArray = new Group[size];
        for (var i = 0; i < size; i++)
            groupArray[i] = new Group();

        return groupArray;
    }

    public bool Supports(File file)
    {
        var fileExtensions = new List<string>
        {
            ".jigsaw"
        };

        return fileExtensions.Contains(file.Extension);
    }
}