using Model;

namespace Frontend;

public class InputHandler
{
    private readonly Renderer _renderer;
    private readonly Sudoku _sudoku;
    private readonly Coordinate _cursorPosition;
    private bool _quit;
    private readonly Dictionary<ConsoleKey, Action> _actionKeys;
    private ConsoleKey _keyPressed;

    public InputHandler(Renderer renderer, Sudoku sudoku)
    {
        _renderer = renderer;
        _sudoku = sudoku;
        _quit = false;
        _cursorPosition = new Coordinate(0, 0);
        
        _actionKeys = new Dictionary<ConsoleKey, Action>
        {
            {ConsoleKey.UpArrow, () => MoveCursor(Direction.Up) },
            {ConsoleKey.RightArrow, () => MoveCursor(Direction.Right) },
            {ConsoleKey.DownArrow, () => MoveCursor(Direction.Down) },
            {ConsoleKey.LeftArrow, () => MoveCursor(Direction.Left) },
            {ConsoleKey.W, () => MoveCursor(Direction.Up) },
            {ConsoleKey.D, () => MoveCursor(Direction.Right) },
            {ConsoleKey.S, () => MoveCursor(Direction.Down) },
            {ConsoleKey.A, () => MoveCursor(Direction.Left) },
            // Number keys for filling in numbers
            {ConsoleKey.D1, () => FillInNumber(1) },
            {ConsoleKey.D2, () => FillInNumber(2) },
            {ConsoleKey.D3, () => FillInNumber(3) },
            {ConsoleKey.D4, () => FillInNumber(4) },
            {ConsoleKey.D5, () => FillInNumber(5) },
            {ConsoleKey.D6, () => FillInNumber(6) },
            {ConsoleKey.D7, () => FillInNumber(7) },
            {ConsoleKey.D8, () => FillInNumber(8) },
            {ConsoleKey.D9, () => FillInNumber(9) },
            {ConsoleKey.D0, () => FillInNumber(0) },
            
            // Numpad 
            {ConsoleKey.NumPad0, () => FillInNumber(0) },
            {ConsoleKey.NumPad1, () => FillInNumber(1) },
            {ConsoleKey.NumPad2, () => FillInNumber(2) },
            {ConsoleKey.NumPad3, () => FillInNumber(3) },
            {ConsoleKey.NumPad4, () => FillInNumber(4) },
            {ConsoleKey.NumPad5, () => FillInNumber(5) },
            {ConsoleKey.NumPad6, () => FillInNumber(6) },
            {ConsoleKey.NumPad7, () => FillInNumber(7) },
            {ConsoleKey.NumPad8, () => FillInNumber(8) },
            {ConsoleKey.NumPad9, () => FillInNumber(9) },
            
        };
    }

    public void Run()
    {
        // _sudoku.Updated += (sender, game1) => _renderer.Draw(game1);

        while (!_quit)
        {
            _keyPressed = Console.ReadKey().Key;
            _quit = _keyPressed == ConsoleKey.Escape;

            if (!_actionKeys.TryGetValue(_keyPressed, out var action)) continue;
            
            action.Invoke();
            _keyPressed = 0;
            
            _renderer.Draw(_sudoku, _cursorPosition);
        }

        Console.ReadLine();
    }

    private void FillInNumber(int number)
    {
        var res = _sudoku.TryEnter(_cursorPosition, number);
    }
    private void MoveCursor(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                if (_cursorPosition.Y != 0) _cursorPosition.Y--;
                break;
            case Direction.Right:
                if (_cursorPosition.X != _sudoku.Grid.GetLength(0) - 1) _cursorPosition.X++;
                break;
            case Direction.Down:
                if (_cursorPosition.Y != _sudoku.Grid[_cursorPosition.Y].GetLength(0) - 1) _cursorPosition.Y++;
                break;
            case Direction.Left:
                if (_cursorPosition.X != 0) _cursorPosition.X--;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unknown direction");
        }
    }
}