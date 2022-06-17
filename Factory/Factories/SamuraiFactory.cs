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
        var grid = new Cell[21, 21];

        // read the file and get the 9x9 sudokus
        var rawSudokus = file.Contents();

        // create a list of sudokus
        var subsudokus = new IValidatable[rawSudokus.Length];

        //there are 41 subgroups in a samurai sudoku
        var subgroups = new SubGroup[45];

        for (var i = 0; i < 45; i++) subgroups[i] = new SubGroup(i);

        // loop over all the 9x9 sudokus
        for (var sudokuNumber = 0; sudokuNumber < rawSudokus.Length; sudokuNumber++)
            if (sudokuNumber != 2)
                subsudokus[sudokuNumber] = createSubSudoku(sudokuNumber, rawSudokus[sudokuNumber], grid, subgroups);

        //do the middle subgroup as last
        //TODO make the cells not duplicate
        subsudokus[2] = createSubSudoku(2, rawSudokus[2], grid, subgroups);

        return new Sudoku(grid, new List<IValidatable>(subsudokus));
    }

    public bool Supports(File file)
    {
        var fileExtensions = new List<string>
        {
            ".samurai"
        };

        return fileExtensions.Contains(file.Extension);
    }

    private Group createSubSudoku(int sudokuNumber, string rawSudoku, Cell[,] grid, SubGroup[] subgroups)
    {
        var rowId = 0;
        var columnId = 0;

        //create a new 9x9 sudoku
        var subSudoku = new Group();

        // calculate the gridLength (SPOiLER! gridLength = 9)
        var gridLength = Convert.ToInt32(Math.Sqrt(rawSudoku.Length));

        // create rows and columns 
        var rows = new Group[gridLength];
        var columns = new Group[gridLength];

        for (var j = 0; j < gridLength; j++)
        {
            rows[j] = new Group();
            columns[j] = new Group();
            subSudoku.AddValidatable(rows[j]);
            subSudoku.AddValidatable(columns[j]);
        }

        // loop over all the characters in the 9x9 sudoku
        foreach (var c in rawSudoku)
        {
            var cellNumber = c - '0';
            // create new cell and pass the corresponding row and column
            var subgroupId = calculateSubGroupIndex(rowId, columnId, sudokuNumber);

            Cell cell;

            if (sudokuNumber == 2)
            {
                // check if the subgroup is overlapping
                if (subgroupId == 9)
                    cell = grid[rowId + 6,columnId + 6];
                else if (subgroupId == 16)
                    cell = grid[rowId + 6,columnId - 6];
                else if (subgroupId == 30)
                    cell = grid[rowId - 6,columnId + 6];
                else if (subgroupId == 37)
                    cell = grid[rowId - 6,columnId - 6];
                else
                    cell = new Cell(cellNumber,new List<Group>
                    {
                        rows[rowId],
                        columns[columnId]
                    }, subgroups[subgroupId], 9);
            }
            else
            {
                cell = new Cell(cellNumber,new List<Group>
                {
                    rows[rowId],
                    columns[columnId]
                }, subgroups[subgroupId], 9);
            }

            //add cell to groups
            rows[rowId].AddValidatable(cell);
            columns[columnId].AddValidatable(cell);
            subgroups[subgroupId].AddValidatable(cell);

            var x = CalculateXValue(sudokuNumber, columnId) - 1;
            var y = CalculateYValue(sudokuNumber, rowId) - 1;
            //add cell to 2d array
            grid[y,x] = cell;

            // if subgroup isn't already in the 9x9 sudoku, add it
            if (!subSudoku.GetChildren().Contains(subgroups[subgroupId]))
                subSudoku.AddValidatable(subgroups[subgroupId]);

            // add one to the columnId
            columnId++;

            // if columnId is greater than the gridLenght, reset it to 0 and add one to the rowId
            if (columnId < gridLength) continue;
            columnId = 0;
            rowId++;
        }

        return subSudoku;
    }

    private int CalculateYValue(int sudokuNumber, int rowId)
    {
        if (sudokuNumber == 0 || sudokuNumber == 1)
            return rowId + 1;
        if (sudokuNumber == 3 || sudokuNumber == 4)
            return rowId + 13;
        return rowId + 7;
    }

    private int CalculateXValue(int sudokuNumber, int columnId)
    {
        if (sudokuNumber == 0 || sudokuNumber == 3) return columnId + 1;

        if (sudokuNumber == 1 || sudokuNumber == 4) return columnId + 13;

        return columnId + 7;
    }

    private int calculateSubGroupIndex(int x, int y, int sudokuNumber)
    {
        // 2 is the middle aka weird one
        if (sudokuNumber != 2) return y / (9 / 3) * 3 + x / (9 / 3) + sudokuNumber * 9;

        // check coordinate is in overlapping top left sudoku
        if (x < 3 && y < 3) return y / (9 / 3) * 3 + x / (9 / 3) + 0 * 9;

        // check coordinate is in overlapping top right sudoku
        if (x > 5 && y < 3) return y / (9 / 3) * 3 + x / (9 / 3) + 1 * 9;

        // check coordinate is in overlapping bottom left sudoku
        if (x < 3 && y > 5) return y / (9 / 3) * 3 + x / (9 / 3) + 3 * 9;

        // check coordinate is in overlapping bottom right sudoku
        if (x > 5 && y > 5) return y / (9 / 3) * 3 + x / (9 / 3) + 4 * 9;

        return y / (9 / 3) * 3 + x / (9 / 3) + sudokuNumber * 9;
    }
}