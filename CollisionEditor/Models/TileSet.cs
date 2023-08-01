using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class TileSet : GodotObject
{
    public Vector2I TileSize { get; private set; }
    public List<Tile> Tiles { get; }

    public TileSet(Image image, Vector2I tileSize, 
        Vector2I separation, Vector2I offset, int tileLimit)
    {
        TileSize = tileSize;
        Tiles = new List<Tile>();

        CreateTiles(image, separation, offset, tileLimit);
    }

    public TileSet(int angleCount = 0, Vector2I tileSize = new())
    {
        TileSize = tileSize;
        Tiles = new List<Tile>(angleCount);

        for (var i = 0; i < angleCount; i++)
        {
            Tiles.Add(new Tile(TileSize));
        }
    }

    public async Task<Image> CreateTileMap(Node viewportContainer, int columnCount, 
        Color[] groupColor, int groupOffset, Vector2I separation, Vector2I offset)
    {
        var cell = new Vector2I(TileSize.X + separation.X, TileSize.Y + separation.Y);
        
        int groupCount = groupColor.Length;
        int tilesRowWidth = (Tiles.Count + groupOffset) * groupCount;
        int rowCount = columnCount == 0 ? 1 : Mathf.CeilToInt((double)tilesRowWidth / columnCount);
        if (columnCount == 0)
        {
            columnCount = tilesRowWidth;
        }

        var tileMapSize = new Vector2I(
            offset.X + columnCount * cell.X - separation.X,
            offset.Y + rowCount * cell.Y - separation.Y);
        
        return await DrawTiles(viewportContainer, tileMapSize, groupOffset, 
            groupColor, separation, offset, columnCount, groupCount);
    }

    public void ChangeTile(int tileIndex, Vector2I pixelPosition, bool isLeftButtonPressed)
    {
        Image image = Tiles[tileIndex].GetImage();
        ChangeTileHeight(image, isLeftButtonPressed ? pixelPosition : new Vector2I(pixelPosition.X, int.MinValue));
        Tiles[tileIndex].SetImage(image);
    }

    public void InsertTile(int tileIndex)
    {
        Tiles.Insert(tileIndex, new Tile(TileSize));
    }

    public void RemoveTile(int tileIndex)
    {
        Tiles.RemoveAt(tileIndex);
    }

    public void UnloadTiles()
    {
        for (var i = 0; i < Tiles.Count; i++)
        {
            Tiles[i] = new Tile(TileSize);
        }
    }

    private void ChangeTileHeight(Image image, Vector2I pixelPosition)
    {
        Func<int, Color> getColor = CreateFuncGetColor(pixelPosition.Y);
        for (var y = 0; y < TileSize.Y; y++)
        {
            image.SetPixel(pixelPosition.X, y, getColor(y));
        }
    }

    private static Func<int, Color> CreateFuncGetColor(int pixelPositionY)
    {
        if (pixelPositionY == int.MinValue)
        {
           return _ => Colors.Transparent;
        }
        return positionY => positionY >= pixelPositionY ? Colors.Black : Colors.Transparent;
    }

    private void CreateTiles(Image tileMap, Vector2I separation, Vector2I offset, int tileLimit)
    {
        var cellCount = new Vector2I(
            (tileMap.GetWidth() - offset.X) / (TileSize.X + separation.X),
            (tileMap.GetHeight() - offset.Y) / (TileSize.Y + separation.Y));

        var number = 0;
        for (var y = 0; y < cellCount.Y; y++)
        {
            for (var x = 0; x < cellCount.X; x++)
            {
                if (tileLimit != 0 && ++number >= tileLimit) return;
                CreateTileFromTileMap(tileMap, GetTilePosition(new Vector2I(x, y), separation, offset));
            }
        }
    }

    private void CreateTileFromTileMap(Image tileMap, Vector2I tilePosition)
    {
        var tile = new Tile(TileSize);
        
        Image image = tile.GetImage();
        image.BlitRect(tileMap, new Rect2I(tilePosition, TileSize), new Vector2I());
        tile.SetImage(image);
        
        Tiles.Add(tile);
    }

    private async Task<Image> DrawTiles(GodotObject viewportContainer, Vector2I tileMapSize, int groupOffset, 
        IReadOnlyList<Color> groupColor, Vector2I separation, Vector2I offset, int columnCount, int groupCount)
    {
        var viewport = new SubViewport()
        {
            Size = tileMapSize,
            TransparentBg = true,
            RenderTargetUpdateMode = SubViewport.UpdateMode.Always
        };
        viewportContainer.CallDeferred("add_child", viewport);
        
        var position = new Vector2I();
        Sprite2D blankSprite = new Tile(TileSize).Sprite;
        Resource material = GD.Load("res://Shaders/shader_material_color.tres");
        int groupTileCount = Tiles.Count + groupOffset;
        for (var group = 0; group < groupCount; group++)
        {
            var spriteContainer = new Node2D();
            Color color = groupColor[group];
            var shaderColor = new Vector3(color.R, color.G, color.B);
            spriteContainer.Material = (Material)material.Duplicate();
            ((ShaderMaterial)spriteContainer.Material).SetShaderParameter("Color", shaderColor);
            
            for (var i = 0; i < groupTileCount; i++)
            {
                Vector2I tilePosition = GetTilePosition(position, separation, offset);
                position = GetNextPosition(columnCount, position);
                AddTileSprite(i < Tiles.Count ? Tiles[i].Sprite : blankSprite, spriteContainer, tilePosition);
            }
            viewport.AddChild(spriteContainer);
        }
        
        await ToSignal(RenderingServer.Singleton, RenderingServer.SignalName.FramePostDraw);
        Image image = viewport.GetTexture().GetImage();
        viewport.QueueFree();
        return image;
    }

    private static void AddTileSprite(Node sprite, 
        Node container, Vector2I tilePosition)
    {
        var duplicate = (Sprite2D)sprite.Duplicate();
        duplicate.Centered = false;
        duplicate.Position = tilePosition;
        container.AddChild(duplicate);
    }

    private static Vector2I GetNextPosition(int columnCount, Vector2I position)
    {
        if (position.X + 1 >= columnCount)
        {
            position.X = 0;
            position.Y++;
        }
        else
        {
            position.X++;   
        }

        return position;
    }

    private Vector2I GetTilePosition(Vector2I position, Vector2I separation, Vector2I offset)
    {
        return new Vector2I(
            offset.X + position.X * (TileSize.X + separation.X), 
            offset.Y + position.Y * (TileSize.Y + separation.Y));
    }
}
