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
        var grid = new Cell[gridSize][];
        for (var column = 0; column < gridSize; column++)
        {
            grid[column] = new Cell[gridSize];
            var rowGroup = rowGroups[column];

            for (var row = 0; row < gridSize; row++)
            {
                var cellValue = characters[column * gridSize + row][0] - '0';
                var subGroupIndex = tryGetSubGroupIndex(characters[column * gridSize + row]);
                if (subGroupIndex == -1)
                {
                    subGroupIndex = calculateSubGroupIndex(row, column, gridSize);
                }
                
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
    
    private int calculateSubGroupIndex(int x, int y, int gridSize)
    {
        if (gridSize == 9)
            return y / (gridSize / 3) * 3 + x / (gridSize / 3);
        if (gridSize == 6)
            return y / (gridSize / 3) * 2 + x / (gridSize / 2);
        if (gridSize == 4)
            return y / (gridSize / 2) * 2 + x / (gridSize / 2);

        throw new ArgumentException("Invalid grid size");
    }
    
    private Group[] createGroupArray(int size)
    {
        var groupArray = new Group[size];
        for (var i = 0; i < size; i++)
            groupArray[i] = new Group();

        return groupArray;
    }
}