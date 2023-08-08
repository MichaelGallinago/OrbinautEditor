using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class TileSet : GodotObject
{
    private const string ShaderMaterialColorPath = "res://Shaders/shader_material_color.tres";
    private const byte CollisionMapsMetadataBuffer = 3;
    
    public Vector2I TileSize { get; private set; }
    public List<Tile> Tiles { get; private set; }

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
    
    public static TileSet CreateFromHeights(IReadOnlyList<byte> heights)
    {
        var tileSize = new Vector2I(heights[1], heights[2]);
        var tileSet = new TileSet(0, tileSize);
        int count = heights.Count / tileSize.X;
        for (var i = 0; i < count; i++)
        {
            var tile = new Tile(tileSet.TileSize);
            Image image = tile.GetImage();
            for (var x = 0; x < tileSize.X; x++)
            {
                int transparentHeight = tileSize.Y - heights[i * tileSize.X + x];
                for (var y = 0; y < tileSize.Y; y++)
                {
                    if (y < transparentHeight) continue;
                    image.SetPixel(x, y, Colors.Black);
                }
            }
            tile.SetImage(image);
            tileSet.Tiles.Add(tile);
        }

        return tileSet;
    }
    
    public static TileSet CreateFromWidths(IReadOnlyList<byte> widths)
    {
        var tileSize = new Vector2I(widths[1], widths[2]);
        widths = widths.Skip(CollisionMapsMetadataBuffer).ToArray();
        var tileSet = new TileSet(0, tileSize);
        int count = widths.Count / tileSize.Y;
        for (var i = 0; i < count; i++)
        {
            var tile = new Tile(tileSet.TileSize);
            Image image = tile.GetImage();
            for (var y = 0; y < tileSize.X; y++)
            {
                int transparentWidth = tileSize.X - widths[i * tileSize.Y + y];
                for (var x = 0; x < tileSize.Y; x++)
                {
                    if (x < transparentWidth) continue;
                    image.SetPixel(x, y, Colors.Black);
                }
            }
            tile.SetImage(image);
            tileSet.Tiles.Add(tile);
        }

        return tileSet;
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

    public void MoveTile(int fromTileIndex, int toTileIndex)
    {
        Tile tile = Tiles[fromTileIndex];
        Tiles.RemoveAt(fromTileIndex);
        Tiles.Insert(toTileIndex, tile);
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
        Vector2I tileMapSize = tileMap.GetSize() - offset + separation;
        var cellCount = new Vector2I(
            tileMapSize.X / (TileSize.X + separation.X), 
            tileMapSize.Y / (TileSize.Y + separation.Y));

        var number = 0;
        for (var y = 0; y < cellCount.Y; y++)
        {
            for (var x = 0; x < cellCount.X; x++)
            {
                if (tileLimit != 0 && number++ >= tileLimit) return;
                CreateTileFromTileMap(tileMap, GetTilePosition(new Vector2I(x, y), separation, offset));
            }
        }

        for (int i = number; i < tileLimit; i++)
        {
            Tiles.Add(new Tile(TileSize));
        }

        ClearFirstTile();
    }

    private void ClearFirstTile()
    {
        if (Tiles.Count >= 1)
        {
            Tiles.RemoveAt(0);
        }
        Tiles.Insert(0, new Tile(TileSize));
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
        SubViewport viewport = CreateViewport(tileMapSize, viewportContainer);
        
        AddTilesToViewport(groupCount, groupColor, separation, offset, 
            columnCount, viewport, Tiles.Count + groupOffset);
        
        await ToSignal(RenderingServer.Singleton, RenderingServer.SignalName.FramePostDraw);
        viewport.QueueFree();
        return viewport.GetTexture().GetImage();
    }
    
    private static SubViewport CreateViewport(Vector2I tileMapSize, GodotObject viewportContainer)
    {
        var viewport = new SubViewport()
        {
            Size = tileMapSize,
            TransparentBg = true,
            RenderTargetUpdateMode = SubViewport.UpdateMode.Always
        };
        viewportContainer.CallDeferred("add_child", viewport);
        return viewport;
    }

    private void AddTilesToViewport(int groupCount, IReadOnlyList<Color> groupColor,
        Vector2I separation, Vector2I offset, int columnCount, Node viewport, int groupTileCount)
    {
        var position = new Vector2I();
        Sprite2D blankSprite = new Tile(TileSize).Sprite;
        Resource material = GD.Load(ShaderMaterialColorPath);
        for (var group = 0; group < groupCount; group++)
        {
            Node2D spriteContainer = CreateSpriteContainer(groupColor[group], material);
            
            for (var i = 0; i < groupTileCount; i++)
            {
                Vector2I tilePosition = GetTilePosition(position, separation, offset);
                position = GetNextPosition(columnCount, position);
                AddTileSprite(i < Tiles.Count ? Tiles[i].Sprite : blankSprite, spriteContainer, tilePosition);
            }
            viewport.AddChild(spriteContainer);
        }
    }
    
    private static Node2D CreateSpriteContainer(Color color, Resource material)
    {
        var spriteContainer = new Node2D();
        var shaderColor = new Vector3(color.R, color.G, color.B);
        spriteContainer.Material = (Material)material.Duplicate();
        ((ShaderMaterial)spriteContainer.Material).SetShaderParameter("Color", shaderColor);
        return spriteContainer;
    }
    
    private static void AddTileSprite(Node sprite, Node container, Vector2I tilePosition)
    {
        var duplicate = (Sprite2D)sprite.Duplicate();
        duplicate.Centered = false;
        duplicate.Position = tilePosition;
        container.AddChild(duplicate);
    }

    private static Vector2I GetNextPosition(int columnCount, Vector2I position)
    {
        if (++position.X < columnCount) return position;
        position.X = 0;
        position.Y++;
        return position;
    }

    private Vector2I GetTilePosition(Vector2I position, Vector2I separation, Vector2I offset)
    {
        return new Vector2I(
            offset.X + position.X * (TileSize.X + separation.X), 
            offset.Y + position.Y * (TileSize.Y + separation.Y));
    }
}
