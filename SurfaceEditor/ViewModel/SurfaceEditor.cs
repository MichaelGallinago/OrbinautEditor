using Godot;

public partial class SurfaceEditor : Control
{
    private static readonly Vector2I BaseSurfaceSize = new(16, 16);
    public static Vector2I SurfaceSize;
    public static TextureRectSurface Surface { get; set; }
    public static TextureRectBackground Background { get; set; }
    
    public SurfaceEditor()
    {
        SurfaceSize = BaseSurfaceSize;
        Background = null;
        CreateSurface();
    }

    public static void CreateSurface()
    {
        var image = Image.Create(SurfaceSize.X, SurfaceSize.Y, false, Image.Format.Rgba8);
        SetContainerTexture(Surface, image);
    }

    public static void SetContainerTexture(TextureRect container, Image image)
    {
        container.Texture = ImageTexture.CreateFromImage(image); 
    }
}
