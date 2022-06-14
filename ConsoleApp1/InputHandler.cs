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
            // {ConsoleKey.Escape, () => _gameView.DrawEnd()}
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
        }

        Console.ReadLine();
    }

    private void MoveCursor(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                if (_cursorPosition.Y != 0) _cursorPosition.Y--;
                break;
            case Direction.Right:
                if (_cursorPosition.X != _sudoku.Field.GetLength(0) - 1) _cursorPosition.X++;
                break;
            case Direction.Down:
                if (_cursorPosition.Y != _sudoku.Field.GetLength(1) - 1) _cursorPosition.Y++;
                break;
            case Direction.Left:
                if (_cursorPosition.X != 0) _cursorPosition.X--;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unknown direction");
        }
        
        _renderer.Draw(_sudoku);
    }
}