using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class TileSet : GodotObject
{
    public Vector2I TileSize { get; private set; }
    public List<Tile> Tiles { get; }
    
    private Control screen;

    public TileSet(Control screen, string imagePath, 
        Vector2I tileSize, Vector2I separation, Vector2I offset)
    {
        TileSize = tileSize;
        Tiles = new List<Tile>();
        this.screen = screen;

        var image = new Image();
        Error loadError = image.Load(imagePath);
        if (loadError != Error.Ok)
        {
            throw new FileLoadException("TileSet");
        }
        
        CreateTiles(image, separation, offset);
    }

    public TileSet(Control screen, int angleCount = 0, Vector2I tileSize = new())
    {
        TileSize = tileSize;
        Tiles = new List<Tile>(angleCount);
        this.screen = screen;

        for (var i = 0; i < angleCount; i++)
        {
            Tiles.Add(new Tile(TileSize));
        }
    }

    public async Task<Image> CreateTileMap(int columnCount, Color[] groupColor,
        int[] groupOffset, Vector2I separation, Vector2I offset)
    {
        var cell = new Vector2I(
            TileSize.X + separation.X,
            TileSize.Y + separation.Y);

        double tilesRowWidth = Tiles.Count * groupOffset.Length + groupOffset.Sum();
        int rowCount = Mathf.CeilToInt(tilesRowWidth / columnCount);

        var tileMapSize = new Vector2I(
            offset.X + columnCount * cell.X - separation.X,
            offset.Y + rowCount * cell.Y - separation.Y);
        
        int groupCount = groupColor.Length;

        return await DrawTiles(tileMapSize, groupOffset, groupColor, 
            separation, offset, columnCount, groupCount);
    }

    public void ChangeTile(int tileIndex, Vector2I pixelPosition, bool isLeftButtonPressed)
    {
        Image image = Tiles[tileIndex].GetImage();
        
        ChangeTileHeight(image, isLeftButtonPressed 
            ? pixelPosition : new Vector2I(pixelPosition.X, image.GetHeight()));
        
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
        if (image.GetPixelv(pixelPosition) == Colors.Transparent || pixelPosition.Y != 0 
            && image.GetPixel(pixelPosition.X, pixelPosition.Y - 1) != Colors.Transparent)
        {
            for (var y = 0; y < TileSize.Y; y++)
            {
                image.SetPixel(pixelPosition.X, y, y >= pixelPosition.Y 
                    ? Colors.Black : Colors.Transparent);
            }
            return;
        }
        
        for (var y = 0; y < TileSize.Y; y++)
        {
            image.SetPixel(pixelPosition.X, y, Colors.Transparent);
        }
    }

    private void CreateTiles(Image tileMap, Vector2I separation, Vector2I offset)
    {
        var cellCount = new Vector2I(
            (tileMap.GetWidth() - offset.X) / (TileSize.X + separation.X),
            (tileMap.GetHeight() - offset.Y) / (TileSize.Y + separation.Y));

        for (var y = 0; y < cellCount.Y; y++)
        {
            for (var x = 0; x < cellCount.X; x++)
            {
                var tilePosition = new Vector2I(
                    x * (TileSize.X + separation.X) + offset.X,
                    y * (TileSize.Y + separation.Y) + offset.Y);

                CreateTileFromTileMap(tileMap, tilePosition);
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

    private async Task<Image> DrawTiles(Vector2I tileMapSize, IList<int> groupOffset, IReadOnlyList<Color> groupColor,
        Vector2I separation, Vector2I offset, int columnCount, int groupCount)
    {
        var viewport = new SubViewport()
        {
            Size = tileMapSize,
            TransparentBg = true,
            RenderTargetUpdateMode = SubViewport.UpdateMode.Always
        };
        screen.AddChild(viewport);
        
        var position = new Vector2I();
        Sprite2D blankSprite = new Tile(TileSize).Sprite;
        for (var group = 0; group < groupCount; group++)
        {
            Vector2I tilePosition;
            foreach (Tile tile in Tiles)
            {
                tilePosition = GetNextTilePosition(separation, offset, columnCount, position);
                AddTileSprite(viewport, tile.Sprite, groupColor[group], tilePosition);
            }
            
            while (groupOffset[group]-- > 0)
            {
                tilePosition = GetNextTilePosition(separation, offset, columnCount, position);
                AddTileSprite(viewport, blankSprite, groupColor[group], tilePosition);
            }
        }
        
        await ToSignal(RenderingServer.Singleton, RenderingServer.SignalName.FramePostDraw);
        Image image = viewport.GetTexture().GetImage();
        viewport.QueueFree();
        return image;
    }

    private void AddTileSprite(SubViewport viewport, 
        Sprite2D sprite, Color tileColor, Vector2I tilePosition)
    {
        var duplicate = (Sprite2D)sprite.Duplicate();

        duplicate.Position = tilePosition;
        viewport.AddChild(duplicate);
        duplicate.Show();
        // TODO: tileColor
    }

    private Vector2I GetNextTilePosition(Vector2I separation, 
        Vector2I offset, int columnCount, Vector2I position)
    {
        var tilePosition = new Vector2I(
            offset.X + position.X * (TileSize.X + separation.X),
            offset.Y + position.Y * (TileSize.Y + separation.Y));

        if (position.X + 1 >= columnCount)
        {
            position.X = 0;
            position.Y++;
        }
        else
        {
            position.X++;   
        }

        return tilePosition;
    }
}
