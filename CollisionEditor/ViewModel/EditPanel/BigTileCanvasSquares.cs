using Godot;

public partial class BigTileCanvasSquares : Control
{
    private CollisionEditorMain _screen;
    private readonly BigTile _bigTile;
    private BigTileSquare _squareBlue;
    private BigTileSquare _squareGreen;
    private byte _transparency;
    private int _transparencyChangeSpeed = 15;
    private readonly Vector2I _gridOffset = new(1, 1);

    private byte Transparency
    {
        get => _transparency;
        set
        {
            _transparency = value;
            _squareBlue.Color.A8 = value;
            _squareGreen.Color.A8 = value;
            QueueRedraw();
        }
    }

    public BigTileCanvasSquares(BigTile bigTile)
    {
        _bigTile = bigTile;
    }

    public override void _Ready()
    {
        _squareBlue = new BigTileSquare(Colors.Blue);
        _squareGreen = new BigTileSquare(Colors.Green);
        
        _screen = CollisionEditorMain.Screen;

        _bigTile.MinimumSizeChanged += OnSizeChanged;

        MouseExited += () => _transparencyChangeSpeed = -Mathf.Abs(_transparencyChangeSpeed);
        MouseEntered += () => _transparencyChangeSpeed = Mathf.Abs(_transparencyChangeSpeed);
    }

    public override void _Process(double delta)
    {
        UpdateTransparency();
        CheckClicks();
    }

    public override void _Draw()
    {
        DrawSquare(_squareBlue);
        DrawSquare(_squareGreen);
    }

    private void UpdateTransparency()
    {
        var canvas = new Rect2(new Vector2(), Size);
        int sign = canvas.HasPoint(GetLocalMousePosition()) ? 1 : -1;
        _transparencyChangeSpeed = sign * Mathf.Abs(_transparencyChangeSpeed);
        
        double transparency = Transparency + _transparencyChangeSpeed;
        Transparency = (byte)Mathf.Clamp(transparency, byte.MinValue, byte.MaxValue);
    }
    
    private void CheckClicks()
    {
        if (_transparencyChangeSpeed < 0 || _bigTile.TileScale == 0) return;
        
        Vector2I mousePosition = (Vector2I)GetLocalMousePosition()
            / _bigTile.TileScale * _bigTile.TileScale + _gridOffset;
        
        bool isLeft = CheckClickOnSquare(mousePosition, "click_left", _squareBlue, _squareGreen);
        bool isRight = CheckClickOnSquare(mousePosition, "click_right", _squareGreen, _squareBlue);

        if ((isLeft || isRight) && _squareBlue.IsActive && _squareGreen.IsActive)
        {
            _screen.SetAngleFromLine(_screen.TileIndex, 
                (Vector2I)_squareBlue.Rectangle.Position, 
                (Vector2I)_squareGreen.Rectangle.Position);
        }
    }

    private static bool CheckClickOnSquare(Vector2I mousePosition, 
        StringName action, BigTileSquare main, BigTileSquare other)
    {
        if (!Input.IsActionJustPressed(action)) return false;
        
        if (main.IsActive && main.Rectangle.Position == mousePosition)
        {
            main.IsActive = false;
            return true;
        }
        
        main.IsActive = true;
        main.Rectangle.Position = mousePosition;

        if (other.Rectangle.Position == mousePosition)
        {
            other.IsActive = false;   
        }
        return true;
    }

    private void DrawSquare(BigTileSquare square)
    {
        if (!square.IsActive) return;
        DrawRect(square.Rectangle, square.Color);
    }

    private void OnSizeChanged()
    {
        _squareBlue.Rectangle.Position = (_squareBlue.Rectangle.Position - _gridOffset) / CustomMinimumSize;
        _squareGreen.Rectangle.Position = (_squareGreen.Rectangle.Position - _gridOffset) / CustomMinimumSize;
        CustomMinimumSize = _bigTile.CustomMinimumSize;
        _squareBlue.Rectangle.Position = _squareBlue.Rectangle.Position * CustomMinimumSize + _gridOffset;
        _squareGreen.Rectangle.Position = _squareGreen.Rectangle.Position * CustomMinimumSize + _gridOffset;
        
        Vector2 squareSize = CustomMinimumSize / _screen.TileSet.TileSize - _gridOffset;
        _squareBlue.Rectangle.Size = squareSize;
        _squareGreen.Rectangle.Size = squareSize;
    }
}