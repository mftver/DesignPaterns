using Factory.Interfaces;
using Model;
using Model.Interfaces;
using File = Factory.models.File;

namespace Factory.Factories;

public abstract class BaseSudokuFactory : IFactory
{
    public abstract Sudoku Create(File file);

    public abstract bool Supports(File file);

    public Sudoku CreateSudoku(string[] characters)
    {
        var gridSize = Convert.ToInt32(Math.Sqrt(characters.Length));
        
        // Create a group for each column
        var columnGroups = createGroupArray(gridSize);
        var rowGroups = createGroupArray(gridSize);
        var subGroups = new List<SubGroup>();

        // Fill grid
        var grid = new Cell[gridSize,gridSize];


        for (var x = 0; x < gridSize; x++)
        {
            for (var y = 0; y < gridSize; y++)
            {
                var cellValue = characters[x * gridSize + y][0] - '0';
                var subGroupIndex = tryGetSubGroupIndex(characters[x * gridSize + y]);
                if (subGroupIndex == -1)
                {
                    subGroupIndex = calculateSubGroupIndex(x, y, gridSize);
                }
                
                var subGroup = subGroups.ElementAtOrDefault(subGroupIndex);
                
                if (subGroup == null)
                {
                    subGroup = new SubGroup(subGroupIndex);
                    subGroups.Insert(subGroupIndex, subGroup);
                }

                var groups = new List<Group>
                {
                    columnGroups[y],
                    rowGroups[x],
                    subGroup
                };

                grid[x,y] = new Cell(cellValue, groups, subGroup, gridSize);
            }
        }

        var allGroups = new List<IValidatable>();
        allGroups.AddRange(columnGroups);
        allGroups.AddRange(rowGroups);
        allGroups.AddRange(subGroups);

        // Tell cells to subscribe to each other
        return new Sudoku(grid, allGroups);
    }
    
    private int tryGetSubGroupIndex(string cell)
    {
        var subGroupIndex = -1;
        try
        {
            subGroupIndex = cell[2] - '0';
        }
        catch (IndexOutOfRangeException e)
        {
            
        }

        return subGroupIndex;
    }
    
    private Group[] createGroupArray(int size)
    {
        var groupArray = new Group[size];
        for (var i = 0; i < size; i++)
            groupArray[i] = new Group();

        return groupArray;
    }

    private int calculateSubGroupIndex(int x, int y, int gridSize)
    {
        if (gridSize == 9)
            return x / (gridSize / 3) * 3 + y / (gridSize / 3);
        if (gridSize == 6)
            return x / (gridSize / 3) * 2 + y / (gridSize / 2);
        if (gridSize == 4)
            return x / (gridSize / 2) * 2 + y / (gridSize / 2);

        throw new ArgumentException("Invalid grid size");
    }
}