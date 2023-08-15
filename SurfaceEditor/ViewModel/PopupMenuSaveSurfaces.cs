using Godot;
using System;

public partial class PopupMenuSaveSurfaces : PopupMenuHandler
{
    public PopupMenuSaveSurfaces()
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
        SurfaceEditor.FileDialog.Open(BinaryFile.Filters, FileDialog.FileModeEnum.SaveFile, 
            SurfaceEditor.SaveBinarySurface, "Save Binary Surface", string.Empty);
    }
    
    private static void OnImageSurfacePressed()
    {
        SurfaceEditor.FileDialog.Open(ImageFile.Filters, FileDialog.FileModeEnum.SaveFile, 
            SurfaceEditor.SaveImageSurface, "Save Image Surface", string.Empty);
    }
}
