using Godot;
using System;

public class Tile
{
    private bool[] pixels;

    public byte[] Widths { get; private set; }
    public byte[] Heights { get; private set; }

    public Tile(Vector2I tileSize)
    {
        Heights = new byte[tileSize.X];
        Widths = new byte[tileSize.Y];
        pixels = new bool[tileSize.Y * tileSize.X];
        Pixels = pixels;
    }

    public bool[] Pixels
    {
        get => pixels;
        set
        {
            if (value.Length != pixels.Length)
            {
                throw new ArgumentException("Wrong array length");
            }

            pixels = value;
            Heights = new byte[Heights.Length];
            Widths = new byte[Widths.Length];

            CalculateCollisionArrays();
        }
    }

    private void CalculateCollisionArrays()
    {
        for (int x = Heights.Length - 1; x >= 0; x--)
        {
            for (int y = Widths.Length - 1; y >= 0; y--)
            {
                if (!Pixels[y * Heights.Length + x]) continue;
                Widths[y]++;
                Heights[x]++;
            }
        }
    }
}
