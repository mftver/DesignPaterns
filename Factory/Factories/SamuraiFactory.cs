using Factory.Interfaces;
using Model;
using Model.Interfaces;
using File = Factory.models.File;

namespace Factory.Factories;

internal class SamuraiFactory : IFactory
{
    public Sudoku Create(File file)
    {
        //samurai is 21x21
        var samurai = new Cell[21][];

        var subsudokus = new List<IValidatable>();
        
        // read the file and get the 9x9 sudokus
        var rawSudokus = file.Contents();
        
        // create a list of sudokus
        var sudokus = new Sudoku[rawSudokus.Length];

        //there are 41 subgroups in a samurai sudoku
        var subgroups = new SubGroup[41];

        for (var i = 0; i < 41; i++)
        {
            subgroups[i] = new SubGroup(i);
        }

        // TODO: add check for sudoku number 3 since that one is different
        // loop over all the 9x9 sudokus
        for (var sudokuNumber = 0; sudokuNumber < rawSudokus.Length; sudokuNumber++)
        {
            var rowId = 0;
            var columnId = 0;
            
            //create a new 9x9 sudoku
            var subSudoku = new Group();

            // calculate the gridLength (SPOiLER! gridLength = 9)
            var gridLength = Convert.ToInt32(Math.Sqrt(rawSudokus[1].Length));
            
            // create rows and columns 
            var rows = new Group[gridLength];
            var columns = new Group[gridLength];
            
            for (var j = 0; j < gridLength; j++)
            {
                rows[j] = new Group();
                columns[j] = new Group();
                subSudoku.AddValidateable(rows[j]);
                subSudoku.AddValidateable(columns[j]);
            }
            
            // loop over all the characters in the 9x9 sudoku
            foreach (var c in rawSudokus[sudokuNumber])
            {
                // get the subgroup id
                var subgroupId = calculateSubGroupIndex(rowId, columnId, sudokuNumber);
                
                // create new cell and pass the corresponding row and column
                var cell = new Cell(c,new List<IValidatable>()
                {
                    rows[rowId],
                    columns[columnId],
                },subgroups[subgroupId]);
                
                //add cell to groups
                rows[rowId].AddValidateable(cell);
                columns[columnId].AddValidateable(cell);
                subgroups[subgroupId].AddValidateable(cell);

                // if subgroup isn't already in the 9x9 sudoku, add it
                if (!subSudoku.GetChildren().Contains(subgroups[subgroupId]))
                {
                    subSudoku.AddValidateable(subgroups[subgroupId]);
                }

                // if subsudoku isn't already in the list, add it
                if (!subsudokus.Contains(subSudoku))
                {
                    subsudokus.Add(subSudoku);
                }
                
                // add one to the columnId
                columnId++;
                
                // if columnId is greater than the gridLenght, reset it to 0 and add one to the rowId
                if (columnId < gridLength) continue;
                columnId = 0;
                rowId++;
            }
        }
        
        return new Sudoku(samurai, subsudokus);
        
        
        
        
        

        // for (var sudokuNumber = 0; sudokuNumber < rawSudokus.Length; sudokuNumber++)
        // {
        //     var sudokuString = rawSudokus[sudokuNumber];
        //     var gridSize = Convert.ToInt32(Math.Sqrt(rawSudokus[0].Length));
        //
        //     if (sudokuNumber != 2)
        //     {
        //         // Create a group for each column
        //         var columnGroups = createGroupArray(gridSize);
        //         var rowGroups = createGroupArray(gridSize);
        //         var subGroups = new List<SubGroup>();
        //
        //         // Fill grid
        //         var grid = new Cell[gridSize][];
        //         for (var row = 0; row < gridSize; row++)
        //         {
        //             grid[row] = new Cell[gridSize];
        //             var rowGroup = rowGroups[row];
        //
        //             for (var column = 0; column < gridSize; column++)
        //             {
        //                 var cellValue = sudokuString[row * gridSize + column] - '0';
        //                 var subGroupIndex = calculateSubGroupIndex(row, column, sudokuNumber);
        //
        //                 var subGroup = subGroups.ElementAtOrDefault(subGroupIndex);
        //                 if (subGroup == null)
        //                 {
        //                     subGroup = new SubGroup(subGroupIndex);
        //                     subGroups.Insert(subGroupIndex, subGroup);
        //                 }
        //
        //                 var groups = new List<IValidatable>
        //                 {
        //                     columnGroups[column],
        //                     rowGroup,
        //                     subGroup
        //                 };
        //
        //                 grid[column][row] = new Cell(cellValue, groups, subGroup);
        //             }
        //         }
        //
        //         var allGroups = new List<IValidatable>();
        //         allGroups.AddRange(columnGroups);
        //         allGroups.AddRange(rowGroups);
        //         allGroups.AddRange(subGroups);
        //
        //         sudokus[sudokuNumber] = new Sudoku(grid, allGroups);
        //     }
        //     else
        //     {
        //         
        //     }
        // }

        return null;
    }

    public bool Supports(File file)
    {
        var fileExtensions = new List<string>
        {
            ".samurai"
        };

        return fileExtensions.Contains(file.Extension);
    }

    private Group[] createGroupArray(int size)
    {
        var groupArray = new Group[size];
        for (var i = 0; i < size; i++)
            groupArray[i] = new Group();

        return groupArray;
    }

    private int calculateSubGroupIndex(int x, int y, int sudokuNumber)
    {
        //todo catch sudokunumber 3 overlapping subgroups
        return y / (9 / 3) * 3 + x / (9 / 3) + sudokuNumber * 9;
    }

    private Sudoku mergeSudokus(Sudoku[] sudokus)
    {
        
        return null;
    }
}