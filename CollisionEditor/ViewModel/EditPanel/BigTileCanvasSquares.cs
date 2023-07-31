using Godot;

public partial class BigTileCanvasSquares : Control
{
    private readonly BigTile _bigTile;
    private BigTileSquare _squareBlue;
    private BigTileSquare _squareGreen;
    private byte _transparency;
    private int _transparencyChangeSpeed = 15;
    private Vector2I _lastMousePosition;
    private bool _isTileEditReady;
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

        _bigTile.MinimumSizeChanged += OnSizeChanged;
        CollisionEditorMain.TileIndexChangedEvents += DisableSquares;
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
        if (_bigTile.TileScale == 0 || _transparencyChangeSpeed < 0) return;
        
        Vector2I mouseGridPosition = (Vector2I)GetLocalMousePosition() / _bigTile.TileScale;

        if (CollisionEditorMain.IsTileMode)
        {
            DisableSquares();
            CheckTileMode(mouseGridPosition);
            return;
        }

        CheckAngleMode(mouseGridPosition * _bigTile.TileScale + _gridOffset);
    }

    private void CheckAngleMode(Vector2I mousePosition)
    {
        bool isLeftClick = CheckClickOnSquare(mousePosition, "click_left", _squareBlue, _squareGreen);
        bool isRightClick = CheckClickOnSquare(mousePosition, "click_right", _squareGreen, _squareBlue);
        
        if ((isLeftClick || isRightClick) && _squareBlue.IsActive && _squareGreen.IsActive)
        {
            CollisionEditorMain.SetAngleFromLine(CollisionEditorMain.TileIndex, 
                (Vector2I)_squareBlue.Rectangle.Position, 
                (Vector2I)_squareGreen.Rectangle.Position);
        }
    }

    private void CheckTileMode(Vector2I mousePosition)
    {
        bool isLeftClick = Input.IsActionPressed("click_left");
        bool isRightClick = Input.IsActionPressed("click_right");
        
        if (!isLeftClick && !isRightClick)
        {
            _isTileEditReady = true;
            return;
        }
        
        if (_lastMousePosition != mousePosition)
        {
            _lastMousePosition = mousePosition;
            _isTileEditReady = true;
        }
        
        if (!_isTileEditReady) return;
        CollisionEditorMain.TileSet.ChangeTile(CollisionEditorMain.TileIndex, mousePosition, isLeftClick);
        CollisionEditorMain.UpdateTile();
        _isTileEditReady = false;
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
        
        Vector2 squareSize = CustomMinimumSize / CollisionEditorMain.TileSet.TileSize - _gridOffset;
        _squareBlue.Rectangle.Size = squareSize;
        _squareGreen.Rectangle.Size = squareSize;
    }

    private void DisableSquares()
    {
        _squareBlue.IsActive = false;
        _squareGreen.IsActive = false;
    }
}