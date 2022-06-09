using Model;

namespace Frontend;

public class InputHandler
{
    private readonly Renderer _renderer;
    private readonly Sudoku _sudoku;
    private bool _quit;
    private readonly Dictionary<ConsoleKey, Action> _actionKeys;
    private ConsoleKey _keyPressed;

    public InputHandler(Renderer renderer, Sudoku sudoku)
    {
        _renderer = renderer;
        _sudoku = sudoku;
        _quit = false;
        
        _actionKeys = new Dictionary<ConsoleKey, Action>
        {
            // {ConsoleKey.UpArrow, () => game.MovePlayer(Direction.North)},
            // {ConsoleKey.LeftArrow, () => game.MovePlayer(Direction.West)},
            // {ConsoleKey.RightArrow, () => game.MovePlayer(Direction.East)},
            // {ConsoleKey.DownArrow, () => game.MovePlayer(Direction.South)},
            // {ConsoleKey.W, () => game.MovePlayer(Direction.North)},
            // {ConsoleKey.A, () => game.MovePlayer(Direction.West)},
            // {ConsoleKey.D,() =>  game.MovePlayer(Direction.East)},
            // {ConsoleKey.S, () => game.MovePlayer(Direction.South)},
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

            if (_actionKeys.TryGetValue(_keyPressed, out var action))
            {
                action.Invoke();

                _keyPressed = 0;
            }
        }

        Console.ReadLine();
    }
}