using Godot;

public partial class PopupMenuLoadSurfaces : PopupMenuHandler
{
    public PopupMenuLoadSurfaces()
    {
        Name = "loadMenu";
        AddItem("Binary Surface", 0);
        AddItem("Image Surface", 1);
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: OnBinarySurfacePressed(); break;
            case 1: OnImageSurfacePressed(); break;
        }
    }
    
    private static void OnBinarySurfacePressed()
    {
        SurfaceEditor.FileDialog.Open(BinaryFile.Filters, FileDialog.FileModeEnum.OpenFile, 
            SurfaceEditor.OpenBinarySurface, "Load Binary Surface", string.Empty);
    }
    
    private static void OnImageSurfacePressed()
    {
        SurfaceEditor.FileDialog.Open(BinaryFile.Filters, FileDialog.FileModeEnum.OpenFile, 
            SurfaceEditor.OpenImageSurface, "Load Image Surface", string.Empty);
    }
}
