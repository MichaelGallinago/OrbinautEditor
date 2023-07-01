using Godot;
using System;

public class Tile
{
    public Sprite2D Sprite { get; }

    public byte[] Widths { get; private set; }
    public byte[] Heights { get; private set; }

    public Tile(Vector2I tileSize)
    {
        Sprite = new Sprite2D();
        Heights = new byte[tileSize.X];
        Widths = new byte[tileSize.Y];
        
        SetImage(Image.Create(tileSize.X, tileSize.Y, false, Image.Format.Rgba8));
    }

    public void SetImage(Image image)
    {
        var texture = ImageTexture.CreateFromImage(image);
        
        if ((Vector2I)texture.GetSize() != new Vector2I(Heights.Length, Widths.Length))
        {
            throw new ArgumentException("Wrong image size");
        }

        Sprite.Texture = texture;
        CalculateCollisionArrays(image);
    }

    public Image GetImage()
    {
        return Sprite.Texture.GetImage();
    }

    private void CalculateCollisionArrays(Image image)
    {
        Heights = new byte[Heights.Length];
        Widths = new byte[Widths.Length];

        for (int x = Heights.Length - 1; x >= 0; x--)
        {
            for (int y = Widths.Length - 1; y >= 0; y--)
            {
                if (image.GetPixel(x, y).A8 == 0) continue;
                Widths[y]++;
                Heights[x]++;
            }
        }
    }
}
