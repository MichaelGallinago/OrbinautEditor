using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TileSet
{
    public Vector2I TileSize { get; }

    public List<Tile> Tiles { get; private set; }

    public TileSet(string path, int tileWidth = 16, int tileHeight = 16,
        Vector2I separation = new(), Vector2I offset = new())
    {
        TileSize = new Vector2I(tileWidth, tileHeight);
        Tiles = new List<Tile>();

        var image = new Image();
        Error loadError = image.Load(path);
        if (loadError != Error.Ok)
        {
            throw new FileLoadException();
        }
        
        var texture = ImageTexture.CreateFromImage(image);

        CreateTiles(texture, separation, offset);
    }

    public TileSet(int angleCount = 0, int tileWidth = 16, int tileHeight = 16)
    {
        TileSize = new Vector2I(tileWidth, tileHeight);
        Tiles = new List<Tile>(angleCount);

        for (int i = 0; i < angleCount; i++)
        {
            Tiles.Add(new Tile(TileSize));
        }
    }

    public ImageTexture DrawTileMap(int columnCount, Color[] groupColor,
        int[] groupOffset, Vector2I separation, Vector2I offset)
    {
        var cell = new Vector2I(
            TileSize.X + separation.X,
            TileSize.Y + separation.Y);

        int rowCount = (int)Math.Ceiling((Tiles.Count * groupOffset.Length
            + groupOffset.Sum()) / (double)columnCount);

        var tileMapSize = new Vector2I(
            offset.X + columnCount * cell.X - separation.X,
            offset.Y + rowCount * cell.Y - separation.Y);
        
        var tileMap = new ImageTexture(
            tileMapSize.X, tileMapSize.Y,
            SKColorType.Rgba8888, SKAlphaType.Premul);
        byte[,,] bitmapArray = BitmapConvertor.GetBitmapArrayFromSKBitmap(tileMap);

        int groupCount = groupColor.Length;

        DrawTiles(bitmapArray, groupOffset, groupColor, separation,
            offset, columnCount, groupCount);

        return BitmapConvertor.GetSKBitmapFromBitmapArray(bitmapArray);
    }

    public void ChangeTile(int tileIndex, Vector2I pixelPosition, bool isLeftButtonPressed)
    {
        bool[] pixels = Tiles[tileIndex].Pixels;

        if (isLeftButtonPressed)
        {
            ChangeHeight(pixels, pixelPosition);
        }
        else
        {
            int pixelOnPositionIndex = GetPixelIndex(pixelPosition.X, pixelPosition.Y);
            pixels[pixelOnPositionIndex] = !pixels[pixelOnPositionIndex];
        }

        Tiles[tileIndex].Pixels = pixels;
    }

    public void InsertTile(int tileIndex)
    {
        Tiles.Insert(tileIndex, new Tile(TileSize));
    }

    public void RemoveTile(int tileIndex)
    {
        Tiles.RemoveAt(tileIndex);
    }

    private void ChangeHeight(bool[] pixels, Vector2I pixelPosition)
    {
        if (!pixels[GetPixelIndex(pixelPosition.X, pixelPosition.Y)] ||
            pixelPosition.Y != 0 && pixels[GetPixelIndex(pixelPosition.X, pixelPosition.Y - 1)])
        {
            for (int y = 0; y < TileSize.Y; y++)
            {
                pixels[GetPixelIndex(pixelPosition.X, y)] = y >= pixelPosition.Y;
            }
        }
        else
        {
            for (int y = 0; y < TileSize.Y; y++)
            {
                pixels[GetPixelIndex(pixelPosition.X, y)] = false;
            }
        }
    }

    private void CreateTiles(ImageTexture tileMap, Vector2I separation, Vector2I offset)
    {
        var cellCount = new Vector2I(
            (tileMap.GetWidth() - offset.X) / (TileSize.X + separation.X),
            (tileMap.GetHeight() - offset.Y) / (TileSize.Y + separation.Y));

        byte[,,] bitmapArray = BitmapConvertor.GetBitmapArrayFromSKBitmap(tileMap);

        for (int y = 0; y < cellCount.Y; y++)
        {
            for (int x = 0; x < cellCount.X; x++)
            {
                var tilePosition = new Vector2I(
                    x * (TileSize.X + separation.X) + offset.X,
                    y * (TileSize.Y + separation.Y) + offset.Y);

                CreateTileFromTileMap(bitmapArray, tilePosition);
            }
        }
    }

    private void CreateTileFromTileMap(byte[,,] bitmapArray, Vector2I tilePosition)
    {
        var tile = new Tile(TileSize);
        bool[] tilePixels = tile.Pixels;

        for (int w = 0; w < TileSize.Y; w++)
        {
            for (int z = 0; z < TileSize.X; z++)
            {
                tilePixels[w * TileSize.X + z] = bitmapArray[
                tilePosition.X + z, tilePosition.Y + w, 0] != 0;
            }
        }

        tile.Pixels = tilePixels;
        Tiles.Add(tile);
    }

    private void DrawTiles(byte[,,] bitmapArray, int[] groupOffset, Color[] groupColor,
        Vector2I separation, Vector2I offset, int columnCount, int groupCount)
    {
        var white = new Color(1F, 1F, 1F, 1F);
        Vector2I position = new();

        for (int group = 0; group < groupCount; group++)
        {
            foreach (Tile tile in Tiles)
            {
                DrawTile(bitmapArray, tile.Pixels, groupColor[group],
                    separation, offset, columnCount, ref position);
            }

            while (groupOffset[group]-- > 0)
            {
                DrawTile(bitmapArray, null, white,
                    separation, offset, columnCount, ref position);
            }
        }
    }

    private void DrawTile(byte[,,] bitmapArray, bool[]? tilePixels, Color tileColor,
        Vector2I separation, Vector2I offset, int columnCount, ref Vector2I position)
    {
        Color secondColor = GetSecondColor(tilePixels, out bool[] checkedTilePixels);

        var tilePosition = new Vector2I(
            offset.X + position.X * (TileSize.X + separation.X),
            offset.Y + position.Y * (TileSize.Y + separation.Y));

        for (int y = 0; y < TileSize.Y; y++)
        {
            for (int x = 0; x < TileSize.X; x++)
            {
                Color pixelColor = checkedTilePixels[y * TileSize.X + x] ? tileColor : secondColor;
                for (int i = 0; i < 4; i++)
                {
                    bitmapArray[tilePosition.X + x, tilePosition.Y + y, i] = (byte)(pixelColor[i] * 255f);
                }
            }
        }

        position = position.X + 1 >= columnCount ?
            new Vector2I(0, position.Y + 1) :
            new Vector2I(position.X + 1, position.Y);
    }

    private Color GetSecondColor(bool[]? tilePixels, out bool[] checkedTilePixels)
    {
        if (tilePixels is not null)
        {
            checkedTilePixels = tilePixels;
            return new Color(0F, 0F, 0F, 0F);
        }

        checkedTilePixels = new bool[TileSize.X * TileSize.Y];
        for (int y = 0; y < TileSize.Y; y++)
        {
            for (int x = 0; x < TileSize.X; x++)
            {
                checkedTilePixels[y * TileSize.X + x] = (x + y) % 4 < 2;
            }
        }

        return new Color(0F, 0F, 0F, 1F);
    }

    private int GetPixelIndex(int positionX, int positionY)
    {
        return positionY * TileSize.X + positionX;
    }
}
