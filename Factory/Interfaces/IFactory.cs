using Model;
using File = Factory.models.File;

namespace Factory.Interfaces;

internal interface IFactory
{
    Sudoku Create(File file);

    bool Supports(File file);
}