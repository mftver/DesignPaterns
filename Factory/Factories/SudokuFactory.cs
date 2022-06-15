using Factory.Interfaces;
using Model;
using Model.Interfaces;

namespace Factory.Factories
{
    internal class SudokuFactory : IFactory
    {
        public Sudoku Create(string[] input)
        {
            var sudokuString = input[0];
            var gridSize = Convert.ToInt32(Math.Sqrt(sudokuString.Length));
            
            
            // Create a group for each column
            var columnGroups = createGroupArray(gridSize);
            var rowGroups = new List<IValidatable>();
            var subGroups = new List<IValidatable>();
            
            // Fill grid
            var grid = new Cell[gridSize][];
            for (var y = 0; y < gridSize; y++)
            {
                grid[y] = new Cell[gridSize];
                // Create group for Y axis
                var rowGroup = new Group();
                rowGroups.Add(rowGroup);
                
                for (var x = 0; x < gridSize; x++)
                {
                    // Get groups for this cell
                    var groups = new List<IValidatable>();
                    groups.Add(columnGroups[x]);
                    groups.Add(rowGroup);

                    var cellValue = sudokuString[y * gridSize + x] - '0';
                    
                    var subGroupIndex = calculateSubGroupIndex(x, y , gridSize);
                    
                    var subGroup = subGroups.ElementAtOrDefault(subGroupIndex);
                    if (subGroup == null)
                    {
                        subGroup = new Group();
                        subGroups.Insert(subGroupIndex, subGroup);
                    }

                    groups.Add(subGroup);
                    grid[y][x] = new Cell(cellValue, groups, subGroupIndex);
                }
            }

            var allGroups = new List<IValidatable>();
            allGroups.AddRange(columnGroups);
            allGroups.AddRange(rowGroups);
            allGroups.AddRange(subGroups);
            
            return new Sudoku(grid, allGroups);
            
        }
        
        private Group[] createGroupArray(int size)
        {
            var groupArray = new Group[size];
            for (var i = 0; i < size; i++)
            {
                groupArray[i] = new Group();
            }
            return groupArray;
        }

        private int calculateSubGroupIndex(int x, int y, int gridSize)
        {
            if (gridSize == 9)
            {
                return (y / (gridSize / 3)) * 3 + x / (gridSize / 3);
            } else if (gridSize == 6)
            {
                return (y / (gridSize / 3)) * 2 + x / (gridSize / 2);
            } else if (gridSize == 4)
            {
                return (y / (gridSize / 2)) * 2 + x / (gridSize / 2);
            }
            
            throw new ArgumentException("Invalid grid size");
        }
    }
}
