using Godot;

namespace OrbinautEditor.SurfaceEditor.ViewModel;

public partial class SurfaceEditor : Control
{
    private static readonly Vector2I BaseSurfaceSize = new(16, 16);
    public static Vector2I SurfaceSize;
    public static TextureRectSurface Surface { get; set; }
    public static TextureRectBackground Background { get; set; }
    public static General.Model.CustomFileDialog FileDialog { get; private set; }
    
    public SurfaceEditor()
    {
        SurfaceSize = BaseSurfaceSize;
        Background = null;
        FileDialog = new General.Model.CustomFileDialog();
    }

    public override void _Ready()
    {
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

    public static void OpenBinarySurface(string path)
    {
        
    }
    
    public static void OpenImageSurface(string path)
    {
        
    }
    
    public static void SaveBinarySurface(string path)
    {
        
    }
    
    public static void SaveImageSurface(string path)
    {
        
    }
}