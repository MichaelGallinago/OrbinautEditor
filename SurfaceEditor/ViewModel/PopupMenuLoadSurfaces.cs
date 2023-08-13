using Godot;
using System;

public partial class PopupMenuLoadSurfaces : PopupMenuHandler
{
    public PopupMenuLoadSurfaces()
    {
        Name = "loadMenu";
        AddItem("Binary Surface", 0);
        AddItem("Image Surface", 1);
        AddItem("Background", 2);
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: OnBinarySurfacePressed(); break;
            case 1: OnImageSurfacePressed(); break;
            case 2: OnBackgroundPressed(); break;
        }
    }
    //TODO
    private static void OnBinarySurfacePressed()
    {
        //SurfaceEditor.OpenFileDialog(BinaryFile.Filters, FileDialog.FileModeEnum.OpenFile, 
        //    SurfaceEditor.OpenSurface, "Load Surface", string.Empty);
    }
    
    private static void OnImageSurfacePressed()
    {
        //SurfaceEditor.OpenFileDialog(BinaryFile.Filters, FileDialog.FileModeEnum.OpenFile, 
        //    SurfaceEditor.OpenSurface, "Load Surface", string.Empty);
    }

    private static void OnBackgroundPressed()
    {
        //SurfaceEditor.OpenFileDialog(BinaryFile.Filters, FileDialog.FileModeEnum.OpenFile, 
        //    SurfaceEditor.OpenBackground, "Load AngleMap", string.Empty);
    }
}
